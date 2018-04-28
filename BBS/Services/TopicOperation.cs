using BBS.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using BBS.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BBS.Services
{
    public class TopicOperation : Operation<Topic>, ITopicOperation
    {
        private readonly BBSContext _dbContext;
        public TopicOperation(BBSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override Topic GetById(string id)
        {
            return _dbContext.Topics.Include(a => a.User).Include(a => a.Node).Include(a => a.LastReplyUser).FirstOrDefault(a => a.TopicId == id);
        }

        public override IEnumerable<Topic> TList()
        {
            return _dbContext.Topics.Include(a => a.User).Include(a => a.Node).Include(a => a.LastReplyUser);
        }

        public override IEnumerable<Topic> TList(Expression<Func<Topic, bool>> predicate)
        {
            return _dbContext.Topics.Include(a => a.User).Include(a => a.Node).Include(a => a.LastReplyUser).Where(predicate);
        }

        public Page<Topic> PageList(int pageSize = 50, int pageIndex = 1)
        {
            return PageList(null, pageSize, pageIndex);
        }

        public Page<Topic> PageList(Expression<Func<Topic, bool>> predicate, int pageSize, int pageIndex)
        {
            var topics = _dbContext.Topics.Include(a => a.User).Include(a => a.Node).Include(a => a.LastReplyUser).AsQueryable().AsNoTracking();
            if (predicate != null)
            {
                topics = topics.Where(predicate);
            }

            var count = topics.Count();
            topics = topics.OrderByDescending(a => a.AddTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new Page<Topic>(topics.ToList(), pageSize, count);
        }
    }
}
