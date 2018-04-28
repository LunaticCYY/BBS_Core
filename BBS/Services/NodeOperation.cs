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
    public class NodeOperation : Operation<Node>, INodeOperation
    {
        private readonly BBSContext _dbContext;
        public NodeOperation(BBSContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override Node GetById(string id)
        {
            return _dbContext.Nodes.Include(a => a.User).FirstOrDefault(a => a.NodeId == id);
        }

        public override IEnumerable<Node> TList()
        {
            return _dbContext.Nodes.Include(a => a.User);
        }

        public override IEnumerable<Node> TList(Expression<Func<Node, bool>> predicate)
        {
            return _dbContext.Nodes.Include(a => a.User).Where(predicate);
        }
    }
}
