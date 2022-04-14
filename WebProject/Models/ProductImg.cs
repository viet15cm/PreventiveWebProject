using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class ProductImage
    {
    
        [Required]
        public Guid ProdtuctId { get; set; }

        [Required]
        public Guid DataImageId { get; set; }
        
        public virtual Product Product { get; set; }

        public virtual DataImage DataImage { get; set; }
    }
}
