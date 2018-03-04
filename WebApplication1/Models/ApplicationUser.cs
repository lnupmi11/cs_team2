using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using xManik.Models;

namespace WebApplication1.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public UserRole Role { get; set; }
        public ApplicationUser()
        {
            LoggedIn = new LoggedIn();
            //switch (Role)
            //{
            //    case UserRole.Client:
            //        {
            //            LoggedIn = new Client();
            //        }
            //        break;
            //    case UserRole.Provider:
            //        {
            //            LoggedIn = new Provider();
            //        }
            //        break;
            //    default:
            //        {
            //            LoggedIn = null;
            //        }
            //        break;
            //}
        }

        public virtual LoggedIn LoggedIn { get; set; }
    }

    public class LoggedIn
    {
        public int Id { get; set; }
    }

    public class Client : LoggedIn
    {
        public string ClientProperty { get; set; }
    }

    public class Provider : LoggedIn
    {
        public string ProviderProperty { get; set; }
    }

}
