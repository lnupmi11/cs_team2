using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xBlogger.EF;
using xBlogger.Interfaces;
using xBlogger.Models;

namespace xBlogger.Repositories
{
    public class NewsRepository : IRepository<News>
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Any(Func<News, bool> predicate)
        {
            return _context.News.Any(predicate);
        }

        public void Create(News item)
        {
            _context.News.Add(item);
        }

        public Task CreateAsync(News item)
        {
            return _context.News.AddAsync(item);
        }

        public void Delete(string id)
        {
           News news = _context.News.Find(id);
            if (news != null)
                _context.News.Remove(news);
        }

        public async Task DeleteAsync(string id)
        {
            News news = await _context.News.FindAsync(id);
            if (news != null)
                _context.News.Remove(news);
        }

        public News Find(string id)
        {
            return _context.News.Find(id);
        }

        public News Find(Func<News, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetAll()
        {
            return _context.News;
        }

        public IEnumerable<News> GetAllByIds(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetAllWhere(Func<News, bool> predicate)
        {
            return _context.News.Where(predicate);
        }

        public void Remove(News item)
        {
            _context.News.Remove(item);
        }

        public News SingleOrDefault(Func<News, bool> predicate)
        {
            return _context.News.SingleOrDefault(predicate);
        }

        public void Update(News item)
        {
            _context.Update(item);
        }
    }
}
