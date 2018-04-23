using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xManik.Models;
using xManik.Repositories;

namespace xManik.Managers
{
    public class NewsManager : IDisposable
    {
        private readonly WorkContext _context;

        public NewsManager(WorkContext context)
        {
            _context = context;
        }

        public IEnumerable<News> GetAll()
        {
            return _context.News.GetAll();
        }

        public async Task CreateAsync(News news)
        {
            news.DatePublished = DateTime.Now;
            _context.News.Create(news);
            await _context.SaveAsync();
        }

        public News Find(string id)
        {
            return _context.News.Find(id);
        }

        public async Task UpdateAsync(News news)
        {
            _context.News.Update(news);
            await _context.SaveAsync();
        }

        public async Task RemoveAsync(News news)
        {
            _context.News.Remove(news);
            await _context.SaveAsync();
        }

        public async Task RemoveAsync(string id)
        {
            _context.News.Delete(id);
            await _context.SaveAsync();
        }

        public bool IsNewsExists(string id)
        {
            return _context.News.Any(e => e.Id == id);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _context != null)
                {
                    _context.Dispose(true);
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
