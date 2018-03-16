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
            if (id == null)
            {
                return NotFound();
            }

            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new ApplicationException("cannot find item with this id");
            }

            var user = await _context.Providers.Include(p => p.Services).Include(p => p.User).Where(p => p.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            var service = user.Services.SingleOrDefault(p => p.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            if(!service.IsPromoted)
            {
                var customers = new StripeCustomerService();
                var charges = new StripeChargeService();

                var customer = customers.Create(new StripeCustomerCreateOptions
                {
                    Email = user.User.Email,
                    SourceToken = stripeToken
                });

                var charge = charges.Create(new StripeChargeCreateOptions
                {
                    Amount = 500,
                    Description = "Service promotion Charge " + user.User.Email,
                    Currency = "usd",
                    CustomerId = customer.Id
                });

                service.IsPromoted = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Services");
        }

        public IActionResult Index(string id)
        {
            @ViewBag.Id = id;
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}