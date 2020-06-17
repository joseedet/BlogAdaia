using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAdaia.Data.Repository;
using BlogAdaia.Models;
using BlogAdaia.ViewModels;

using MyBlog.Data.Repository;

namespace BlogAdaia.Data.Repository
{
   
          public interface IPostRepository : IRepository<Post>
            {
                List<Post> GetAll(string category);
        // new IQueryable IndexViewModel GetAll(int pageNumber);
                IndexViewModel GetAllPosts(int pageNumber);
                IndexViewModel GetAllPosts(int pageNumber,string category);

    }
    

}
