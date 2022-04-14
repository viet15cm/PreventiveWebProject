using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;
using WebProject.ModelValidations;

namespace WebProject.Areas.ProductManager.Models
{
    public class ImageData
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Chọn {0} !")]
        [Display(Name = "Màu sắc")]
        public string Color { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Nhập {0} !")]
        [StringLength(100, ErrorMessage = "{0} không quá 100 kí tự !")]
        public string Name { get; set; }

        public string UrlImg { get; set; }

        [Display(Name = "Dòng")]
        [Required(ErrorMessage = "Chọn {0} !")]
        public Guid? LinesId { get; set; }


        [Display(Name = "Ảnh")]
        [FileImgValidations(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile FormFile { get; set; }

        public IEnumerable<Lines> Lines { get; set; }

        public bool Option { get; set; }
      


       
    }
}
