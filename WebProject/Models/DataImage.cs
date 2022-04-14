using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class DataImage
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(10)]
        [Required]
        public string Color { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        [Required]
        public string UrlImg { get; set; }

        public Guid? LinesId { get; set; }

        public virtual Lines Lines { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }


    }
}
