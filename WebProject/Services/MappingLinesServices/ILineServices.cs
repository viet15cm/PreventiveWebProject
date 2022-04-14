using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Services.MappingLinesServices
{
    public interface  ILineServices
    {
        Task<IEnumerable<Lines>> GetAllAsync();
        Task<IdentityResult> CreateAsync(Lines lines);

        Task<IdentityResult> EditorAsync(Lines lines);

        Task<IdentityResult> DeleteAsync(Guid? id);

        Task<Lines> GetLineAsync(Guid? id);

        Task<IEnumerable<Lines>> GetFindCategorizeId(Guid? CategorizeId);
        
    }
}
