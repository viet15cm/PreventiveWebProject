using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Areas.Docs.Models
{
    public class ManagerDocsNavPages
    {
        public static string Csharp => "Csharp";

        public static string Java => "Java";

        public static string CsharpBasic => "CsharpBasic";
        public static string CsharpAdvanced => "CsharpAdvanced";

        public static string JavapBasic => "JavaBasic";

        public static string JavapAdvanced => "JavaAdvanced";

        public static string CsharpNavClass(ViewContext viewContext) => PageNavClassCollapses(viewContext, Csharp);
        public static string JavaNavClass(ViewContext viewContext) => PageNavClassCollapses(viewContext, Java);
        public static string CsharpBasicNavClass(ViewContext viewContext) => PageNavClass(viewContext, CsharpBasic);
        public static string CsharpAdvancedNavClass(ViewContext viewContext) => PageNavClass(viewContext, CsharpAdvanced);
        public static string JavaBasicNavClass(ViewContext viewContext) => PageNavClass(viewContext, JavapBasic);
        public static string JavaAdvancedNavClass(ViewContext viewContext) => PageNavClass(viewContext, JavapAdvanced);



        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageDocsManager"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        private static string PageNavClassCollapses(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageDocsManagerCollapses"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "show" : null;
        }
    }
}
