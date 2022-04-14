using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.AppDbContextLayer;
using WebProject.Areas.ProductManager.Models;
using WebProject.Models;
using WebProject.Services.MappingCategorizeServices;
using WebProject.Services.MappingImageDataServices;
using WebProject.Services.MappingLinesServices;
using WebProject.Services.MappingProductServices;

namespace WebProject.Areas.ProductManager.Controllers
{
    [Area("ProductManager")]
    public class LineController : Controller
    {
        private readonly ILineServices _lineServices;
        private readonly ICategorizeServices _categorizeServices;
        private readonly AppDbContext _context;
        private readonly IProductServcies _product;
        private readonly IDataImageServices _imageData;
        public LineController(ILineServices lineServices ,
                            ICategorizeServices categorizeServices,
                            AppDbContext context,
                            IProductServcies product,
                            IDataImageServices imageData
                            )
        {
            _lineServices = lineServices;
            _categorizeServices = categorizeServices;
            _context = context;
            _product = product;
            _imageData = imageData;
        }
        public async Task<IActionResult> Index(Guid? Id)
        {
            if(Id != null)
            {
                var data = await _lineServices.GetLineAsync(Id);
                if (data != null)
                {
                    var listData = new List<LineData>();
                    listData.Add(new LineData()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Code = data.Code,
                        Products = await _product.GetFindIdLines(data.Id),
                        DataImages = await _imageData.GetFindIdLine(data.Id)
                    });

                    return View("Index", listData);
                }
            }

            // data alll List

            var datalist = await (from x in _context.lines
                               select new LineData()
                               {
                                   Id = x.Id,
                                   Name = x.Name,
                                   Code = x.Code,
                               }).ToListAsync();

            foreach (var item in datalist)
            {
                item.Products = await _product.GetFindIdLines(item.Id);
                item.DataImages = await _imageData.GetFindIdLine(item.Id);
            }

            return View("Index", datalist);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLine(Guid? CategorizeId , Guid? CommodityId)
        {
            var data = new LineData();

            data.CategorizeId = CategorizeId;
            data.CommodityId = CommodityId;
          
            data.categorizes = await( from i in _context.categorizes
                               where i.CommotityId == CommodityId
                               select i).ToListAsync();
            
            return View("CreateLine", data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLine([FromForm]LineData line)
        {
            if (line == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = new Lines()
                {
                    Code = line.Code,
                    Name = line.Name,
                    CategorizeId = line.CategorizeId
                };
                var result = await _lineServices.CreateAsync(data);
                if (result.Succeeded)
                {
                    StatusMessage = $"Thêm thành công dòng {data.Name}";
                    return RedirectToAction("Index", "Categorize");
                }

                result.Errors.ToList().ForEach(error => {

                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }

            line.categorizes = await (from i in _context.categorizes
                                      where i.CommotityId == line.CommodityId
                                      select i).ToListAsync();

            return View("CreateLine", line);
        }

        [TempData]
        public string StatusMessage { get; set; }
    }
}
