using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using xManik.EF;
using xManik.Models;
using xManik.Repositories;
using xManik.Extensions.Managers;
using xManik.Models.UserUserProfileViewModels;

namespace xManik.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly WorkContext _context;
        private readonly UserUserProfileManager<UserUserProfile> _userUserProfileManager;
        private readonly IHostingEnvironment _environment;

        public UserProfileController(
              UserManager<ApplicationUser> userManager,
              ILogger<ManageController> logger,
              ApplicationDbContext context,
              IHostingEnvironment environment)
        {
            _userManager = userManager;
            _logger = logger;
            _context = new WorkContext(context);
            _userUserProfileManager = new UserUserProfileManager<UserUserProfile>(_context);
            _environment = environment;
        }

        [TempData]
        public string StatusMessage { get; set; }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userUserProfile = _userUserProfileManager.GetUserUserProfile(User);
            var model = new IndexViewModel
            {
                FirstName = userUserProfile.FirstName,
                SecondName = userUserProfile.SecondName,
                UserProfileImagePath = userUserProfile.ImageName,
                Description = userUserProfile.Description,
                Rate = userUserProfile.Rate,
                DateRegistered = userUserProfile.DateRegistered
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

            var userUserProfile = _userUserProfileManager.GetUserUserProfile(User);
            if (model.FirstName != string.Empty)
            {
                await _userUserProfileManager.ChangeFirstNameAsync(userUserProfile, model.FirstName);
            }

            if (model.SecondName != string.Empty)
            {
                await _userUserProfileManager.ChangeSecondNameAsync(userUserProfile, model.SecondName);
            }

            if (model.Description != string.Empty)
            {
                await _userUserProfileManager.ChangeDescriptionAsync(userUserProfile, model.Description);
            }

            if (file != null)
            {
                await _userUserProfileManager.ChangeUserProfilePhotoAsync(userUserProfile, file, _environment.WebRootPath);
            }

            StatusMessage = "Your UserProfile has been updated";
            return RedirectToAction(nameof(Index));
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var result = context.Result as ViewResult;
            if (result != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //    var newOrdersNum = _context.Orders.Where(o => o.ProviderId == userId && !o.IsRead).Count();
                // result.ViewData["newMsgs"] = newOrdersNum;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public IActionResult Orders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Order> orders = null; // _context.Orders.Where(o => o.ProviderId == userId);

            //orders = orders.OrderBy(o => o.IsRead);

            return View(orders);
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> OrderDetails(string orderId)
        {
            //var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            //var customer = await _context.Users.FirstOrDefaultAsync(u => u.Id == order.CustomerId);
            //var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == order.ServiceId);

            //if (order == null || customer == null || service == null)
            //{
            //    return NotFound();
            //}

            //if (!order.IsRead)
            //{
            //    order.IsRead = true;
            //    await _context.SaveChangesAsync();
            //}

            //OrderDetailsViewModel orderDetail = new OrderDetailsViewModel
            //{
            //    CustomerName = customer.UserName,
            //    CustomerEmail = customer.Email,
            //    CustomerRegistered = customer.DateRegistered,
            //    ServiceDescription = service.Description,
            //    ServicePrice = service.Price,
            //    ServicePosted = service.DatePublished,
            //    AdditionalServiceInfo = order.AdditionalInfo,
            //    ServiceStartTime = order.StartTime,
            //    ServiceEndTime = order.EndTime
            //};

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Portfolio()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var provider = await _context.Providers.Include(p => p.User).Include(p => p.Portfolio).Where(p => p.Id == userId).FirstOrDefaultAsync();

            //if (provider == null)
            //{
            //    throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            //}

            //var model = new PortfolioViewModel
            //{
            //    StatusMessage = StatusMessage,
            //    Images = provider.Portfolio
            //};

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Provider")]
        //public async Task<IActionResult> Portfolio(PortfolioViewModel model, IFormFile file)
        //{
        //    if (!ModelState.IsValid || file == null)
        //    {
        //        return RedirectToAction(nameof(Portfolio));
        //    }

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var provider = await _context.Providers.Include(p => p.User).Include(p => p.Portfolio).Where(p => p.Id == userId).FirstOrDefaultAsync();

        //    if (provider == null)
        //    {
        //        throw new ApplicationException($"Unable to load user with ID '{userId}'.");
        //    }

        //    if (file != null)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await file.CopyToAsync(memoryStream);
        //            var item = new Artwork
        //            {
        //                Description = model.Description,
        //                Image = memoryStream.ToArray()
        //            };
        //            provider.Portfolio.Add(item);
        //            _context.Update(provider);
        //            await _context.SaveChangesAsync();
        //        }
        //    }

        //    StatusMessage = "Your UserProfile has been updated";

        //    return RedirectToAction(nameof(Portfolio));
        //}

        //[HttpGet]
        //[Authorize(Roles = "Provider")]
        //public async Task<IActionResult> EditPortfolioItem(string id)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var image = await _context.Artworks.Include(p => p.Provider).Where(p => p.Id == id).FirstOrDefaultAsync();
        //    if (image.Provider.Id != userId)
        //    {
        //        return NotFound();
        //    }

        //    return View(image);
        //}

        //[HttpPost]
        //[Authorize(Roles = "Provider")]
        //public async Task<IActionResult> EditPortfolioItem(Artwork model, IFormFile file)
        //{
        //    if (file != null)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await file.CopyToAsync(memoryStream);
        //            model.Image = memoryStream.ToArray();
        //        }
        //    }
        //    _context.Update(model);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Portfolio));
        //}

        //[HttpGet]
        //[Authorize(Roles = "Provider")]
        //public async Task<IActionResult> RemovePortfolioImage(string id)
        //{
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var image = await _context.Artworks.Include(p => p.Provider).Where(p => p.Id == id).FirstOrDefaultAsync();
        //    if (image.Provider.Id != userId)
        //    {
        //        return NotFound();
        //    }
        //    _context.Remove(image);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Portfolio));
        //}


        [HttpGet]
        [Authorize(Roles = "Provider")]
        public IActionResult UserProfileDescription()
        {
            var user = _userUserProfileManager.GetUserUserProfile(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{user.Id}'.");
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
        public async Task<IActionResult> UserProfileDescription(DescriptionViewModel model)
        {
            await _userUserProfileManager.ChangeDescriptionAsync(User, model.Description);
            StatusMessage = "Your UserProfile has been updated";

            return RedirectToAction(nameof(UserProfileDescription));
        }


    }
}