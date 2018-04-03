using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xManik.DAL.EF;
using xManik.DAL.Entities;
using xManik.DAL.Interfaces;

namespace xManik.DAL.Repositories
{
    public class ServiceRepository : IRepository<Service>
    {
        private ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Service> GetAll()
        {
            return _context.Services;
        }

        public Service Get(int id)
        {
            return _context.Services.Find(id);
        }

        public void Create(Service book)
        {
            _context.Services.Add(book);
        }

        public void Update(Service book)
        {
            _context.Services.Update(book);
        }

        public IEnumerable<Service> Find(Func<Service, Boolean> predicate)
        {
            return _context.Services.Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            Service book = _context.Services.Find(id);
            if (book != null)
                _context.Services.Remove(book);
        }

        public void Remove(Service item)
        {
            _context.Services.Remove(item);
        }

        public async Task CreateAsync(Service item)
        {
            await _context.Services.AddAsync(item);
        }

        public async Task DeleteAsync(string id)
        {
            Service book = await _context.Services.FindAsync(id);
            if (book != null)
                _context.Services.Remove(book);
        }

        public Task<Service> SingleOrDefaultAsync(Func<Service, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Any(Func<Service, bool> predicate)
        {
            return _context.Services.Any(predicate);
        }
    }
}
