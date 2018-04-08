using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xManik.DAL.Entities;

namespace xManik.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Service> Services { get; }
        IRepository<Order> Orders { get; }
        IRepository<PortfolioItem> PortfolioItems { get; }
        IRepository<Comment> Comments { get; }
        Task SaveAsync();
    }
}
