using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace xManik.Views.UserProfile
{
    public static class UserProfileNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string UserProfile => "UserProfile";

        public static string Portfolio => "Portfolio";

        public static string Orders => "Orders";

        public static string UserProfileDescription => "UserProfileDescription";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string IndexOrdersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Orders);

        public static string IndexPortfolioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Portfolio);

        public static string UserProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserProfile);

        public static string UserProfileDescriptionNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserProfileDescription);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
