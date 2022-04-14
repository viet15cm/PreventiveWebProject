using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Areas.ProductManager.Models
{
    public class ProductImageData
    {
        [Required]
        public Guid ProdtuctId { get; set; }

        [Required]
        public Guid DataImageId { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }
      
    }
}
