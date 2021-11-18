using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Omega.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll([Optional] bool LazyLoadingEnabled);
        List<T> FindWhere(Expression<Func<T, bool>> predicate, List<string> dependencies = null);
        T FindSingle(Expression<Func<T, bool>> predicate, List<string> dependencies = null);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        List<T> AddRange(List<T> entities);
        Task<List<T>> AddRangeAsync(List<T> entities);
        T Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        T Update(T entity);
        void SaveChanges();
        Task SaveChangesAsync();
        List<long> GetIds(Expression<Func<T, bool>> predicate);
        long GetCount(Expression<Func<T, bool>> predicate);
        long GetId(Expression<Func<T, bool>> predicate);
        //Expression<Func<T, bool>> GetPredicateFromFindExpression(FindExpression findExpression);
        T LastOrDefault(Expression<Func<T, bool>> predicate, [Optional] bool lazyLoadingEnabled);
    }
}
