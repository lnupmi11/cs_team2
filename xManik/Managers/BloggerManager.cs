using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xManik.Models.BloggerViewModels;
using xManik.Repositories;

namespace xManik.Managers
{
    public class BloggerManager<TBlogger> : IDisposable where TBlogger : class
    {
        private readonly WorkContext _context;

        public BloggerManager(WorkContext context)
        {
            _context = context;
        }

        public IEnumerable<BloggerViewModels> GetAllBloggers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BloggerViewModels> GetAllClients()
        {
            throw new NotImplementedException();
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
