using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Areas.ProductManager.Models
{
    public class LineData
    {
        
        public Guid Id { get; set; }

        [StringLength(5, ErrorMessage = "{0} không quá 5 kí tự !!")]
        [Required(ErrorMessage = "{0} không được bỏ trống !!")]
        public string Code { get; set; }

        [StringLength(100, ErrorMessage = "{0} không quá 100 kí tự !!")]
        [Required(ErrorMessage = "{0} không được bỏ trống !!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống !!")]
        public Guid? CategorizeId { get; set; }
      
        public Guid? CommodityId { get; set; }
        public IEnumerable<Categorize> categorizes { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<DataImage> DataImages { get; set; }
    }
}
