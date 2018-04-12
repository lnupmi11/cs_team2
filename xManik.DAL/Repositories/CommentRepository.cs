using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xManik.DAL.EF;
using xManik.DAL.Entities;
using xManik.DAL.Interfaces;

namespace xManik.DAL.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _context.Comments.Include(o => o.Author).Include(o => o.Recipent);
        }

        public Comment Find(string id)
        {
            return _context.Comments.Find(id);
        }

        public void Create(Comment item)
        {
            _context.Comments.Add(item);
        }

        public Task CreateAsync(Comment item)
        {
            return _context.Comments.AddAsync(item);
        }

        public void Update(Comment item)
        {
            _context.Update(item);
        }

        public IEnumerable<Comment> Find(Func<Comment, Boolean> predicate)
        {
            return _context.Comments.Include(o => o.Author).Include(o => o.Recipent).Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            Comment order = _context.Comments.Find(id);
            if (order != null)
                _context.Comments.Remove(order);
        }

        public async Task DeleteAsync(string id)
        {
            Comment order = await _context.Comments.FindAsync(id);
            if (order != null)
                _context.Comments.Remove(order);
        }

        public Comment SingleOrDefault(Func<Comment, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(Comment item)
        {
            _context.Comments.Remove(item);
        }

        public bool Any(Func<Comment, bool> predicate)
        {
            return _context.Comments.Any(predicate);
        }
    }
}
