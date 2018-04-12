﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xManik.DAL.EF;
using xManik.DAL.Entities;
using xManik.DAL.Interfaces;

namespace xManik.DAL.Repositories
{
    public class PortfolioItemRepository : IRepository<PortfolioItem>
    {
        private readonly ApplicationDbContext _cotnext;

        public PortfolioItemRepository(ApplicationDbContext context)
        {
            _cotnext = context;
        }

        public bool Any(Func<PortfolioItem, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Create(PortfolioItem item)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(PortfolioItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PortfolioItem> Find(Func<PortfolioItem, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public PortfolioItem Find(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PortfolioItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(PortfolioItem item)
        {
            throw new NotImplementedException();
        }

        public PortfolioItem SingleOrDefault(Func<PortfolioItem, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(PortfolioItem item)
        {
            throw new NotImplementedException();
        }
    }
}
