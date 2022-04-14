using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models.Enum
{
    public enum Color
    {
        //đen, xanh lá, hồng, đỏ, trắng, vàng, tím, cam, nâu,
        [Display(Name = "Đen")]
        Black = 1,
        
        [Display(Name = "Xanh lá")]
        Green = 2,
        
        [Display(Name = "Hồng")]
        Pink = 3,
        
        [Display(Name = "Đỏ")]
        Red = 4 ,
        
        [Display(Name = "Trắng")]
        White = 5,
        
        [Display(Name = "Vàng")]
        Yellow = 6,
        
        [Display(Name = "Tím")]
        Purple = 7,
        
        [Display(Name = "Cam")]
        Orange = 8,
       
        [Display(Name = "Nâu")]
        Brown = 9,

        [Display(Name = "Xám")]
        Grey = 10,

        [Display(Name = "Bạc")]
        Silver = 11,
    }


}