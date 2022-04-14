using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProject.AppDbContextLayer;
using WebProject.ModelIdentityErrors;
using WebProject.Models;

namespace WebProject.Services.MappingLinesServices
{
    public class LineServices : ILineServices
    {
        private readonly AppDbContext _context;
       
        private readonly AppIdentityErrorDescriber _appIdentityErrorDescriber;

        public LineServices(AppDbContext context,
                            AppIdentityErrorDescriber appIdentityErrorDescriber
                            )
        {
            _appIdentityErrorDescriber = appIdentityErrorDescriber;
            _context = context;
        }
        public async Task<IdentityResult> CreateAsync(Lines lines)
        {
            try
            {
                if(lines == null)
                {
                    return IdentityResult.Failed(_appIdentityErrorDescriber.DataNullErorr());
                }

                var IsDupolicate = await _context.lines.AnyAsync(X => X.Code == lines.Code);

                if (IsDupolicate)
                {
                    return IdentityResult.Failed(_appIdentityErrorDescriber.DuplicateCodeErorr());
                }
                await _context.lines.AddAsync(lines);
                await _context.SaveChangesAsync();

                return IdentityResult.Success;
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

        public async Task<IdentityResult> DeleteAsync(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return IdentityResult.Failed(_appIdentityErrorDescriber.DataNullErorr());
                }

                var data = await _context.lines.FindAsync(id);

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

        public async Task<IdentityResult> EditorAsync(Lines lines)
        {
            try
            {
                if(lines == null)
                {
                    return IdentityResult.Failed(_appIdentityErrorDescriber.DataNullErorr());
                }

                var data = await _context.lines.FindAsync(lines.Id);
                
                if(data!= null)
                {
                    var IsDuplicate = await _context.lines.AnyAsync(x => x.Code == lines.Code);

                    if(data.Code == lines.Code)
                    {
                        IsDuplicate = false;
                    }

                    if (IsDuplicate)
                    {
                        return IdentityResult.Failed(_appIdentityErrorDescriber.DuplicateCodeErorr());
                    }

                    data.Code = lines.Code;
                    data.Name = lines.Name;
                    data.CategorizeId = lines.CategorizeId;

                    await _context.SaveChangesAsync();

                    return IdentityResult.Success;

                }

                return IdentityResult.Failed(_appIdentityErrorDescriber.DataNullErorr());
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

        public async Task<IEnumerable<Lines>> GetAllAsync()
        {
            return await _context.lines.ToListAsync();
        }

        public async Task<IEnumerable<Lines>> GetFindCategorizeId(Guid? CategorizeId)
        {
            var datas = await (from c in _context.lines
                        where c.CategorizeId == CategorizeId                 
                        select c).ToListAsync();

            return datas;
        }

        public async Task<Lines> GetLineAsync(Guid? id)
        {
            return await _context.lines.FindAsync(id);
        }
    }
}
