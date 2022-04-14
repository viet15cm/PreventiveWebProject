using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Commodity
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(5)]
        [Required]
        [Column(TypeName =("char"))]
        public string Code { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Categorize> Categorizes { get; set; }
    }
}
