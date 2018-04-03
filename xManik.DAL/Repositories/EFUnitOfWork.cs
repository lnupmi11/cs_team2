using System;
using System.Threading.Tasks;
using xManik.DAL.EF;
using xManik.DAL.Entities;
using xManik.DAL.Interfaces;

namespace xManik.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        private ServiceRepository _serviceRepository;
        private OrderRepository _orderRepository;
        private PortfolioItemRepository _portfolioItemRepository;

        public EFUnitOfWork(ApplicationDbContext context)
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

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                   _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
