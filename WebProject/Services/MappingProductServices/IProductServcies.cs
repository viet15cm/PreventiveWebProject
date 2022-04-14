using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Services.MappingProductServices
{
    public interface IProductServcies
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IdentityResult> CreateAsync(Product product);

        Task<IdentityResult> EditorAsync(Product product);

        Task<IdentityResult> DeleteAsync(Product product);

        Task<Product> GetProductAsync(Guid? id);

        Task<IEnumerable<Product>> GetFindIdLines(Guid? LineId);
    }
}
