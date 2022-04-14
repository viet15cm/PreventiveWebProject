using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Services.MappingProductImage
{
    public interface IProductImageServices
    {
        Task<IEnumerable<ProductImage>> GetAllAsync();
        Task<IdentityResult> CreateAsync(ProductImage product);

        Task<IdentityResult> EditorAsync(ProductImage product);

        Task<IdentityResult> DeleteAsync(ProductImage product);

        Task<ProductImage> GetProductAsync(Guid? id);
   
     
    }
}
