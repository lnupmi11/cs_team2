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

        public static string Index => "Index";

        public static string SearchBloggers => "SearchBloggers";

        public static string AddAssigment => "AddAssigment";

        public static string UserAssigments => "UsersAsigments";

        public static string Assigments => "Assigments";

        public static string AddChanel => "AddChanel";

        public static string UserChanels => "UserChanels";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string SearchBloggersNavClass(ViewContext viewContext) => PageNavClass(viewContext, SearchBloggers);

        public static string AddAssigmentNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddAssigment);

        public static string UserAssigmentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserAssigments);

        public static string AssigmentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Assigments);

        public static string AddChanelNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddChanel);

        public static string UserChanelsNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserChanels);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}