using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace xBlogger.Views.Shared
{
    public static class UserProfileMenuNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Main => "Main";

        public static string SearchChannels => "SearchChannels";

        public static string AddAssigment => "AddAssigment";

        public static string UserAssigments => "UsersAsigments";

        public static string AllAssigments => "AllAssigments";

        public static string AddChanel => "AddChanel";

        public static string UserChanels => "UserChanels";

        public static string ManageAccount => "Index";

        public static string News => "AllNews";

        public static string ManageUserProfile => "ManageUserProfile";

        public static string AllDeals => "AllDeals";

        public static string SearchChannelsNavClass(ViewContext viewContext) => PageNavClass(viewContext, SearchChannels);

        public static string AddAssigmentNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddAssigment);

        public static string UserAssigmentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserAssigments);

        public static string AllAssigmentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, AllAssigments);

        public static string AddChanelNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddChanel);

        public static string UserChanelsNavClass(ViewContext viewContext) => PageNavClass(viewContext, UserChanels);

        public static string ManageAccountNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageAccount);

        public static string NewsNavClass(ViewContext viewContext) => PageNavClass(viewContext, News);

        public static string UserProfileNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageUserProfile);

        public static string MainNavClass(ViewContext viewContext) => PageNavClass(viewContext, Main);

        public static string UserDealsNavClass(ViewContext viewContext) => PageNavClass(viewContext, AllDeals);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}