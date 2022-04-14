using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Areas.Docs.Controllers
{
    [Area("Docs")]
    public class JavaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JavaAdvanced()
        {
            return View();
        }
    }
}
