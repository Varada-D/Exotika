using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        // T-class eg: Employee
        T GetFirstOrDefault(Expression<Func<T,bool>> filter, string? includeProperties=null, bool tracked = true);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null,  string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);

        // For removing multiple entities
        void RemoveRange(IEnumerable<T> entity);
    }
}
