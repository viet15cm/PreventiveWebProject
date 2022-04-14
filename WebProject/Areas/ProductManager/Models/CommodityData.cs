using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;
using WebProject.ModelValidations;

namespace WebProject.Areas.ProductManager.Models
{
    public class CommodityData
    {

        public Guid Id { get; set; }

        [Display(Name = "Mã")]
        [CodeValidations]
        public string Code { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [StringLength(30, ErrorMessage = "{0} độ dài tối thiểu {1} kí tự")]        
        public string Name { get; set; }

        public ICollection<Categorize> categorizes { get; set; }
    }
}
