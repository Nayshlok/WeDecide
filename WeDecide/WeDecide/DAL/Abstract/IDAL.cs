using System;
using System.Collections.Generic;
using System.Linq;

namespace WeDecide.DAL.Abstract
{
    public interface IDAL<T>
    {
        bool Create(T entity);
        T Get(int id);
        T Delete(int id);
        T Update(int id, T entity);
        IEnumerable<T> GetAll(Func<T, bool> predicate);
    }
}
