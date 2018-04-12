using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace xManik.DAL.Interfaces
{

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Find(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);      
        void Create(T item);
        void Update(T item);
        void Delete(string id);
        void Remove(T item);
        bool Any(Func<T, Boolean> predicate);
        Task CreateAsync(T item);
        Task DeleteAsync(string id);
        Task<T> SingleOrDefaultAsync(Func<T, Boolean> predicate);
    }

}
