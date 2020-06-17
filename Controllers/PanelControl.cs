
using System.Threading.Tasks;
using BlogAdaia.Data;
using BlogAdaia.Data.Repository;
using BlogAdaia.Models;
using BlogAdaia.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace BlogAdaia.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PanelController : Controller
    {
        private IPostRepository _repo;
        private IFileManager _fileManager;
        public PanelController(IPostRepository repo,IFileManager fileManager)

        {
            _repo = repo;

            _fileManager = fileManager;

        }

        public IActionResult Index()
        {
            var post = _repo.GetAll();
            return View(post);
        }

       

        [HttpGet]
        public  async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return View(new PostViewModel());
            else
            {
                var post =await _repo.GetById((int)id);
               return View(new PostViewModel
                { 
                    Id=post.Id,
                    Title=post.Title,
                    Body=post.Body,
                    CurrentImage=post.Image,
                    Description=post.Description,
                    Category=post.Category,
                    Tags=post.Tags,
                    
                    
                  

                }
                );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
           
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags,
                //MainComments=vm.
                //Image =await _fileManager.SaveImage(vm.Image)
            };

            if (vm.Image == null)
            {

                post.Image = vm.CurrentImage;
            }
            else
            {
                if (!string.IsNullOrEmpty(vm.CurrentImage))
                    _fileManager.RemoveImage(vm.CurrentImage);


                post.Image = await _fileManager.SaveImage(vm.Image);
            }

            if (post.Id > 0)
               await _repo.UpdateAsync(post);
            else

               await _repo.AddAsync(post);


            if
                (await _repo.SaveAllChangesAsync())

                return RedirectToAction("Index","Panel");

            else
                { 
                return View(post);
                }
            }
        public async Task<IActionResult> Remove(int id)
        {
            await _repo.RenoveAsync(await _repo.GetById(id));

            await _repo.SaveAllChangesAsync();
            return RedirectToAction("Index","Panel");

        }


    }
}
