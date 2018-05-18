﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using xBlogger.EF;
using xBlogger.Managers;
using xBlogger.Models;
using xBlogger.Repositories;

namespace xBlogger.Controllers
{
    [Authorize]
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
        
        public IActionResult AllDeals()
        {
            var userId = _userProfileManager.GetUserProfileId(User);
            var userDeals = _dealsManager.GetUserDeals(userId);

            return View(userDeals);
        }

        public async Task<IActionResult> AddDeal(string id, string role)
        {
            if (id == null)
            {
                NotFound();
            }
            var userId = _userProfileManager.GetUserProfileId(User);

            Deal deal = null;

            switch(role)
            {
                case "Blogger":
                    var assigment = _context.Assigments.Find(id);
                    if (assigment == null)
                    {
                        NotFound();
                    }
                    deal = new Deal
                    {
                        AssigmentId = id,
                        SenderId = userId,
                        RecipientId = assigment.UserProfileId
                    };
                    break;
                case "Client":
                    var chanel = _context.Chanels.Find(id);
                    if (chanel == null)
                    {
                        NotFound();
                    }
                    deal = new Deal
                    {
                        ChanelId = id,
                        SenderId = userId,
                        RecipientId = chanel.UserProfileId
                    };
                    break;
                default:
                    throw new Exception("Unknown role");
            }

            await _dealsManager.AddDealAsync(deal);

            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                NotFound();
            }

            var deal = _context.Deals.SingleOrDefault(o => o.DealId == id);
            if (deal == null)
            {
                NotFound();
            }

            deal.IsRead = true;
            _context.Deals.Update(deal);
            await _context.SaveAsync();
            return View(deal);
        }

        public async Task<IActionResult> ConfirmDeal(string id)
        {
            if (id == null)
            {
                NotFound();
            }

            var deal = _context.Deals.SingleOrDefault(o => o.DealId == id);
            if (deal == null)
            {
                NotFound();
            }

            deal.IsConfirmed = true;
            _context.Deals.Update(deal);
            await _context.SaveAsync();

            return View();
        }

        public async Task<IActionResult> DeclineDeal(string id)
        {
            if (id == null)
            {
                NotFound();
            }

            var deal = _context.Deals.SingleOrDefault(o => o.DealId == id);
            if (deal == null)
            {
                NotFound();
            }

            deal.IsConfirmed = false;
            _context.Deals.Update(deal);
            await _context.SaveAsync();

            return View();
        }
    }
}