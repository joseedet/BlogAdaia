using BlogAdaia.Models.Comments;
using MyBlog.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAdaia.Data.Repository
{
    public interface ICommentRepository : IRepository<SubComment>
    {
    }
}
