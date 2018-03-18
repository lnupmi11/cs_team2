using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using xManik.Data;
using xManik.Extensions;
using xManik.Models;

namespace xManik.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public PaymentController(
            ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> PromoteService(string id, string stripeToken)
        {
            var service = await _context?.Services.FirstOrDefaultAsync(p => p.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                NotFound();
            }

            if (!service.IsPromoted)
            {
                var customers = new StripeCustomerService();
                var charges = new StripeChargeService();

                var customer = customers.Create(new StripeCustomerCreateOptions
                {
                    Email = user.Email,
                    SourceToken = stripeToken
                });

                var charge = charges.Create(new StripeChargeCreateOptions
                {
                    Amount = 500,
                    Description = "Service promotion charge.",
                    Currency = "usd",
                    CustomerId = customer.Id
                });

                service.IsPromoted = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Services");
        }

        public async Task<IActionResult> Index(string serviceId)
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null || serviceId == null)
            {
                NotFound();
            }

            ViewBag.Id = serviceId;
            ViewBag.Description = "Service promotion charge.";
            ViewBag.Email = user.Email;
            ViewBag.Amount = 500;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}