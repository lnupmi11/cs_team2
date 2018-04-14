using System;
using System.Threading.Tasks;
using xManik.Models;

namespace xManik.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Service> Services { get; }
        IRepository<Order> Orders { get; }
        IRepository<PortfolioItem> PortfolioItems { get; }
        IRepository<Comment> Comments { get; }
        IRepository<UserUserProfile> UserUserProfiles { get; }
        Task SaveAsync();
    }
}
