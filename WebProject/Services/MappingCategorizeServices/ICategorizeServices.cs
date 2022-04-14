using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Services.MappingCategorizeServices
{
    public interface ICategorizeServices
    {
        Task<IEnumerable<Categorize>> GetAllAsync();
        Task<IdentityResult> CreateAsync(Categorize product);

        Task<IdentityResult> EditorAsync(Categorize product);

        Task<IdentityResult> DeleteAsync(Guid? Id);

        Task<Categorize> GetCategorizeAsync(Guid? id);
    }
}
