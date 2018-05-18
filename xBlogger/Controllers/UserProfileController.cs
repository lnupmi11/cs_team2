using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using xBlogger.EF;
using xBlogger.Models;
using xBlogger.Repositories;
using xBlogger.Managers;
using xBlogger.Models.UserProfileViewModels;

namespace xBlogger.Controllers
{
    [Authorize(Roles = "Blogger,Client")]
    public class UserProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly WorkContext _context;
        private readonly UserProfileManager<UserProfile> _userProfileManager;
        private readonly IHostingEnvironment _environment;
        private readonly ChanelsManager<Chanel> _chanelsManager;
        private readonly DealsManager<Deal> _dealsManager;

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
            _dealsManager = new DealsManager<Deal>(_context);
        }

        [TempData]
        public string StatusMessage { get; set; }

        #region Profile managment

        [HttpGet]
        public async Task<IActionResult> ManageUserProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var userProfile = _userProfileManager.GetUserProfile(User);

            if (userProfile == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

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
        public async Task<IActionResult> ManageUserProfile(IndexViewModel model, IFormFile file)
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
            return RedirectToAction(nameof(ManageUserProfile));
        }

        #endregion

        [HttpGet]
        public IActionResult UserProfile(string id = null)
        {
            UserProfile userProfile = id == null ? _userProfileManager.GetUserProfile(User) :
                _userProfileManager.GetUserProfileById(id);

            var model = new IndexViewModel
            {
                FirstName = userProfile.FirstName,
                SecondName = userProfile.SecondName,
                UserProfileImagePath = userProfile.ImageName,
                DateRegistered = userProfile.DateRegistered
            };

            return View(model);
        }
    }
}