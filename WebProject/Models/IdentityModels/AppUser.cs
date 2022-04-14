using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models.IdentityModels
{
    public class AppUser : IdentityUser
    {
        [StringLength(30)]
        public string Address { get; set; }
    }
}
