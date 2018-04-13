using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xManik.EF;
using xManik.Models;
using xManik.Interfaces;

namespace xManik.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.Include(o => o.Service);
        }

        public Order Find(string id)
        {
            return _context.Orders.Find(id);
        }

        public void Create(Order item)
        {
            _context.Orders.Add(item);
        }

        public Task CreateAsync(Order item)
        {
            return _context.Orders.AddAsync(item);
        }

        public void Update(Order item)
        {
            _context.Update(item);
        }

        public IEnumerable<Order> Find(Func<Order, Boolean> predicate)
        {
            return _context.Orders.Include(o => o.Service).Where(predicate).ToList();
        }

        public void Delete(string id)
        {
            Order order = _context.Orders.Find(id);
            if (order != null)
                _context.Orders.Remove(order);
        }
               
        public async Task DeleteAsync(string id)
        {
            Order order = await _context.Orders.FindAsync(id);
            if (order != null)
                _context.Orders.Remove(order);
        }

        public Order SingleOrDefault(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(Order item)
        {
            _context.Orders.Remove(item);
        }

        public bool Any(Func<Order, bool> predicate)
        {
            return _context.Orders.Any(predicate);
        }
    }
}
