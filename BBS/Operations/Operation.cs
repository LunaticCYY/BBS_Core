using BBS.Interfaces;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Semantics;
using BBS.Models;
using System.Linq.Expressions;
using BBS.Data;
using Microsoft.EntityFrameworkCore;

namespace BBS.Operations
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

        public void Edit(T model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public virtual T GetById(string id)
        {
            throw new NotImplementedException();
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
