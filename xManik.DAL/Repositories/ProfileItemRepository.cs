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
    public class PortfolioItemRepository : IRepository<PortfolioItem>
    {
        private ApplicationContext db;

        public PortfolioItemRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<PortfolioItem> GetAll()
        {
            return db.PortfolioItems;
        }

        public PortfolioItem Get(int id)
        {
            return db.PortfolioItems.Find(id);
        }

        public void Create(PortfolioItem order)
        {
            db.PortfolioItems.Add(order);
        }

        public void Update(PortfolioItem order)
        {
            db.Entry(order).State = EntityState.Modified;
        }
        public IEnumerable<PortfolioItem> Find(Func<PortfolioItem, Boolean> predicate)
        {
            return db.PortfolioItems.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            PortfolioItem order = db.PortfolioItems.Find(id);
            if (order != null)
                db.PortfolioItems.Remove(order);
        }
    }
}
