using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Areas.ProductManager.Models.Navigateto
{
    public class ManagerProductNavPages
    {
        public static string Product => "Product";

        public static string Commodity => "Commodity";

        public static string DataImage => "DataImage";

        public static string CommodityNavClass(ViewContext viewContext) => PageNavClass(viewContext, Commodity);
        public static string ProductNavClass(ViewContext viewContext) => PageNavClass(viewContext, Product);
        public static string DataImageNavClass(ViewContext viewContext) => PageNavClass(viewContext, DataImage);



        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageProductManager"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
