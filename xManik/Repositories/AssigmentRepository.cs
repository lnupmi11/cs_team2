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
    public class AssigmentRepository : IRepository<Assigment>
    {
        private readonly ApplicationDbContext _context;

        public AssigmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Assigment> GetAll()
        {
            return _context.Assigments;
        }

        public Assigment Find(string id)
        {
            return _context.Assigments.Find(id);
        }

        public void Create(Assigment book)
        {
            _context.Assigments.Add(book);
        }

        public void Update(Assigment book)
        {
            _context.Assigments.Update(book);
        }

        public IEnumerable<Assigment> GetAllWhere(Func<Assigment, Boolean> predicate)
        {
            return _context.Assigments.Where(predicate);
        }

        public Assigment Find(Func<Assigment, bool> predicate)
        {
            return _context.Assigments.Include(p => p.UserProfile).FirstOrDefault();
        }

        public void Delete(string id)
        {
            Assigment book = _context.Assigments.Find(id);
            if (book != null)
                _context.Assigments.Remove(book);
        }

        public void Remove(Assigment item)
        {
            _context.Assigments.Remove(item);
        }

        public async Task CreateAsync(Assigment item)
        {
            await _context.Assigments.AddAsync(item);
        }

        public async Task DeleteAsync(string id)
        {
            Assigment book = await _context.Assigments.FindAsync(id);
            if (book != null)
                _context.Assigments.Remove(book);
        }

        public Assigment SingleOrDefault(Func<Assigment, bool> predicate)
        {
            return _context.Assigments.SingleOrDefault(predicate);
        }

        public bool Any(Func<Assigment, bool> predicate)
        {
            return _context.Assigments.Any(predicate);
        }
    }
}
