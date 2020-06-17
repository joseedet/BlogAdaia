using BlogAdaia.Data;
using BlogAdaia.Data.Repository;
using BlogAdaia.Models;
using BlogAdaia.Models.Comments;
using BlogAdaia.ViewModels;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {

        private AppDbContext _context;
        public PostRepository(AppDbContext context) : base(context)
        {


            _context = context;
        }

        public Post GetByIds(int id)
        {
            return _context.Posts
                  .Include(p => p.MainComments)
                  .ThenInclude(p => p.SubComments)
                  .FirstOrDefault(p => p.Id == id);
        }
        /*public IQueryable<Post> GetAll(string category)
        {
            //Func<Post>,bool> InCategory=(post)=> { return post.Category.ToLower();
            // return _context.Set<post>().AsNoTracking();
            { }
            return _context.Posts.Where(c => c.Category.Contains(category));
        }*/

        List<Post> IPostRepository.GetAll(string category)
        {
            return _context.Posts.Where(c => c.Tags.Contains(category)).ToList();
        }
        public new async Task<Post> GetById(int id)
        {
            return _context.Posts
                .Include(p => p.MainComments)
                .ThenInclude(mc => mc.SubComments)
                .FirstOrDefault(p => p.Id == id);

        }



        public IndexViewModel GetAllPosts(int pageNumber)
        {
            int pagesize = 1;
            int skipAmount = pagesize * (pageNumber - 1);
            int postCount = _context.Posts.Count();

            if(skipAmount<1)
            {
                skipAmount = 0;
            }
            return new IndexViewModel
            {
                PageNumber = pageNumber,
                NextPage = postCount > skipAmount + pagesize,

                Posts = _context
                .Posts.
                Skip(skipAmount)
                .Take(pagesize)
                .ToList()
            };


        }

        public IndexViewModel GetAllPosts(int pageNumber, string category)
        {
            Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };

            int pagesize = 1;
            int skipAmount = pagesize * (pageNumber - 1);


            var query = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(category))

                query.Where(x => InCategory(x));
                int postCount= query.Count();



            return new IndexViewModel
            {
                PageNumber = pageNumber,
                NextPage = postCount > skipAmount + pagesize,
                Catgory=category,
                Posts = query
                    .Skip(skipAmount)
                    .Take(pagesize)
                    .ToList()

            };


        }    






        /*IQueryable<IndexViewModel> IPostRepository.GetAll(int pageNumber)
        {
            int pagesize = 1;
            int skipAmount = pagesize * (pageNumber - 1);
            int postCount = _context.Posts.Count();
            int capacity = (skipAmount + pagesize);

            bool canGoNext = postCount > capacity;

            //return  new IndexViewModel {
            var indexModel= _context.Posts.Skip(skipAmount).Take(pagesize).ToList() ;

            return new IndexViewModel { Pl}

             //.Skip(skipAmount)
             //.Take(pagesize)

              }*/
    }

    }


     
    
 