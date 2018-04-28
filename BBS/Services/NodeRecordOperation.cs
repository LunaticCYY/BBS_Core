using BBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBS.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BBS.Services
{
    public class NodeRecordOperation : Operation<NodeRecord>, INodeRecordOperation
    {
        private readonly BBSContext _dbContext;
        public NodeRecordOperation(BBSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override NodeRecord GetById(string id)
        {
            return _dbContext.NodeRecords.Include(a => a.User).Include(a => a.Node).FirstOrDefault(a => a.NodeRecordId == id);
        }

        public override IEnumerable<NodeRecord> TList()
        {
            return _dbContext.NodeRecords.Include(a => a.User).Include(a => a.Node);
        }

        public override IEnumerable<NodeRecord> TList(Expression<Func<NodeRecord, bool>> predicate)
        {
            return _dbContext.NodeRecords.Include(a => a.User).Include(a => a.Node).Where(predicate);
        }
    }
}
