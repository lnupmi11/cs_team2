using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xManik.Views.Shared
{
    public static class UserProfileMenuNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Home => "Index";

        public static string SearchBloggers => "SearchBloggers";

        public static string AddAssigment => "AddAssigment";

        public static string UserAssigments => "UsersAsigments";

        public static string AllAssigments => "AllAssigments";

        public static string Assigments => "Assigments";

        public static string AddChanel => "AddChanel";

        public static string UserChanels => "UserChanels";

        public static string ManageAccount => "ManageAccount";

        public static string UserProfile => "UserProfile";

        public static string Deals => "UserDeals";

        public static string SearchBloggersNavClass(ViewContext viewContext) => PageNavClass(viewContext, SearchBloggers);

        public static string AddAssigmentNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddAssigment);

        public static string UserAssigmentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserAssigments);

        public static string AssigmentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Assigments);

        public static string AllAssigmentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, AllAssigments);

        public static string AddChanelNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddChanel);

        public static string UserChanelsNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserChanels);

        public static string ManageAccountNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageAccount);

        public static string UserProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserProfile);

        public static string HomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Home);

        public static string UserDealsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Deals);


        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}