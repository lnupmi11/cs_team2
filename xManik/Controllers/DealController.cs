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
                throw new ArgumentNullException("Null id");
            }
            var userId = _userProfileManager.GetUserProfileId(User);
            var assigment = _context.Assigments.Find(id);
            if(assigment == null)
            {
                throw new EntryPointNotFoundException("No assgiments found with such id " + assigment.AssigmentId);
            }
            Deal deal = new Deal
            {
                AssigmentId = id,
                BloggerId = userId,
                ClientId = assigment.UserProfileId
            };

            await _dealsManager.AddDealAsync(deal);
        }
    }
}