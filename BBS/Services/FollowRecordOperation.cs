using BBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBS.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BBS.Services
{
    public class FollowRecordOperation : Operation<FollowRecord>, IFollowRecordOperation
    {
        private readonly BBSContext _dbContext;
        public FollowRecordOperation(BBSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override FollowRecord GetById(string id)
        {
            return _dbContext.FollowRecords.Include(a => a.User).Include(a => a.FollowUser).FirstOrDefault(a => a.FollowRecordId == id);
        }

        public override IEnumerable<FollowRecord> TList()
        {
            return _dbContext.FollowRecords.Include(a => a.User).Include(a => a.FollowUser);
        }

        public override IEnumerable<FollowRecord> TList(Expression<Func<FollowRecord, bool>> predicate)
        {
            return _dbContext.FollowRecords.Include(a => a.User).Include(a => a.FollowUser).Where(predicate);
        }
    }
}
