using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Areas.ProductManager.Models.RouteEnvironment
{
    public class UrlImageFile
    {
        public static string FoderImg = "FileImg";
        public static string FoderProduct = "ProductImg";

        public static string PathRepresentProductWebRootPath(IWebHostEnvironment webHostEnvironment)
        {
            return Path.Combine(webHostEnvironment.WebRootPath, FoderImg, FoderProduct);
        }

        public static string PathRepresentProductContentRootPath(IWebHostEnvironment webHostEnvironment)
        {
            return Path.Combine(webHostEnvironment.ContentRootPath, FoderImg, FoderProduct);
        }

        public static string PathImgSrcProductIndex(string UrlImg)
        {
            if (UrlImg != null)
            {
                return string.Format("{0}/{1}/{2}", FoderImg, FoderProduct, UrlImg);
            }
            return null;
        }
    }
}
