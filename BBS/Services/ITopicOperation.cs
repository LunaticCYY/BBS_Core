using BBS.Models;
using System;
using System.Linq.Expressions;

namespace BBS.Services
{
    public interface ITopicOperation : IOperation<Topic>
    {
        Page<Topic> PageList(int pageSize, int pageIndex);
        Page<Topic> PageList(Expression<Func<Topic, bool>> predicate, int pageSize, int pageIndex);
    }
}
