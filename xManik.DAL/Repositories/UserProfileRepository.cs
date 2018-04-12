﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xManik.DAL.EF;
using xManik.DAL.Entities;
using xManik.DAL.Interfaces;

namespace xManik.DAL.Repositories
{
    public class UserProfileRepository : IRepository<UserProfile>
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Any(Func<UserProfile, bool> predicate)
        {
            return _context.UserProfiles.Any(predicate);
        }

        public void Create(UserProfile item)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(UserProfile item)
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

        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return _context.UserProfiles.Where(predicate).ToList();
        }

        public UserProfile Find(int id)
        {
            return _context.UserProfiles.Find(id);
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return _context.UserProfiles;
        }

        public void Remove(UserProfile item)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile> SingleOrDefaultAsync(Func<UserProfile, bool> predicate)
        {
            return SingleOrDefaultAsync(predicate);
        }

        public void Update(UserProfile item)
        {
            _context.UserProfiles.Update(item);
        }
    }
}