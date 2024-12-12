using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccess.Infrastructure.IRepository
{

    // this interface contains the function that controller is performing.
    public interface IRepository<T>
    {
        // getting all records
         IEnumerable<T> GetAll();

        //finding 
        T GetT(Expression<Func<T, bool>> predicate);

        // inserting 
        void Add(T entity);

        //deleting 
        void Del(T entity);

        //deleting by range
        void DeleteRange(IEnumerable<T> entity);

    }
}
