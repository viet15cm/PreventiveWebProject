using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Services.MappingImageDataServices
{
    public interface IDataImageServices
    {
        Task<IEnumerable<DataImage>> GetAllAsync();
        Task<IdentityResult> CreateAsync(DataImage dataImage);

        Task<IdentityResult> EditorAsync(DataImage dataImage);

        Task<IdentityResult> DeleteAsync(Guid? id);

        Task<IdentityResult> DeleteFileAsync(string UrlImg);
        Task<IdentityResult> DeleteFileAsync(Guid? Id);
        Task<IdentityResult> CreateFileAsync(IFormFile formFile, string UrlImage);  
        Task<DataImage> GetDataImageAsync(Guid? id);

        Task<IEnumerable<DataImage>> GetFindIdLine(Guid? LineId);
        
    }
}
