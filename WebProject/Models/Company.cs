using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(5)]
        [Column(TypeName = ("char"))]
        [Required]
        public string Code { get; set; }

        public  string Name { get; set; }

        public ICollection<Categorize> categorizes { get; set; }
    }
}
