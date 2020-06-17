using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogAdaia.Data.Repository;
using BlogAdaia.Data;
using BlogAdaia.Models.Comments;
using BlogAdaia.ViewModels;
using BlogAdaia.ViewModels.Comment;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Linq;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlogAdaia.Controllers
{

    public class HomeController : Controller
    {
        private IPostRepository _repo;
        private IFileManager _fileMager;
        private ICommentRepository _commentRepository;

        public HomeController(IPostRepository repo, IFileManager fileManager, ICommentRepository commentRepository)
        {
            _repo = repo;
            _fileMager = fileManager;
            _commentRepository = commentRepository;
            var _comment = new MainComment();

        }

        public IActionResult Index(int pageNumber, string category) //=>
        {
            if (pageNumber < 1)
                RedirectToAction("index", new { pageNumber = 1, category });

            /* var vm = new IndexViewModel
             {
                 PageNumber=pageNumber,
                 Posts = string.IsNullOrEmpty(category) ? _repo.GetAllPosts(pageNumber) : _repo.GetAll(category)

             };*/
            var vm = _repo.GetAllPosts(pageNumber);
            return View(vm);
        }

    

      public async Task<IActionResult> Post(int id) => View(await _repo.GetById(id));



        // GET: /<controller>/
        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf("."), +1);

            return new FileStreamResult(_fileMager.imageStream(image), $"image/{mime}");
        }


        [HttpPost]
        public async Task<IActionResult> Comment(CommentViewModel vm)
        {
            if (!ModelState.IsValid)

                return RedirectToAction("Post", new { id = vm.PostId });

            var post = await _repo.GetById(vm.PostId);

            if (vm.MainCommentId == 0)
            {

                post.MainComments = post.MainComments ?? new List<MainComment>();

                post.MainComments.Add(new MainComment
                {
                    Message = vm.Message,
                    Created = DateTime.Now,


                });


                await _repo.UpdateAsync(post);
            }
            else
            {

                var comment = new SubComment
                {
                    MainCommentId = vm.MainCommentId,
                    Message = vm.Message,
                    Created = DateTime.Now

                };

                await _commentRepository.AddAsync(comment);
            }

            await _repo.SaveAllChangesAsync();



            return RedirectToAction("Post", new { id = vm.PostId });
        }


    }
}

    

