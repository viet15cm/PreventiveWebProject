using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.AppDbContextLayer;
using WebProject.ModelExceptions;
using WebProject.ModelIdentityErrors;
using WebProject.Models;

namespace WebProject.Services.MappingCommodityServices
{
    public class CommodityServices : ICommodityServices
    {
        public readonly AppDbContext _context;
        private AppIdentityErrorDescriber _identityErrorDescriber { get; }

        public CommodityServices(AppDbContext context, AppIdentityErrorDescriber identityErrorDescriber)
        {
            _context = context;
            _identityErrorDescriber = identityErrorDescriber;
        }
        public async Task<IdentityResult> CreateAsync(Commodity commodity)
        {
            try
            {
                var IsDuplicate = await _context.commodities.AnyAsync(x => x.Code == commodity.Code);

                if (IsDuplicate)
                {
                    throw new PropertyCodeDulicateException();
                }
                await _context.commodities.AddAsync(commodity);

                await _context.SaveChangesAsync();

                return IdentityResult.Success;

            }
            catch (PropertyCodeDulicateException)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DuplicateCodeErorr());
            }
            catch (DbUpdateException)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DbUpdateErorr());
            }
            catch (Exception)
            {

                return IdentityResult.Failed(_identityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IdentityResult> DeleteAsync(Guid? Id)
        {
            try
            {
                var data = await _context.commodities.FindAsync(Id);

                if (data != null)
                {
                    _context.Remove(data);
                    await _context.SaveChangesAsync();
                    return IdentityResult.Success;
                }

                return IdentityResult.Failed(_identityErrorDescriber.DataNullErorr());
            }
            catch (Exception)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IdentityResult> EditorAsync(Commodity commodity)
        {
            try
            {
                if(commodity == null)
                {
                    return IdentityResult.Failed(_identityErrorDescriber.DataNullErorr());
                }
                var data = await _context.commodities.FindAsync(commodity.Id);

                if (data != null)
                {
                    var IsDuplicate = await _context.commodities.AnyAsync(x => x.Code == commodity.Code);
                    if(data.Code == commodity.Code)
                    {
                        IsDuplicate = false;
                    }

                    if (IsDuplicate)
                    {
                        throw new PropertyCodeDulicateException();
                    }

                    data.Code = commodity.Code;
                    data.Name = commodity.Name;

                    await _context.SaveChangesAsync();

                    return IdentityResult.Success;
                }

                return IdentityResult.Failed(_identityErrorDescriber.DataNullErorr());
            }
            catch (PropertyCodeDulicateException)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DuplicateCodeErorr());
            }
            catch (DuplicateWaitObjectException)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DuplicateCodeErorr());
            }
            catch (Exception)
            {
                return IdentityResult.Failed(_identityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IEnumerable<Commodity>> GetAllAsync()
        {
            return await _context.commodities.ToListAsync();
        }

        
        public async Task<Commodity> GetCommodityAsync(Guid? Id)
        {
            var data = await _context.commodities.FindAsync(Id);

            return data;
        }
    }
}
