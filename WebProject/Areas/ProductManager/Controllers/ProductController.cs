
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebProject.AppDbContextLayer;
using WebProject.Areas.ProductManager.Models;
using WebProject.Models;
using WebProject.Services;
using WebProject.Services.MappingCategorizeServices;
using WebProject.Services.MappingLinesServices;
using WebProject.Services.MappingProductImage;
using WebProject.Services.MappingProductServices;
using static System.Net.Mime.MediaTypeNames;

namespace WebProject.Areas.ProductManager.Controllers
{
    [Area("ProductManager")]
   
    public class ProductController : Controller
    {
        //    Kiểu trả về                 | Phương thức
        //------------------------------------------------
        //ContentResult               | Content()
        //EmptyResult                 | new EmptyResult()
        //FileResult                  | File()
        //ForbidResult                | Forbid()
        //JsonResult                  | Json()
        //LocalRedirectResult         | LocalRedirect()
        //RedirectResult              | Redirect()
        //RedirectToActionResult      | RedirectToAction()
        //RedirectToPageResult        | RedirectToRoute()
        //RedirectToRouteResult       | RedirectToPage()
        //PartialViewResult           | PartialView()
        //ViewComponentResult         | ViewComponent()
        //StatusCodeResult            | StatusCode()
        //ViewResult                  | View()

        private IWebHostEnvironment _webHostEnvironment { get; set; }
        private readonly IProductServcies _productServcies;
        private readonly AppDbContext _context;
        private readonly ILineServices _lineServices;
        public ProductController(IProductServcies productServices, 
                                IWebHostEnvironment webHostEnvironment ,
                                AppDbContext context,
                                ILineServices lineServices )
        {
            _productServcies = productServices;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _lineServices = lineServices;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> Index()
        {
            var items = await (from p in _context.products
                               select new ProductData()
                               {
                                   Id = p.Id,
                                   Code = p.Code,
                                   Name = p.Name,
                                   Price = p.Price,
                                   LinesId = p.LinesId,
                                   CountImage =  _context.productImages.Count(x =>x.ProdtuctId == p.Id)
                                   
                               }).ToListAsync();

            return View(items);

        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductData productData)
        {
            if(productData == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var data = new Product()
                {
                    Name = productData.Name,
                    Code = productData.Code,
                    Price = productData.Price,
                    LinesId = productData.LinesId

                };

                var result = await _productServcies.CreateAsync(data);

                if (result.Succeeded)
                {
                    StatusMessage = $"Thêm thành công sản phẩm có tên {data.Name}";
                    return RedirectToAction("index");
                }

                result.Errors.ToList().ForEach(error => {

                    ModelState.AddModelError(string.Empty, error.Description);

                });
            }

            productData.lines = await _lineServices.GetAllAsync();

            return View(productData);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct(Guid? LinesId)
        {
            var data = new ProductData();

            data.LinesId = LinesId;
            data.lines = await _lineServices.GetAllAsync();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var product = await _productServcies.GetProductAsync(Id);
            if (product != null)
            {
                var data = new ProductData()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Code = product.Code
                };

                return View("DeleteProduct", data);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductData productData)
        {
            if (productData == null)
            {
                return NotFound();
            }

            var data = await _productServcies.GetProductAsync(productData.Id);

            if (data != null)
            {
                var result = await _productServcies.DeleteAsync(data);
                if (result.Succeeded)
                {
                    StatusMessage = $"Xóa thành công sản phẩn tên {data.Name}";
                    return RedirectToAction("index");
                }

                result.Errors.ToList().ForEach(error => {
                    ModelState.AddModelError(string.Empty, error.Description);
                });

                return View("DeleteProduct", data.Id);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = await _productServcies.GetProductAsync(id);

            if(product != null)
            {
                var data = new ProductData()
                {
                    Id = product.Id,
                    Code = product.Code,
                    Name = product.Name,
                    Price = product.Price,
                    LinesId = product.LinesId,
                    lines = await _lineServices.GetAllAsync()
                };

                return View("EditProduct", data);
            };

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> EditProduct([FromForm]ProductData productData)
        {
            if (productData == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = new Product()
                {
                    Id = productData.Id,
                    Code = productData.Code,
                    Name = productData.Name,
                    Price = productData.Price,
                    LinesId = productData.LinesId
                };

                var result = await _productServcies.EditorAsync(data);

                if (result.Succeeded)
                {
                    StatusMessage = $"Chỉnh sữa thành công sản phẩm {data.Name}";
                    return RedirectToAction("index");
                }

                result.Errors.ToList().ForEach(error => {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }

            productData.lines = await _lineServices.GetAllAsync();

            return View("EditProduct", productData);
        }
    }
}
