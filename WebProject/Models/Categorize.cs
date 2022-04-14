using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Categorize
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(5)]
        public string Code { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public Guid? CommotityId { get; set; }

        public Guid? CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual Commodity Commodity { get; set; }

        public virtual ICollection<Lines> Lines { get; set; }



    }
}
