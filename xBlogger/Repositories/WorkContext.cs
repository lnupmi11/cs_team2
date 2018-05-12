using System;
using System.Threading.Tasks;
using xBlogger.EF;
using xBlogger.Models;
using xBlogger.Interfaces;

namespace xBlogger.Repositories
{
    public class WorkContext : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private AssigmentRepository _serviceRepository;
        private ChanelRepository _orderRepository;
        private UserProfileRepository _userProfileRepositiry;
        private NewsRepository _newsRepository;
        private DealRepository _dealsRepository;

        public WorkContext(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Assigment> Assigments
        {
            get
            {
                if (_serviceRepository == null)
                    _serviceRepository = new AssigmentRepository(_context);
                return _serviceRepository;
            }
        }

        public IRepository<Chanel> Chanels
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new ChanelRepository(_context);
                return _orderRepository;
            }
        }

        public IRepository<UserProfile> UserProfiles
        {
            get
            {
                if (_userProfileRepositiry == null)
                    _userProfileRepositiry = new UserProfileRepository(_context);
                return _userProfileRepositiry;
            }
        }

        public IRepository<News> News
        {
            get
            {
                if (_newsRepository == null)
                    _newsRepository = new NewsRepository(_context);
                return _newsRepository;
             }
         }
        public IRepository<Deal> Deals
        {
            get
            {
                if (_dealsRepository == null)
                    _dealsRepository = new DealRepository(_context);
                return _dealsRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
