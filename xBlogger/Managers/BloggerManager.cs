﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xBlogger.Models;
using xBlogger.Models.BloggerViewModels;
using xBlogger.Repositories;

namespace xBlogger.Managers
{
    public class BloggerManager<TBlogger> : IDisposable where TBlogger : class
    {
        private readonly WorkContext _context;

        public BloggerManager(WorkContext context)
        {
            _context = context;
        }

        //public IEnumerable<BloggerViewModel> GetAllBloggers()
        //{
        //    IEnumerable<BloggerViewModel> bloggers = new IEnumerable<BloggerViewModel>();
        //}
          
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
