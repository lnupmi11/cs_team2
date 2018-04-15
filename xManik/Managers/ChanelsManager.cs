using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using xManik.Models;
using xManik.Repositories;

namespace xManik.Managers
{
    public class ChanelsManager<TChanel> : IDisposable where TChanel : class
    {
        private readonly WorkContext _context;

        public ChanelsManager(WorkContext context)
        {
            _context = context;
        }

        public IEnumerable<Chanel> GetAll()
        {
            return _context.Chanels.GetAll();
        }

        public async Task CreateAsync(Chanel service)
        {
            _context.Chanels.Create(service);
            await _context.SaveAsync();
        }

        public Chanel Find(string id)
        {
            return _context.Chanels.Find(id);
        }

        public async Task UpdateAsync(Chanel service)
        {
            _context.Chanels.Update(service);
            await _context.SaveAsync();
        }

        public async Task RemoveAsync(Chanel service)
        {
            _context.Chanels.Remove(service);
            await _context.SaveAsync();
        }

        public bool IsChanelExists(string id)
        {
            return _context.Chanels.Any(e => e.ChanelId == id);
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
