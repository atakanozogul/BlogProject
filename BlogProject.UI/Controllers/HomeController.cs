using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using BlogProject.UI.Models;
using BlogProject.UI.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICoreService<Category> categoryService;
        private readonly ICoreService<Post> postService;
        private readonly ICoreService<User> userService;
        public HomeController(ILogger<HomeController> logger, ICoreService<Category> _categoryService, ICoreService<Post> _postService, ICoreService<User> _userService)
        {
            _logger = logger;
            categoryService = _categoryService;
            postService = _postService;
            userService = _userService;
        }

        public IActionResult Index()
        {
            //VM oluşturduk çünkü view tarafında userın propertysine ihtiyacımız oldu.
            PostUserVM postUserVM = new PostUserVM();
            postUserVM.Posts = postService.GetActive();
            postUserVM.Users = userService.GetActive();
            //post servisi çalıştır ve get active metodu aktif olanları getirsin
            return View(postService.GetActive());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
