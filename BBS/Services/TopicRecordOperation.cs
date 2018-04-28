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
    public class TopicRecordOperation : Operation<TopicRecord>, ITopicRecordOperation
    {
        private readonly BBSContext _dbContext;
        public TopicRecordOperation(BBSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override TopicRecord GetById(string id)
        {
            return _dbContext.TopicRecords.Include(a => a.User).Include(a => a.Topic).FirstOrDefault(a => a.TopicRecordId == id);
        }

        public override IEnumerable<TopicRecord> TList()
        {
            return _dbContext.TopicRecords.Include(a => a.User).Include(a => a.Topic);
        }

        public override IEnumerable<TopicRecord> TList(Expression<Func<TopicRecord, bool>> predicate)
        {
            return _dbContext.TopicRecords.Include(a => a.User).Include(a => a.Topic).Where(predicate);
        }
    }
}
