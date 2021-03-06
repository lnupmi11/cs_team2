﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xBlogger.Interfaces
{

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Find(string id);
        IEnumerable<T> GetAllWhere(Func<T, Boolean> predicate);
        IEnumerable<T> GetAllByIds(IEnumerable<string> ids);
        T Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(string id);
        void Remove(T item);
        bool Any(Func<T, Boolean> predicate);
        Task CreateAsync(T item);
        Task DeleteAsync(string id);
        T SingleOrDefault(Func<T, Boolean> predicate);
    }

}
