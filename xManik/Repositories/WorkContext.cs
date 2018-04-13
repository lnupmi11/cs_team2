using System;
using System.Threading.Tasks;
using xManik.DAL.EF;
using xManik.DAL.Entities;
using xManik.DAL.Interfaces;

namespace xManik.DAL.Repositories
{
    public class WorkContext : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private ServiceRepository _serviceRepository;
        private OrderRepository _orderRepository;
        private PortfolioItemRepository _portfolioItemRepository;
        private CommentRepository _commentRepository;
        private UserProfileRepository _userProfileRepositiry;

        public WorkContext(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Service> Services
        {
            get
            {
                if (_serviceRepository == null)
                    _serviceRepository = new ServiceRepository(_context);
                return _serviceRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_context);
                return _orderRepository;
            }
        }

        public IRepository<PortfolioItem> PortfolioItems
        {
            get
            {
                if (_portfolioItemRepository == null)
                    _portfolioItemRepository = new PortfolioItemRepository(_context);
                return _portfolioItemRepository;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_context);
                return _commentRepository;
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
