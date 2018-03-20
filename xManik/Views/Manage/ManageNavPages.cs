﻿using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace xManik.Views.Manage
{
    public static class ManageNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string Profile => "Profile";

        public static string Portfolio => "Portfolio";

        public static string Orders => "Orders";

        public static string ProfileDescription => "ProfileDescription";

        public static string ChangePassword => "ChangePassword";

        public static string ExternalLogins => "ExternalLogins";

        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string IndexOrdersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Orders);

        public static string IndexPortfolioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Portfolio);

        public static string ProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, Profile);

        public static string ProfileDescriptionNavClass(ViewContext viewContext) => PageNavClass(viewContext, ProfileDescription);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);
    
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}