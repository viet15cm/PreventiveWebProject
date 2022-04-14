using Microsoft.AspNetCore.Identity;
using WebProject.ModelIdentityErrors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;
using WebProject.AppDbContextLayer;
using Microsoft.EntityFrameworkCore;
using WebProject.ModelExceptions;

namespace WebProject.Services.MappingCategorizeServices
{
    public class CategorizeServices : ICategorizeServices
    {
        public readonly AppDbContext _context;
        private AppIdentityErrorDescriber _identityErrorDescriber { get; }

        public CategorizeServices(AppDbContext context, AppIdentityErrorDescriber identityErrorDescriber)
        {
            _context = context;
            _identityErrorDescriber = identityErrorDescriber;
        }
        public async Task<IdentityResult> CreateAsync(Categorize product)
        {
            try
            {
                if (product == null)
                {
                    return IdentityResult.Failed(_identityErrorDescriber.DataNullErorr());
                }

                var IsDuplicate = await _context.categorizes.AnyAsync(x => x.Code == product.Code);

                if (!IsDuplicate)
                {
                    await _context.categorizes.AddAsync(product);
                    await _context.SaveChangesAsync();

                    return IdentityResult.Success;
                }

                return IdentityResult.Failed(_identityErrorDescriber.DbUpdateErorr());

            }
            catch(PropertyCodeDulicateException)
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
                if (Id == null)
                {
                    return IdentityResult.Failed(_identityErrorDescriber.DataNullErorr());
                }
                var data = await _context.categorizes.FindAsync(Id);

                if(data != null)
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

        public async Task<IdentityResult> EditorAsync(Categorize categorize)
        {
            try
            {
                if (categorize == null)
                {
                    return IdentityResult.Failed(_identityErrorDescriber.DataNullErorr());
                }
               
                var data = await _context.categorizes.FindAsync(categorize.Id);
                var isDuplicate = await _context.categorizes.AnyAsync(x => x.Code == categorize.Code);
                if(categorize.Code == categorize.Code)
                {
                    isDuplicate = false;
                }
                if(data != null && !isDuplicate)
                {
                    data.Name = categorize.Name;
                    data.Code = categorize.Code;
                    data.CommotityId = categorize.CommotityId;
                    data.CompanyId = categorize.CompanyId;

                    await _context.SaveChangesAsync();

                    return IdentityResult.Success;
                }

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

        public async Task<IEnumerable<Categorize>> GetAllAsync()
        {
            return await _context.categorizes.ToListAsync();
        }

        public async Task<Categorize> GetCategorizeAsync(Guid? id)
        {
            return await _context.categorizes.FindAsync(id);
        }
    }
}
