﻿using BBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BBS.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BBS.Services
{
    public class ReplyOperation : Operation<Reply>
    {
        private readonly BBSContext _dbContext;
        public ReplyOperation(BBSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override IEnumerable<Reply> TList(Expression<Func<Reply, bool>> predicate)
        {
            return _dbContext.Replys.Include(a => a.User).Where(predicate);
        }
    }
}
