using BBS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BBS.Services
{
    public interface IOperation<T> where T : class
    {
        void Add(T model);
        void Delete(T model);
        void Update(T model);
        T GetById(string id);
        IEnumerable<T> TList();
        IEnumerable<T> TList(Expression<Func<T, bool>> predicate);
    }
}
