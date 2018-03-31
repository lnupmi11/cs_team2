using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using xManik.DAL.EF;
using xManik.DAL.Entities;
using xManik.DAL.Interfaces;

namespace xManik.DAL.Repositories
{
    public class ServiceRepository : IRepository<Service>
    {
        private ApplicationContext db;

        public ServiceRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Service> GetAll()
        {
            return db.Services;
        }

        public Service Get(int id)
        {
            return db.Services.Find(id);
        }

        public void Create(Service book)
        {
            db.Services.Add(book);
        }

        public void Update(Service book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IEnumerable<Service> Find(Func<Service, Boolean> predicate)
        {
            return db.Services.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Service book = db.Services.Find(id);
            if (book != null)
                db.Services.Remove(book);
        }
    }
}
