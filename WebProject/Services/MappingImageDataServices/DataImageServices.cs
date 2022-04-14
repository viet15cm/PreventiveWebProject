using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebProject.AppDbContextLayer;
using WebProject.Areas.ProductManager.Models;
using WebProject.Areas.ProductManager.Models.RouteEnvironment;
using WebProject.ModelExceptions;
using WebProject.ModelIdentityErrors;
using WebProject.Models;

namespace WebProject.Services.MappingImageDataServices
{
    public class DataImageServices : IDataImageServices
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly AppIdentityErrorDescriber _appIdentityErrorDescriber;
        public DataImageServices(AppDbContext context, AppIdentityErrorDescriber appIdentityErrorDescriber, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _appIdentityErrorDescriber = appIdentityErrorDescriber;
            _context = context;

        }
        public async Task<IdentityResult> CreateAsync(DataImage dataImage)
        {
            try
            {
                
                var IsImgDuplicate = await _context.dataImages.AnyAsync(x => x.UrlImg == dataImage.UrlImg);
                if (IsImgDuplicate)
                {
                    throw new PropertyCodeDulicateException();
                }
                if (IsImgDuplicate)
                {
                    throw new PropertyUrlFileDulicateException();
                }
                await _context.dataImages.AddAsync(dataImage);

                await _context.SaveChangesAsync();

                return IdentityResult.Success;

            }
            catch (PropertyUrlFileDulicateException)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DuplicateUrlFileError());
            }
            catch (PropertyCodeDulicateException)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DuplicateCodeErorr());
            }
            catch (DbUpdateException)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DbUpdateErorr());
            }
            catch (Exception)
            {

                return IdentityResult.Failed(_appIdentityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IdentityResult> CreateFileAsync(IFormFile formFile , string UrlImage)
        {
            try
            {
                var IsDuplicate = await _context.dataImages.AnyAsync(x => x.UrlImg == UrlImage);

                if (IsDuplicate)
                {
                    throw new PropertyCodeDulicateException();
                }
                if (!Directory.Exists(UrlImageFile.PathRepresentProductWebRootPath(_webHostEnvironment)))
                {
                    Directory.CreateDirectory(UrlImageFile.PathRepresentProductWebRootPath(_webHostEnvironment));
                }
                var uniqueFileName = UrlImage;
                var uploads = UrlImageFile.PathRepresentProductWebRootPath(_webHostEnvironment);
                var filePath = Path.Combine(uploads, uniqueFileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await formFile.CopyToAsync(stream);
                }
                return IdentityResult.Success;

            }

            catch (PropertyUrlFileDulicateException)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DuplicateUrlFileError());
            }
            catch (DbUpdateException)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DbUpdateErorr());
            }
            catch (Exception)
            {

                return IdentityResult.Failed(_appIdentityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IdentityResult> DeleteAsync(Guid? id)
        {
            try
            {
                var data = await _context.dataImages.FindAsync(id);
                if (data != null)
                {
                    _context.Remove(data);

                   await _context.SaveChangesAsync();

                    return IdentityResult.Success;
                }

                return IdentityResult.Failed(_appIdentityErrorDescriber.DataNullErorr());
            }
            catch (Exception)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IdentityResult> EditorAsync(DataImage dataImage)
        {
            try
            {

                var data = await _context.dataImages.FindAsync(dataImage.Id);

                var IsDuplicate = await _context.dataImages.AnyAsync(x => x.UrlImg == data.UrlImg);

                if(data.UrlImg == dataImage.UrlImg)
                {
                    IsDuplicate = false;
                }

                if(data != null)
                {
                    data.Name = dataImage.Name;
                    data.UrlImg = dataImage.UrlImg;
                    data.Color = dataImage.Color;
                    data.LinesId = dataImage.LinesId;

                    await _context.SaveChangesAsync();

                    return IdentityResult.Success;
                }

                return IdentityResult.Failed(_appIdentityErrorDescriber.DataNullErorr());

            }
            catch (Exception)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IEnumerable<DataImage>> GetAllAsync()
        {
            return await _context.dataImages.ToListAsync();
        }

        public async Task<DataImage> GetDataImageAsync(Guid? id)
        {
            try
            {
                var data = await _context.dataImages.FindAsync(id);
                return data;
            }
            catch (Exception)
            {

                return  null;
            }

        }

        private string GetUniqueFileName(string fileName)
        {

            return DateTime.Now.ToString("yymmssfff")
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 8)
                      + Path.GetExtension(fileName);
        }


        public async Task<IdentityResult> DeleteFileAsync(string UrlImg)
        {
            try
            {
                var task = new Task<IdentityResult>(() => {

                    if (UrlImg != null)
                    {
                        var path = Path.Combine(UrlImageFile.PathRepresentProductWebRootPath(_webHostEnvironment), UrlImg);
                        if (File.Exists(path))
                        {
                            File.Delete(path);

                            return IdentityResult.Success;
                        }

                        return IdentityResult.Failed(_appIdentityErrorDescriber.NotFindFileErorr());
                    }

                    return IdentityResult.Failed(_appIdentityErrorDescriber.NotFindFileErorr());

                });

                task.Start();

                return await task;
            }
            catch (Exception)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DatabaseAllErorr());
            }   
        }

        public async Task<IdentityResult> DeleteFileAsync(Guid? Id)
        {
            try
            {
                var data = await _context.dataImages.FindAsync(Id);
                
                if (data.UrlImg != null)
                {
                    var path = Path.Combine(UrlImageFile.PathRepresentProductWebRootPath(_webHostEnvironment), data.UrlImg);
                    if (File.Exists(path))
                    {
                        File.Delete(path);

                        return IdentityResult.Success;
                    }

                    return IdentityResult.Failed(_appIdentityErrorDescriber.NotFindFileErorr());
                }

                return IdentityResult.Failed(_appIdentityErrorDescriber.NotFindFileErorr());
            }
            catch (Exception)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IEnumerable<DataImage>> GetFindIdLine(Guid? LineId)
        {
            var datas = await (from x in _context.dataImages
                               where x.LinesId == LineId
                               select x).ToListAsync();

            return datas;
        }
    }
}
