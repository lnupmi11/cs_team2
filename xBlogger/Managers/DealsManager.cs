﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using xBlogger.Extensions.IdentityExtensions;
using xBlogger.Models;
using xBlogger.Repositories;

namespace xBlogger.Managers
{
    public class DealsManager<TDeal>: IDisposable where TDeal : class
    {
        private readonly WorkContext _context;

        public DealsManager(WorkContext context)
        {
            _context = context;
        }

        public IEnumerable<Deal> GetUserDeals(string userId)
        {
            return _context.Deals.GetAllWhere(p => p.RecipientId == userId).OrderBy(o => !o.IsRead);
        }

        public async Task AddDealAsync(Deal deal)
        {
            await _context.Deals.CreateAsync(deal);
            await _context.SaveAsync();
        }

        public int GetUnreadDealsNum(ClaimsPrincipal principal)
        {
            return _context.Deals.GetAllWhere(o => o.RecipientId == principal.GetUserId()).Count();
        }


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _context != null)
                {
                    _context.Dispose(true);
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
