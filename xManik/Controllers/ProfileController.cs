using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using xManik.Data;
using xManik.Models;
using xManik.Models.ProfileViewModels;

namespace xManik.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;

        public ProfileController(
              UserManager<ApplicationUser> userManager,
              ILogger<ManageController> logger,
              ApplicationDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var result = context.Result as ViewResult;
            if (result != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var newOrdersNum = _context.Orders.Where(o => o.ProviderId == userId && !o.IsRead).Count();
                result.ViewData["newMsgs"] = newOrdersNum;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Orders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _context.Orders.Where(o => o.ProviderId == userId);

            orders = orders.OrderBy(o => o.IsRead);

            return View(await orders.ToListAsync());
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> OrderDetails(string orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            var customer = await _context.Users.FirstOrDefaultAsync(u => u.Id == order.CustomerId);
            var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == order.ServiceId);

            if (order == null || customer == null || service == null)
            {
                return NotFound();
            }

            if (!order.IsRead)
            {
                order.IsRead = true;
                await _context.SaveChangesAsync();
            }

            OrderDetailsViewModel orderDetail = new OrderDetailsViewModel
            {
                CustomerName = customer.UserName,
                CustomerEmail = customer.Email,
                CustomerRegistered = customer.DateRegistered,
                ServiceDescription = service.Description,
                ServicePrice = service.Price,
                ServicePosted = service.DatePublished,
                AdditionalServiceInfo = order.AdditionalInfo,
                ServiceStartTime = order.StartTime,
                ServiceEndTime = order.EndTime
            };

            return View(orderDetail);
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Portfolio()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var provider = await _context.Providers.Include(p => p.User).Include(p => p.Portfolio).Where(p => p.Id == userId).FirstOrDefaultAsync();

            if (provider == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            var model = new PortfolioViewModel
            {
                StatusMessage = StatusMessage,
                Images = provider.Portfolio
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Portfolio(PortfolioViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid || file == null)
            {
                return RedirectToAction(nameof(Portfolio));
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var provider = await _context.Providers.Include(p => p.User).Include(p => p.Portfolio).Where(p => p.Id == userId).FirstOrDefaultAsync();

            if (provider == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var item = new Artwork
                    {
                        Description = model.Description,
                        Image = memoryStream.ToArray()
                    };
                    provider.Portfolio.Add(item);
                    _context.Update(provider);
                    await _context.SaveChangesAsync();
                }
            }

            StatusMessage = "Your profile has been updated";

            return RedirectToAction(nameof(Portfolio));
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> EditPortfolioItem(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var image = await _context.Artworks.Include(p => p.Provider).Where(p => p.Id == id).FirstOrDefaultAsync();
            if (image.Provider.Id != userId)
            {
                return NotFound();
            }

            return View(image);
        }

        [HttpPost]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> EditPortfolioItem(Artwork model, IFormFile file)
        {
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    model.Image = memoryStream.ToArray();
                }
            }
            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Portfolio));
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> RemovePortfolioImage(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var image = await _context.Artworks.Include(p => p.Provider).Where(p => p.Id == id).FirstOrDefaultAsync();
            if (image.Provider.Id != userId)
            {
                return NotFound();
            }
            _context.Remove(image);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Portfolio));
        }


        [HttpGet]
        public async Task<IActionResult> ProfileDescription()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Providers.Where(p => p.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            DescriptionViewModel model = new DescriptionViewModel()
            {
                StatusMessage = StatusMessage,
                Description = user.Description
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> ProfileDescription(DescriptionViewModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Providers.Where(p => p.Id == userId).FirstOrDefaultAsync();
            user.Description = model.Description;
            _context.Update(user);
            await _context.SaveChangesAsync();
            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(ProfileDescription));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage,
                ProfileImage = user.ProfileImage,
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    user.ProfileImage = memoryStream.ToArray();
                    await _userManager.UpdateAsync(user);
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }
    }
}
