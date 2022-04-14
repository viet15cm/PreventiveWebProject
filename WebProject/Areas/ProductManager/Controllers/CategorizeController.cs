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
using WebProject.Services.MappingCommodityServices;
using WebProject.Services.MappingProductServices;

namespace WebProject.Areas.ProductManager.Controllers
{
    [Area("ProductManager")]
    public class CategorizeController : Controller
    {
        public readonly AppDbContext _context;
        public readonly ICategorizeServices _categorizeServices;
        public readonly IProductServcies _productServcies;
        public readonly ICommodityServices _commodityServices;
        public CategorizeController(AppDbContext context , ICategorizeServices categorizeServices, 
            IProductServcies productServcies, 
            ICommodityServices commodityServices )
        {
            _context = context;
            _categorizeServices = categorizeServices;
            _productServcies = productServcies;
            _commodityServices = commodityServices;
        }

        public async Task<IActionResult> Index(Guid? Id)
        {
            if (Id != null)
            {
                var data = await _categorizeServices.GetCategorizeAsync(Id);

                if (data != null)
                {
                    var item = new CategorizeData()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Code = data.Code,
                        CommodityId = data.CommotityId,
                        Lines = _context.lines.Where(x => x.CategorizeId == data.Id).ToList()
                    };

                    var list = new List<CategorizeData>();
                    list.Add(item);

                    return View(list);
                }
            }

            var data_1 = await (from i in _context.categorizes                           
                              select new CategorizeData()
                              {
                                  Id = i.Id,
                                  Name = i.Name,
                                  Code = i.Code,
                                  CommodityId = i.CommotityId,
                                  Lines = _context.lines.Where(x => x.CategorizeId == i.Id).ToList()
                              }).ToListAsync();

            return View(data_1);

        }

        [HttpGet]
        public async Task<IActionResult> CreateCategorize(Guid? CommodityId)
        {
            
            var data = new CategorizeData();
            data.Commodities = await _commodityServices.GetAllAsync();
            data.CommodityId = CommodityId;
            return View("CreateCategorize", data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategorize([FromForm] CategorizeData categorizeData)
        {
            if (categorizeData == null)
            {
                return NotFound();
            }
            var data = new Categorize()
            {
                Name = categorizeData.Name,
                Code = categorizeData.Code,
                CommotityId = categorizeData.CommodityId
            };
            var result = await _categorizeServices.CreateAsync(data);

            if (result.Succeeded)
            {
                StatusMessage = $"Thêm thành công loại mặt hàng {data.Name}";
                return RedirectToAction("index", "Commodity");
            }

            result.Errors.ToList().ForEach(error => {
                ModelState.AddModelError(string.Empty,error.Description);
            });
            categorizeData.Commodities = await _commodityServices.GetAllAsync();
            return View("CreateCategorize", categorizeData);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategorize(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var item = await _categorizeServices.GetCategorizeAsync(Id);

            var data = new CategorizeData()
            {
                Id = item.Id,
                Code = item.Code,
                Name = item.Name,
                CommodityId = item.CommotityId,
                Commodities = await _commodityServices.GetAllAsync()
            };

            return View("EditCategorize", data);

        }

        [HttpPost]
        public async Task<IActionResult> EditCategorize(CategorizeData categorizeData)
        {
            if (categorizeData == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var data = new Categorize()
                {
                    Id = categorizeData.Id,
                    Code = categorizeData.Code,
                    CommotityId = categorizeData.CommodityId,
                    Name = categorizeData.Name
                };

                var result = await _categorizeServices.EditorAsync(data);

                if (result.Succeeded)
                {
                    StatusMessage = $"Sửa thành công loại mặt hàng {data.Name}";

                    return RedirectToAction("index", "Commodity");
                }

                result.Errors.ToList().ForEach(error => {

                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }
            categorizeData.Commodities = await _commodityServices.GetAllAsync();
            return View("EditCategorize", categorizeData);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategorize(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var data = await _categorizeServices.GetCategorizeAsync(Id);

            return View("DeleteCategorize", data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategorize(Categorize categorize)
        {
            if (categorize == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _categorizeServices.DeleteAsync(categorize.Id);

                if (result.Succeeded)
                {
                    StatusMessage = $"Xóa thành công loại mặt hàng {categorize.Name}";
                    return RedirectToAction("index", "Commodity");
                }

                result.Errors.ToList().ForEach(error => {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }

            return View("DeleteCategorize", categorize);
        }
        [TempData]
        public string StatusMessage { get; set; }

    }
}
