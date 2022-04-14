using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Models
{
    public class Lines
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(5)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public Guid? CategorizeId { get; set; }

        public virtual Categorize Categorize { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<DataImage> DataImages { get; set; }
    }
}
