using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebProject.AppDbContextLayer;
using WebProject.Areas.ProductManager.Models;
using WebProject.Areas.ProductManager.Models.RouteEnvironment;
using WebProject.Models;
using WebProject.Models.Extentions;
using WebProject.Services.MappingImageDataServices;
using WebProject.Services.MappingLinesServices;

namespace WebProject.Areas.ProductManager.Controllers
{
    [Area("ProductManager")]
    public class DataImageController : Controller
    {

        private IWebHostEnvironment _webHostEnvironment { get; set; }
        private IDataImageServices _dataImageServices { get; set; }

        private readonly ILineServices _lineServices;
        private readonly AppDbContext _context;
   
        public DataImageController(IWebHostEnvironment webHostEnvironment,
            AppDbContext context, 
            IDataImageServices dataImageServices,
            ILineServices lineServices)
        {
            _lineServices = lineServices;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _dataImageServices = dataImageServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]Guid? LinesId, [FromQuery] string Color,  [FromQuery] string Name)
        {
            var listIndex = new ModelIndex();

            if (LinesId != null)
            {
                listIndex.ImageDatas = await (from c in _context.dataImages
                                              where c.LinesId == LinesId
                                              select c).ToListAsync();

                if (Color != null)
                {
                    listIndex.ImageDatas = (from c in listIndex.ImageDatas
                                            where c.Color == Color
                                            select c).ToList();
                }

                if (Name != null)
                {
                    listIndex.ImageDatas = (from c in listIndex.ImageDatas
                                            where Name.ToLower().StartsWith(c.Name.ToLower())
                                            select c
                                            ).ToList();

                }

                listIndex.Lines = await _lineServices.GetAllAsync();

                listIndex.LinesId = LinesId;

                return View(listIndex);
            }

            listIndex.ImageDatas = await _dataImageServices.GetAllAsync();
            listIndex.Lines = await _lineServices.GetAllAsync();

            return View(listIndex);

        }

        [HttpPost]
        public IActionResult FiterImage([FromForm]ModelIndex modelIndex)
        {
            if(modelIndex == null)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                if (modelIndex.LinesId != null)
                {
                    return RedirectToAction("index", new { LinesId = modelIndex.LinesId, Color = modelIndex.Color, Name = modelIndex.Name });
                
                }

            }
            return View("index");
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> CreateDataImage(Guid? Id , Guid? CategorizeId)
        {
            if(Id == null)
            {
                return NotFound();
            }

            var data = new ImageData();
            if (CategorizeId != null)
            {
                var listData = await _lineServices.GetFindCategorizeId(CategorizeId);

                data.Lines = listData;
                data.Id = (Guid)Id;
                return View(data);
                
            }

            data.Lines = await _lineServices.GetAllAsync();
            data.Id = (Guid)Id;

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDataImage([FromForm] ImageData imageData)
        {
            if (imageData == null)
            {
                return NotFound();
            }

            if(imageData.FormFile == null)
            {
                ModelState.AddModelError(string.Empty,$"File ảnh không được bỏ trống !!");
                imageData.Lines = await _lineServices.GetAllAsync();
                return View(imageData);
            }
            
            if (ModelState.IsValid)
            {
                var data = new DataImage()
                {
                    Color = imageData.Color,
                    LinesId = imageData.LinesId,
                    Name = imageData.Name,
                    UrlImg = GetUniqueFileName(imageData.FormFile.FileName)
                };
                var result = await _dataImageServices.CreateFileAsync(imageData.FormFile, data.UrlImg);
                if (result.Succeeded)
                {
                    result = await _dataImageServices.CreateAsync(data);

                    if (result.Succeeded)
                    {
                        StatusMessage = $"Thêm thành công ảnh {data.Name} màu {EnumValue.GetValueColor(data.Color)}";
                        return RedirectToAction("index", new { LinesId = imageData.LinesId, Color = imageData.Color, Name = imageData.Name });
                    }

                    var remove = await _dataImageServices.DeleteFileAsync(data.UrlImg);
                    
                }

                result.Errors.ToList().ForEach(erorr =>
                {
                    ModelState.AddModelError(string.Empty, erorr.Description);
                });

            }

            imageData.Lines = await _lineServices.GetAllAsync();

            return View(imageData);
        }
         
        [HttpGet]
        public IActionResult ShowDataImage(string UrlImg)
        {
            if (UrlImg == null)
            {
                return NotFound();
            }
            try
            {
                string filePath = Path.Combine(UrlImageFile.PathRepresentProductWebRootPath(_webHostEnvironment), UrlImg);
                var bytes = System.IO.File.ReadAllBytes(filePath);
                return File(bytes, "image/jpg");
            }
            catch (Exception)
            {
                return NotFound();
            }   

        }

        [HttpGet]
        public async Task<IActionResult> DeleteDataImage(Guid? Id)
        {
            if(Id== null)
            {
                return NotFound();
            }

            var data = await _context.dataImages.FindAsync(Id);

            return View("DeleteDataImage", data);

        }

        [HttpGet]
        public async Task<IActionResult> EditDataImage(Guid? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }

            var data = await _dataImageServices.GetDataImageAsync(Id);
            
              if(data != null)
            {
                var imageData = new ImageData()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Color = data.Color,
                    LinesId = data.LinesId,
                    UrlImg = data.UrlImg,
                };           
                imageData.Lines = await _lineServices.GetAllAsync();
                
                return View(imageData);
            }

            return NotFound();
           
        }
        [HttpPost]
        public async Task<IActionResult> EditDataImage(ImageData imageData)
        {
            if (imageData == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(imageData.FormFile == null && imageData.UrlImg != null)
                {
                    var data = new DataImage()
                    {
                        Id = imageData.Id,
                        Name = imageData.Name,
                        UrlImg = imageData.UrlImg,
                        Color = imageData.Color,
                        LinesId = imageData.LinesId
                    };

                    var result = await _dataImageServices.EditorAsync(data);

                    if (result.Succeeded)
                    {
                        StatusMessage = $"Cập nhật thành công ảnh {data.Name} màu {EnumValue.GetValueColor(data.Color)}";
                        return RedirectToAction("index", new { LinesId = imageData.LinesId, Color = imageData.Color, Name = imageData.Name });
                    }

                    result.Errors.ToList().ForEach(error =>
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    });
                }

                if (imageData.FormFile != null)
                {
                    var data = new DataImage()
                    {
                        Id = imageData.Id,
                        Color = imageData.Color,
                        LinesId = imageData.LinesId,
                        Name = imageData.Name,
                        UrlImg = GetUniqueFileName(imageData.FormFile.FileName)
                    };
                    var result = await _dataImageServices.CreateFileAsync(imageData.FormFile, data.UrlImg);

                    if (result.Succeeded)
                    {
                        result = await _dataImageServices.DeleteFileAsync(imageData.Id);

                        if (result.Succeeded)
                        {
                            result = await _dataImageServices.EditorAsync(data);
                            if (result.Succeeded)
                            {
                                StatusMessage = $"Cập nhật thành công ảnh {data.Name} màu {data.Color}";
                                return RedirectToAction("index", new { LinesId = imageData.LinesId, Color = imageData.Color, Name = imageData.Name });
                            }
                        }

                        var remove = await _dataImageServices.DeleteFileAsync(data.UrlImg);

                    }
                   
                    result.Errors.ToList().ForEach(erorr => {
                        ModelState.AddModelError(string.Empty, erorr.Description);
                    });
              
                }

            }
            imageData.Lines = await _lineServices.GetAllAsync();
            return View("EditDataImage",imageData);
        }

        [HttpPost]
        public async Task<IActionResult>DeleteDataImage([FromForm]DataImage dataImage)
        {
            if(dataImage == null)
            {
                return NotFound();
            }

            var result = await _dataImageServices.DeleteFileAsync(dataImage.Id);

            if (result.Succeeded)
            {
                result = await _dataImageServices.DeleteAsync(dataImage.Id);

                if (result.Succeeded)
                {
                    StatusMessage = $"Xóa thành công ảnh tên {dataImage.Name}";

                    return RedirectToAction("DeleteDataImage");
                }

                result.Errors.ToList().ForEach((errror) => {

                    ModelState.AddModelError(string.Empty, errror.Description);
                
                });
            }
            result.Errors.ToList().ForEach((errror) => {

                ModelState.AddModelError(string.Empty, errror.Description);

            });

            return View("DeleteDataImage", dataImage);
        }

        private string GetUniqueFileName(string fileName)
        {

            return DateTime.Now.ToString("yymmssfff")
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 8)
                      + Path.GetExtension(fileName);
        }

        public class ModelIndex
        {
            [Required]
            public Guid? LinesId { get; set; }

            public string Name { get; set; }

            public string Color { get; set; }

            public IEnumerable<DataImage> ImageDatas { get; set; }

            public IEnumerable<Lines> Lines { get; set; }
        }

    }
}
