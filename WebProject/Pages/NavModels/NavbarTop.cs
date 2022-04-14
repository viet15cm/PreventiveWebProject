using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Pages.NavModels
{
    public class NavbarTop
    {
        public static string Home => "Home";

        public static string ProductManager => "ProductManager";


        public static string Docs => "Docs";

        public static string HomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Home);
        public static string ProductManagerNavClass(ViewContext viewContext) => PageNavClass(viewContext, ProductManager);
        public static string DocsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Docs);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageTop"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
