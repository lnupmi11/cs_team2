using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xManik.EF;
using xManik.Models;
using xManik.Interfaces;

namespace xManik.Repositories
{
    public class UserUserProfileRepository : IRepository<UserUserProfile>
    {
        private readonly ApplicationDbContext _context;

        public UserUserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Any(Func<UserUserProfile, bool> predicate)
        {
            return _context.UserUserProfiles.Any(predicate);
        }

        public void Create(UserUserProfile item)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(UserUserProfile item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserUserProfile> Find(Func<UserUserProfile, bool> predicate)
        {
            return _context.UserUserProfiles.Where(predicate).ToList();
        }

        public UserUserProfile Find(string id)
        {
            return _context.UserUserProfiles.Find(id);
        }

        public IEnumerable<UserUserProfile> GetAll()
        {
            return _context.UserUserProfiles;
        }

        public void Remove(UserUserProfile item)
        {
            throw new NotImplementedException();
        }

        public UserUserProfile SingleOrDefault(Func<UserUserProfile, bool> predicate)
        {
            return _context.UserUserProfiles.SingleOrDefault(predicate);
        }

        public void Update(UserUserProfile item)
        {
            _context.UserUserProfiles.Update(item);
        }
    }
}
