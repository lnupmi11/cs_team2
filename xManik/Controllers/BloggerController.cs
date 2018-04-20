using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using xManik.EF;
using xManik.Models;
using xManik.Models.BloggerViewModels;
using xManik.Repositories;

namespace xManik.Controllers
{
    public class BloggerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WorkContext _context;

        public BloggerController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = new WorkContext(context);
        }

        public IActionResult Index()
        {   
            return View(GetAllBloggersViewModels());
        }

        private IEnumerable<BloggerViewModel> GetAllBloggersViewModels()
        {
            var usersId = _userManager.GetUsersInRoleAsync(Enum.GetName(typeof(Roles), Roles.Blogger)).Result.Select(p => p.Id);
            var bloggersProfiles = _context.UserProfiles.GetAllByIds(usersId);
            var bloggersViewModels = bloggersProfiles.Select(p => new BloggerViewModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                SecondName = p.SecondName,
                DateRegistered = p.DateRegistered,
                ImageName = p.ImageName,
                Chanels = p.Chanels
            });

            return bloggersViewModels;
        }
    }
}