using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using xManik.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using xManik.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using xManik.Services;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace xManik.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";
            if (User.Identity.IsAuthenticated)
            {
               
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var cust = await _context.Clients.FindAsync(user.Id);
                var custp = await _context.Providers.FindAsync(user.Id);
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Client")]
        public IActionResult Artists()
        {
            List<Provider> artists = _context.Providers.ToList<Provider>();

            return View(artists);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
