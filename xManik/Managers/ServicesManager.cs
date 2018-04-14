using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xManik.Models;
using xManik.Repositories;

namespace xManik.Managers
{
    public class ServicesManager<TService> : IDisposable where TService : class
    {
        private readonly WorkContext _context;

        public ServicesManager(WorkContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Service service)
        {
            _context.Services.Create(service);
            await _context.SaveAsync();
        }

        public Service Find(string id)
        {
            return _context.Services.Find(id);
        }

        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveAsync();
        }

        public async Task RemoveAsync(Service service)
        {
            _context.Services.Remove(service);
            await _context.SaveAsync();
        }

        public bool IsServiceExists(string id)
        {
            return _context.Services.Any(e => e.Id == id);
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
