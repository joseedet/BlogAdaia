using BlogAdaia.Data.Repository;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAdaia.ViewModels;

namespace BlogAdaia.Controllers
{
    public class AuthController : Controller

    {
        private SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser>signInManager)
        {

            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {

            return  View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {

            var Result= await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);

            return RedirectToAction("Index","Panel");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await  _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");

            //return View();
        }
    }
}
