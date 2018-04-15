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
using xManik.Managers;
using xManik.Models.UserProfileViewModels;
using Microsoft.EntityFrameworkCore;

namespace xManik.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly WorkContext _context;
        private readonly UserProfileManager<UserProfile> _userProfileManager;
        private readonly IHostingEnvironment _environment;
        private readonly ChanelsManager<Chanel> _chanelsManager;

        public UserProfileController(
              UserManager<ApplicationUser> userManager,
              ILogger<ManageController> logger,
              ApplicationDbContext context,
              IHostingEnvironment environment)
        {
            _userManager = userManager;
            _logger = logger;
            _context = new WorkContext(context);
            _userProfileManager = new UserProfileManager<UserProfile>(_context);
            _environment = environment;
            _chanelsManager = new ChanelsManager<Chanel>(_context);
        }

        [TempData]
        public string StatusMessage { get; set; }

        #region Profile managment
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userProfile = _userProfileManager.GetUserProfile(User);
            var model = new IndexViewModel
            {
                FirstName = userProfile.FirstName,
                SecondName = userProfile.SecondName,
                UserProfileImagePath = userProfile.ImageName,
                DateRegistered = userProfile.DateRegistered
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

            var userProfile = _userProfileManager.GetUserProfile(User);
            if (model.FirstName != string.Empty)
            {
                await _userProfileManager.ChangeFirstNameAsync(userProfile, model.FirstName);
            }

            if (model.SecondName != string.Empty)
            {
                await _userProfileManager.ChangeSecondNameAsync(userProfile, model.SecondName);
            }

            if (file != null)
            {
                await _userProfileManager.ChangeUserProfilePhotoAsync(userProfile, file, _environment.WebRootPath);
            }

            StatusMessage = "Your UserProfile has been updated";
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Chanels managment

        [Authorize(Roles = "Blogger")]
        public IActionResult UserChanels()
        {
            return View(_userProfileManager.GetAllChanels(User));
        }

        // GET: Chanels/DetailsChanel/5
        public IActionResult DetailsChanel(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chanel = _chanelsManager.Find(id);
            if (chanel == null)
            {
                return NotFound();
            }

            return View(chanel);
        }

        // GET: Chanels/DetailsChanel/5
        public ActionResult DetailsChanel(int id)
        {
            return View(_context.Chanels.Find(id.ToString()));
        }

        // GET: Chanels/CreateChanel
        [Authorize(Roles = "Blogger")]
        public IActionResult CreateChanel()
        {
            return View();
        }

        // POST: Chanels/CreateChanel
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Blogger")]
        public async Task<IActionResult> CreateChanel([Bind("Network,Category,Description")] Chanel chanel)
        {
            if (ModelState.IsValid)
            {
                chanel.UserProfileId = _userProfileManager.GetUserProfileId(User);
                await _chanelsManager.CreateAsync(chanel);

                return RedirectToAction(nameof(Index));
            }
            return View(chanel);
        }

        // GET: Chanels/EditChanel/5
        [Authorize(Roles = "Blogger")]
        public IActionResult EditChanel(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chanel = _chanelsManager.Find(id);
            if (chanel == null)
            {
                return NotFound();
            }
            return View(chanel);
        }

        // POST: Chanels/EditChanel/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Blogger")]
        public async Task<IActionResult> EditChanel(string id, [Bind("ChanelId,UserProfileId,Network,Category,Description")] Chanel chanel)
        {
            if (id != chanel.ChanelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _chanelsManager.UpdateAsync(chanel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_chanelsManager.IsChanelExists(chanel.ChanelId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chanel);
        }

        // GET: Chanels/DeleteChanel/5
        [Authorize(Roles = "Blogger")]
        public IActionResult DeleteChanel(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chanel = _chanelsManager.Find(id);
            if (chanel == null)
            {
                return NotFound();
            }

            return View(chanel);
        }

        // POST: Chanels/DeleteChanel/5
        [HttpPost, ActionName("DeleteChanel")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Blogger")]
        public async Task<IActionResult> DeleteChanelConfirmed(string id)
        {
            var chanel = _chanelsManager.Find(id);
            await _chanelsManager.RemoveAsync(chanel);

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}