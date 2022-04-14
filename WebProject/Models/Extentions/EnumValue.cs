using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models.Extentions
{
    public class EnumValue
    {
        public static string GetValueColor(string value)
        {
            switch (value)
            {
                case "0":
                    return null;
                case "1":
                    return "Đen";
                case "2":
                    return "Xanh lá";
                case "3":
                    return "Hồng";
                case "4":
                    return "Đỏ";
                case "5":
                    return "Trắng";
                case "6":
                    return "Vàng";
                case "7":
                    return "Tím";
                case "8":
                    return "Cam";
                case "9":
                    return "Nâu";
                case "10":
                    return "Xám";
                case "11":
                    return "Bạc";

                default:
                    return null;
            }
        }

        //đen, xanh lá, hồng, đỏ, trắng, vàng, tím, cam, nâu, 
    }
}
