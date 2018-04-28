using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using xManik.EF;
using xManik.Managers;
using xManik.Models;
using xManik.Repositories;

namespace xManik.Controllers
{
    public class DealController : Controller
    {
        private readonly WorkContext _context;
        private readonly UserProfileManager<UserProfile> _userProfileManager;
        private readonly DealsManager<Deal> _dealsManager;

        public DealController(ApplicationDbContext context)
        {
            _context = new WorkContext(context);
            _userProfileManager = new UserProfileManager<UserProfile>(_context);
            _dealsManager = new DealsManager<Deal>(_context);
        }

        public IActionResult Index()
        {
            var userId = _userProfileManager.GetUserProfileId(User);
            var userDeals = _dealsManager.GetUserDeals(userId);
            return View(userDeals);
        }

        [Authorize(Roles="Blogger")]
        public async Task AddDeal(string id)
        {
            if(id == null)
            {
                NotFound();
            }
            var userId = _userProfileManager.GetUserProfileId(User);
            var assigment = _context.Assigments.Find(id);
            if(assigment == null)
            {
                NotFound();
            }
            Deal deal = new Deal
            {
                AssigmentId = id,
                BloggerId = userId,
                ClientId = assigment.UserProfileId,
                IsReadByBlogger = true
            };

            await _dealsManager.AddDealAsync(deal);

            Redirect("Index");
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                NotFound();
            }

            var deal = _context.Deals.Find(id);

            if (deal == null)
            {
                NotFound();
            }

            //var assigment = _context.Assigments.Find(deal.AssigmentId);
            //var ClientProfile = _context.UserProfiles.Find(deal.ClientId);
            //var BloggerProfile = _context.UserProfiles.Find(deal.BloggerId);

            deal.IsReadByClient = true;

            _context.Deals.Update(deal);

            return View(deal);
        }
    }
}