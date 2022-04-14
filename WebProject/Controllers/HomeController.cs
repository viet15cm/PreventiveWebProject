using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Controllers
{
    public class HomeController : Controller
    {
        private static string contenRootPath { get; set; }

        public HomeController (IWebHostEnvironment webHostEnvironment)
        {
            contenRootPath = webHostEnvironment.ContentRootPath;
        }
        
        public IActionResult ShowImg()
        {
            string filePath = Path.Combine(contenRootPath, "FileImg", "3301.jpg");
            var bytes = System.IO.File.ReadAllBytes(filePath);


            return File(bytes, "image/jpg");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
