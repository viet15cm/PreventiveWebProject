using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;
using WebProject.ModelValidations;

namespace WebProject.Areas.ProductManager.Models
{
    public class ProductData
    {      
        public Guid Id { get; set; }

        [Display(Name ="Mã")]
        [CodeValidations]
        [Required(ErrorMessage = "{0} không được bỏ trống !")]
        public string Code { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "{0} không được bỏ trống !")]
        [StringLength(30, ErrorMessage = "{0} độ dài không vượt quá {1} kí tự !")]       
        public string Name { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Range(0, 9999999999999999, ErrorMessage ="{0} trong khoảng từ {1} đến {2} !")]
        [DecimalValidations]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage = "Chọn {0} !")]
        [Display(Name ="Dòng")]
        public Guid? LinesId { get; set; }
        public IEnumerable<Lines> lines { get; set; }

        [NotMapped]
        public int CountImage { get; set;}
    }
}
