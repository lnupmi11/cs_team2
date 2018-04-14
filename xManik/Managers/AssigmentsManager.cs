using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xManik.Models;
using xManik.Repositories;

namespace xManik.Managers
{
    public class AssigmentsManager<TAssigment> : IDisposable where TAssigment : class
    {
        private readonly WorkContext _context;

        public AssigmentsManager(WorkContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Assigment service)
        {
            _context.Assigments.Create(service);
            await _context.SaveAsync();
        }

        public Assigment Find(string id)
        {
            return _context.Assigments.Find(id);
        }

        public async Task UpdateAsync(Assigment service)
        {
            _context.Assigments.Update(service);
            await _context.SaveAsync();
        }

        public async Task RemoveAsync(Assigment service)
        {
            _context.Assigments.Remove(service);
            await _context.SaveAsync();
        }

        public bool IsAssigmentExists(string id)
        {
            return _context.Assigments.Any(e => e.AssigmentId == id);
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
