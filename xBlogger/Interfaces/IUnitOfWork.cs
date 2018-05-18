using System;
using System.Threading.Tasks;
using xBlogger.Models;

namespace xBlogger.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Assigment> Assigments { get; }
        IRepository<Chanel> Chanels { get; }
        IRepository<UserProfile> UserProfiles { get; }
        IRepository<Deal> Deals { get; }
        Task SaveAsync();
    }
}
