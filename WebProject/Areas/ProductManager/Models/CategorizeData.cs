using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Areas.ProductManager.Models
{
    public class CategorizeData
    {
        public Guid Id { get; set; }

        [StringLength(5)]

        [Display(Name = "Mã")]
        [Required(ErrorMessage = "{0} không bỏ trống !")]
        public string Code { get; set; }

        [Display(Name ="Tên")]
        [Required(ErrorMessage = "{0} không bỏ trống !")]
        public string Name { get; set; }

        [Display(Name = "Dòng")]
        public IEnumerable<Lines> Lines { get; set; }

        [Display(Name = "Mặt hàng")]
        [Required(ErrorMessage = "{0} không bỏ trống !")]
        public Guid? CommodityId { get; set; }

        public IEnumerable<Commodity> Commodities { get; set; }

        

    }
}
