using System;
using System.Collections.Generic;
using System.Text;
using xManik.DAL.EF;
using xManik.DAL.Entities;
using xManik.DAL.Interfaces;

namespace xManik.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;
        private ServiceRepository serviceRepository;
        private OrderRepository orderRepository;
        private PortfolioItemRepository portfolioItemRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
        }
        public IRepository<Service> Services
        {
            get
            {
                if (serviceRepository == null)
                    serviceRepository = new ServiceRepository(db);
                return serviceRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public IRepository<PortfolioItem> PortfolioItems
        {
            get
            {
                if (portfolioItemRepository == null)
                    portfolioItemRepository = new PortfolioItemRepository(db);
                return portfolioItemRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
