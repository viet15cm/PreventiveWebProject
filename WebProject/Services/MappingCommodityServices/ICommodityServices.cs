using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Services.MappingCommodityServices
{
    public interface ICommodityServices
    {
        Task<IEnumerable<Commodity>> GetAllAsync();

        Task<IdentityResult> EditorAsync(Commodity commodity);

        Task<IdentityResult> CreateAsync(Commodity commodity);


        Task<IdentityResult> DeleteAsync(Guid? Id);

        Task<Commodity> GetCommodityAsync(Guid? Id);

       

       
    }
}
