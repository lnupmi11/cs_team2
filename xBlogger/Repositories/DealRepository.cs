﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xBlogger.EF;
using xBlogger.Interfaces;
using xBlogger.Models;
using Microsoft.EntityFrameworkCore;

namespace xBlogger.Repositories
{
    public class DealRepository: IRepository<Deal>
    {
            private readonly ApplicationDbContext _context;

            public DealRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Deal> GetAll()
            {
                return _context.Deals.Include(o => o.Assigment).Include(o => o.Chanel);
            }

            public Deal Find(string id)
            {
                return _context.Deals.Find(id);
            }

            public void Create(Deal book)
            {
                _context.Deals.Add(book);
            }

            public void Update(Deal book)
            {
                _context.Deals.Update(book);
            }

            public IEnumerable<Deal> GetAllWhere(Func<Deal, Boolean> predicate)
            {
                return _context.Deals.Include(o => o.Assigment).Include(o => o.Chanel).Where(predicate);
            }

            public Deal Find(Func<Deal, bool> predicate)
            {
                return _context.Deals.Include(o => o.Assigment).Include(o => o.Chanel).FirstOrDefault();
            }

            public void Delete(string id)
            {
                Deal book = _context.Deals.Find(id);
                if (book != null)
                    _context.Deals.Remove(book);
            }

            public void Remove(Deal item)
            {
                _context.Deals.Remove(item);
            }

            public async Task CreateAsync(Deal item)
            {
                await _context.Deals.AddAsync(item);
            }

            public async Task DeleteAsync(string id)
            {
                Deal book = await _context.Deals.FindAsync(id);
                if (book != null)
                    _context.Deals.Remove(book);
            }

            public Deal SingleOrDefault(Func<Deal, bool> predicate)
            {
                return _context.Deals.Include(o => o.Assigment).Include(o => o.Chanel).SingleOrDefault(predicate);
            }

            public bool Any(Func<Deal, bool> predicate)
            {
                return _context.Deals.Include(o => o.Assigment).Include(o => o.Chanel).Any(predicate);
            }

            public IEnumerable<Deal> GetAllByIds(IEnumerable<string> ids)
            {
                throw new NotImplementedException();
            }
    }
}
