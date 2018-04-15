using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xManik.EF;
using xManik.Models;
using xManik.Interfaces;

namespace xManik.Repositories
{
    public class ChanelRepository : IRepository<Chanel>
    {
        private readonly ApplicationDbContext _context;

        public ChanelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Chanel> GetAll()
        {
            return _context.Chanels.Include(o => o.UserProfile);
        }

        public Chanel Find(string id)
        {
            return _context.Chanels.Find(id);
        }

        public void Create(Chanel item)
        {
            _context.Chanels.Add(item);
        }

        public Task CreateAsync(Chanel item)
        {
            return _context.Chanels.AddAsync(item);
        }

        public void Update(Chanel item)
        {
            _context.Update(item);
        }

        public IEnumerable<Chanel> GetAllWhere(Func<Chanel, Boolean> predicate)
        {
            return _context.Chanels.Include(o => o.UserProfile).Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            Chanel order = _context.Chanels.Find(id);
            if (order != null)
                _context.Chanels.Remove(order);
        }
               
        public async Task DeleteAsync(string id)
        {
            Chanel order = await _context.Chanels.FindAsync(id);
            if (order != null)
                _context.Chanels.Remove(order);
        }

        public Chanel SingleOrDefault(Func<Chanel, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(Chanel item)
        {
            _context.Chanels.Remove(item);
        }

        public bool Any(Func<Chanel, bool> predicate)
        {
            return _context.Chanels.Any(predicate);
        }

        public Chanel Find(Func<Chanel, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
