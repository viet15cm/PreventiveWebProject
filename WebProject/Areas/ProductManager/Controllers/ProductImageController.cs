using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.AppDbContextLayer;
using WebProject.Areas.ProductManager.Models;
using WebProject.Models;
using WebProject.Models.Extentions;
using WebProject.Services.MappingCategorizeServices;
using WebProject.Services.MappingProductImage;

namespace WebProject.Areas.ProductManager.Controllers
{
    [Area("ProductManager")]
    public class ProductImageController : Controller
    {
        public readonly AppDbContext _context;
        public readonly ICategorizeServices _categorizeServices;
        public readonly IProductImageServices _productImageServices;
        public ProductImageController (AppDbContext appContext ,
                                        ICategorizeServices categorizeServices,
                                        IProductImageServices productImageServices)
                                    {
            _context = appContext;
            _categorizeServices = categorizeServices;
            _productImageServices = productImageServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilterOptionImages(Guid? Id, string SearchName)
        {
            var datas = new OptionImageDatas();
            if (Id == null)
            {
                return NotFound();
            }
            if (SearchName != null)
            {
                var product = await _context.products.FindAsync(Id);
                if (product != null)
                {
                    datas.Name = product.Name;
                    datas.ProductId = product.Id;
                }
                datas.ImageDatas = await (from i in _context.dataImages
                                          where i.Name.ToLower().StartsWith(SearchName) || SearchName.ToLower().StartsWith(i.Name.ToLower())
                                          select new ImageData
                                          {
                                              Id = i.Id,
                                              Name = i.Name,
                                              Color = i.Color,
                                              UrlImg = i.UrlImg
                                          }).ToArrayAsync();
            }
            return View("GetOptionImages", datas);
        }

        [HttpGet]
        public async Task<IActionResult> GetOptionImages(Guid? Id, Guid? LinesId)
        {
            if(Id == null)
            {
                return NotFound();
            }

            var datas = new OptionImageDatas();
            if (LinesId != null)
            {
                datas.LinesId = (Guid)LinesId;

                datas.ImageDatas = await (from i in _context.dataImages
                                          where i.LinesId == LinesId
                                          select new ImageData
                                          {
                                              Id = i.Id,
                                              Name = i.Name,
                                              Color = i.Color,
                                              UrlImg = i.UrlImg
                                          }).ToArrayAsync();
            }

            var product = await _context.products.FindAsync(Id);
            datas.ProductId = (Guid)Id;
            datas.Name = product.Name;

            if (product != null)
            {
                datas.ImageDatas = await (from i in _context.dataImages
                                          where i.Name.ToLower().StartsWith(product.Name.ToLower()) || product.Name.ToLower().StartsWith(i.Name.ToLower())
                                          select new ImageData
                                          {
                                              Id = i.Id,
                                              Name = i.Name,
                                              Color = i.Color,
                                              UrlImg = i.UrlImg
                                          }).ToArrayAsync();

            }
            return View("GetOptionImages", datas);
        }

        [HttpPost]
        public IActionResult FilterImageOptions([Bind("ProductId, Name, SearchName")] OptionImageDatas optionImageDatas)
        {
            if (optionImageDatas == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("GetFilterOptionImages", new { id = optionImageDatas.ProductId, SearchName = optionImageDatas.SearchName });
            }

            return View("GetOptionImages", optionImageDatas);
        }

        [HttpPost]
        public async Task<IActionResult> CreateImageOptions([FromForm] OptionImageDatas optionImageDatas)
        {
            if (optionImageDatas == null)
            {
                return NotFound();
            }

            if (optionImageDatas.ImageDatas == null)
            {
                ModelState.AddModelError(string.Empty, "Chưa chọn danh sách ảnh !!! ");

                return View("GetOptionImages", new { name = optionImageDatas.Name, id = optionImageDatas.ProductId });
            }

            var datas = from l in optionImageDatas.ImageDatas
                        where l.Option == true
                        select new ProductImageData()
                        {
                            ProdtuctId = optionImageDatas.ProductId,
                            DataImageId = l.Id,
                            Name = l.Name,
                            Color = l.Color
                            
                        };
            StatusMessage = string.Format($"Đã thêm thành công : ");
            var count = 0;
            foreach (var item in datas)
            {
                var productImage = new ProductImage()
                {
                    ProdtuctId = item.ProdtuctId,
                    DataImageId = item.DataImageId
                };
                var result = await _productImageServices.CreateAsync(productImage);

                if (result.Succeeded)
                {
                    StatusMessage += $"<{item.Name}-{EnumValue.GetValueColor(item.Color)}> ";
                    count += 1;
                }
            }

            StatusMessage += string.Format($": tổng {count} ảnh");


            return RedirectToAction("index", "Product", new { area = "ProductManager" });

        }

        [HttpGet]
        public async Task<IActionResult> DeleteImageOptions(Guid? Id)
        {
            var options = new OptionImageDatas();

            if(Id != null)
            {
                var product = await _context.products.FindAsync(Id);
                if (product != null)
                {
                    options.ProductId = product.Id;
                    options.Name = product.Name;
                    options.LinesId = (Guid)product.LinesId;

                    var productImages = from i in _context.productImages
                                        where i.ProdtuctId == product.Id
                                        select i;

                    options.ImageDatas = await (from pi in productImages
                                                join i in _context.dataImages
                                                on pi.DataImageId equals i.Id
                                                select new ImageData()
                                                {
                                                    Name = i.Name,
                                                    UrlImg = i.UrlImg,
                                                    Color = i.Color,
                                                    Id = i.Id,
                                                }).ToArrayAsync();
                }
                
            }

            return View("DeleteImageOptions", options);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImageOptions([FromForm] OptionImageDatas optionImageDatas)
        {
            if (optionImageDatas == null)
            {
                return NotFound();
            }
            var datas = (from i in optionImageDatas.ImageDatas
                         where i.Option == true
                         select new ProductImageData()
                         {
                             DataImageId = i.Id,
                             ProdtuctId = optionImageDatas.ProductId,
                             Name = i.Name,
                             Color = i.Color
                         }).ToList();
            var count = 0;
            StatusMessage = string.Format($"Đã xóa thành công : ");
            foreach (var item in datas)
            {
                var productImage = new ProductImage()
                {
                    DataImageId = item.DataImageId,
                    ProdtuctId = item.ProdtuctId
                };

                var result = await _productImageServices.DeleteAsync(productImage);             
                if (result.Succeeded)
                {
                    count += 1;
                    StatusMessage += string.Format($"<{item.Name}-{EnumValue.GetValueColor(item.Color)}>");
                }

            }
            StatusMessage += string.Format($": tổng {count} ảnh");
            return RedirectToAction("index", "product", new { area = "ProductManager" });

        }


        [TempData]
        public string StatusMessage { get; set; }

    }
}
