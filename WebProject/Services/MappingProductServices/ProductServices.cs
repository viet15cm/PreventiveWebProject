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
using WebProject.ModelExceptions;
using WebProject.ModelIdentityErrors;
using WebProject.Models;
using WebProject.Services.MappingCategorizeServices;

namespace WebProject.Services.MappingProductServices
{
    public class ProductServices : IProductServcies
    {

        private readonly AppDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly AppIdentityErrorDescriber _appIdentityErrorDescriber;
       
        public ProductServices(AppDbContext context ,
            AppIdentityErrorDescriber appIdentityErrorDescriber ,
            IWebHostEnvironment webHostEnvironment,
            ICategorizeServices categorizeServices)
        {
            _webHostEnvironment = webHostEnvironment;
            _appIdentityErrorDescriber = appIdentityErrorDescriber;
            _context = context;
          

        }
        public async Task<IdentityResult> CreateAsync(Product product)
        {
            try
            {
                var IsDuplicate = await _context.products.AnyAsync(x => x.Code == product.Code);
                if (IsDuplicate)
                {
                    throw new PropertyCodeDulicateException();
                }
            
                await _context.products.AddAsync(product);

                await _context.SaveChangesAsync();

                return IdentityResult.Success;

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

        public async Task<IdentityResult> DeleteAsync(Product product)
        {
            try
            {
                var data = await _context.products.FindAsync(product.Id);

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

        public async Task<IdentityResult> EditorAsync(Product product)
        {
            try
            {
                if (product == null)
                {
                    return IdentityResult.Failed(_appIdentityErrorDescriber.DataNullErorr());
                }
                var data = await _context.products.FindAsync(product.Id);

                if(data != null)
                {
                    var IsDuplicate = await _context.products.AnyAsync(x => x.Code == product.Code);

                    if(product.Code == data.Code)
                    {
                        IsDuplicate = false;
                    }

                    if (IsDuplicate)
                    {
                        throw new PropertyCodeDulicateException();
                    }

                    data.Code = product.Code;
                    data.Name = product.Name;
                    data.Price = product.Price;
                    data.LinesId = product.LinesId;
                    await _context.SaveChangesAsync();

                    return IdentityResult.Success;
                }

                return IdentityResult.Failed(_appIdentityErrorDescriber.DataNullErorr());
            }
            catch(PropertyCodeDulicateException)
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

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.products.ToListAsync();
        }

        public async Task<Product> GetProductAsync(Guid? id)
        {
            return await _context.products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetFindIdLines(Guid? LineId)
        {
            var datas = await (from x in _context.products
                               where x.LinesId == LineId
                               select x).ToListAsync();

            return datas;

        }
    }
}
