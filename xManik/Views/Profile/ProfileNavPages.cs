using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace xManik.Views.Profile
{
    public static class ProfileNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string Profile => "Profile";

        public static string Portfolio => "Portfolio";

        public static string Orders => "Orders";

        public static string ProfileDescription => "ProfileDescription";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string IndexOrdersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Orders);

        public static string IndexPortfolioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Portfolio);

        public static string ProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, Profile);

        public static string ProfileDescriptionNavClass(ViewContext viewContext) => PageNavClass(viewContext, ProfileDescription);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
