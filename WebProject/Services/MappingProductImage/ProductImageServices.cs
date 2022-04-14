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

namespace WebProject.Services.MappingProductImage
{
    public class ProductImageServices : IProductImageServices
    {
        public readonly AppDbContext _context;
        public readonly AppIdentityErrorDescriber _appIdentityErrorDescriber;
        public ProductImageServices(AppDbContext appContext , AppIdentityErrorDescriber appIdentityErrorDescriber)
        {
            _context = appContext;
            _appIdentityErrorDescriber = appIdentityErrorDescriber;
        }
        public async Task<IdentityResult> CreateAsync(ProductImage product)
        {
            try
            {
                var isDuuplicate = await _context.productImages.AnyAsync(x => x.ProdtuctId == product.ProdtuctId && x.DataImageId == product.DataImageId);
                if (isDuuplicate)
                {
                    throw new PropertyIdDulicateException();
                }
                await _context.productImages.AddAsync(product);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;

            }
            catch (PropertyIdDulicateException e)
            {
                return IdentityResult.Failed(new IdentityError() { Code = "PropertyIdDulicateException" , Description = e.ToString() });
            }
            catch (Exception)
            {
                return IdentityResult.Failed(_appIdentityErrorDescriber.DatabaseAllErorr());
            }
        }

        public async Task<IdentityResult> DeleteAsync(ProductImage productImage)
        {
            try
            {
                var data = await _context.productImages.Where(x => x.ProdtuctId == productImage.ProdtuctId && x.DataImageId == x.DataImageId).FirstOrDefaultAsync();
                if(data != null)
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

        public Task<IdentityResult> EditorAsync(ProductImage product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductImage>> GetAllAsync()
        {
            return await _context.productImages.ToListAsync();
        }
        public async Task<ProductImage> GetProductAsync(Guid? id)
        {
            return await _context.productImages.FindAsync(id);
        }
    }
}
