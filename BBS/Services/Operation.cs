using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BBS.Data;
using Microsoft.EntityFrameworkCore;

namespace BBS.Services
{
    public class Operation<T> : IOperation<T> where T : class
    {
        private readonly BBSContext _dbContext;

        public Operation(BBSContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T model)
        {
            _dbContext.Set<T>().Add(model);
            _dbContext.SaveChanges();
        }

        public void Delete(T model)
        {
            _dbContext.Set<T>().Remove(model);
            _dbContext.SaveChanges();
        }

        public void Update(T model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public virtual T GetById(string id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> TList()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public virtual IEnumerable<T> TList(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AsEnumerable();
        }
    }
}
