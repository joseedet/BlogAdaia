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
            return await _context.Posts
                .Include(p => p.MainComments)
                .ThenInclude(mc => mc.SubComments)
                .FirstOrDefaultAsync(p => p.Id == id);

        }



        public IndexViewModel GetAllPosts(int pageNumber)
        {
            int pagesize = 2;
            int skipAmount = pagesize * (pageNumber - 1);
            int postCount = _context.Posts.Count();

            if(skipAmount<1)
            {
                skipAmount = 0;
            }
            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PageCount = postCount / pagesize,
                NextPage = postCount > skipAmount + pagesize,
                Posts = _context
                .Posts.
                Skip(skipAmount)
                .Take(pagesize)
                .ToList()
            };


        }

        public  IndexViewModel GetAllPosts(int pageNumber, string category)
        {
            Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };

            int pageSize = 2;
            int skipAmount = pageSize * (pageNumber+1);


            var query = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(category))

                query.Where(x => InCategory(x));
                int postCount= query.Count();
                int pageCount=(int)Math.Ceiling((double)postCount / pageSize);


            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PageCount =pageCount ,
                NextPage = postCount > skipAmount + pageSize,
                Pages=PageNumbers(pageNumber,postCount),
                Catgory=category,
                Posts = query
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToList()

            };


        }    
        private IEnumerable<int> PageNumbers(int pageNumber,int pageCount)
        {


            //List<int> pages= new List<int>();
            //rang of 5
            //+2 from left border 0r -2 from right border

            if (pageCount <= 2)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    yield return i;
                }
            }
            else
            {




                int midPoint = pageNumber < 3 ? 3
                    : pageNumber > pageCount - 2 ? pageCount - 2
                    : pageNumber;

                int lowerBound = midPoint - 2;
                int upperBound = midPoint + 2;



                if (lowerBound != 1)
                {
                    //pages.Insert(0, 1);
                    yield return 1;
                    if (lowerBound - 1 > 1)
                    {
                        yield return -1;
                    }

                }

                for (int i = midPoint - 2; i <= midPoint + 2; i++)
                {
                    yield return i;
                }


                if (upperBound != pageCount)
                {
                    //pages.Insert(pages.Count, pageCount);
                    if (pageCount - upperBound > 1)
                    {
                        yield return -1;
                    }
                    yield return pageCount;
                }

            }
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


     
    
 