using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(5)]
        [Column(TypeName = ("char"))]
        [Required]
        public string Code { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(16,2)")]
        public decimal Price { get; set; }
  
        public Guid? LinesId { get; set; }

        public virtual Lines Lines { get; set; }

        public virtual ICollection<ProductImage> ProductImages {get; set;}
    }
}
