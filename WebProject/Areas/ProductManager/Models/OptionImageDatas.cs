using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Areas.ProductManager.Models
{
    public class OptionImageDatas
    {
        public Guid ProductId { get; set; }
        [StringLength(50, ErrorMessage ="không quá {1} kí tự !")]
        [Required(ErrorMessage ="Không bỏ trống")]
        public string SearchName { get; set; }

        public string Name { get; set; }
        public Guid LinesId { get; set; }    
        public ImageData[] ImageDatas { get; set; }
        public string Color { get; set; }
    }
}
