using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using xManik.EF;
using xManik.Extensions.Managers;
using xManik.Models;
using xManik.Models.ProfileViewModels;
using xManik.Repositories;

namespace xManik.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserProfileManager<ApplicationUser> _profileManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly WorkContext _context;
        private UserProfile _userProfile;
        private IHostingEnvironment _hostingEnvironment;

        public ProfileController(
              UserManager<ApplicationUser> userManager,
              ILogger<ManageController> logger,
              ApplicationDbContext context,
              IHostingEnvironment environment)
        {
            _context = new WorkContext(context);
            _profileManager = new UserProfileManager<ApplicationUser>(_context);
            _userProfile = _profileManager.GetUserProfile(User);
            _userManager = userManager;
            _hostingEnvironment = environment;
            _logger = logger;
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

            var model = new IndexViewModel
            {
                FirstName = _userProfile.FirstName,
                SecondName = _userProfile.SecondName,
                ProfileImagePath = _userProfile.ImageName,
                Description = _userProfile.Description,
                Rate = _userProfile.Rate,
                DateRegistered = _userProfile.DateRegistered
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

            if(model.FirstName != string.Empty)
            {
                _userProfile.FirstName = model.FirstName;
            }

            if (model.SecondName != string.Empty)
            {
                _userProfile.FirstName = model.SecondName;
            }

            if (model.Description != string.Empty)
            {
                _userProfile.Description = model.FirstName;
            }

            if (file != null)
            {
                _userProfile.ImageName = file.FileName;
                _profileManager.ChangeProfilePhoto(_userProfile, file, _hostingEnvironment.WebRootPath);
            }

            await _profileManager.UpdateSaveAsync(_userProfile);
            StatusMessage = "Your profile has been updated";

            return RedirectToAction(nameof(Index));
        }
    }
}