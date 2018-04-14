using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading.Tasks;
using xManik.Models;
using xManik.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace xManik.Extensions.Managers
{
    public class UserProfileManager<TUserProfile> : IDisposable where TUserProfile : class
    {
        private readonly WorkContext _context;

        public UserProfileManager(WorkContext context)
        {
            _context = context;
        }

        #region Services management

        public async Task AddServiceAsync(IPrincipal principal, Service service)
        {
            var userProfile = GetUserProfile(principal);
            userProfile.Services.Add(service);
            await UpdateSaveAsync(userProfile);
        }

        public IEnumerable<Service> GetAllServices(IPrincipal principal)
        {
            return GetUserProfile(principal).Services;
        }

        #endregion

        public IEnumerable<Comment> GetAllComments(IPrincipal principal)
        {
            return GetUserProfile(principal).Comments;
        }

         #region UserProfile entities management

        public async Task ChangeFirstNameAsync(UserProfile user, string firstName)
        {
            user.FirstName = firstName;
            _context.UserProfiles.Update(user);
            await _context.SaveAsync();
        }

        public async Task ChangeFirstNameAsync(IPrincipal principal, string firstName)
        {
            var user = GetUserProfile(principal);
            await ChangeFirstNameAsync(user, firstName);
        }

        public async Task ChangeSecondNameAsync(UserProfile user, string secondName)
        {
            user.SecondName = secondName;
            _context.UserProfiles.Update(user);
            await _context.SaveAsync();
        }

        public async Task ChangeSecondNameAsync(IPrincipal principal, string secondName)
        {
            var user = GetUserProfile(principal);
            await ChangeSecondNameAsync(user, secondName);
        }

        public async Task ChangeDescriptionAsync(UserProfile user, string description)
        {
            user.Description = description;
            _context.UserProfiles.Update(user);
            await _context.SaveAsync();
        }

        public async Task ChangeDescriptionAsync(IPrincipal principal, string description)
        {
            var user = GetUserProfile(principal);
            await ChangeDescriptionAsync(user, description);
        }

        public async Task<bool> ChangeUserProfilePhotoAsync(UserProfile user, IFormFile file, string webRootPath)
        {
            if (file == null)
            {
                return false;
            }

            string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            string filePath = Path.Combine(webRootPath, "Storage\\UserProfileImages\\" + filename);

            if (!Directory.Exists(webRootPath + "\\Storage\\UserProfileImages"))
            {
                Directory.CreateDirectory(webRootPath + "\\Storage\\UserProfileImages");
            }

            using (FileStream fs = File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            user.ImageName = file.FileName;

            _context.UserProfiles.Update(user);
            await _context.SaveAsync();

            return true;
        }

        public async Task<bool> ChangeUserProfilePhotoAsync(IPrincipal principal, IFormFile file, string webRootPath)
        {
            var user = GetUserProfile(principal);
            if (file == null)
            {
                return false;
            }

            await ChangeUserProfilePhotoAsync(user, file, webRootPath);

            return true;
        }
        #endregion

        public UserProfile GetUserProfile(IPrincipal principal)
        {
            return new UserProfile();
        }

        public async Task UpdateSaveAsync(UserProfile user)
        {
            _context.UserProfiles.Update(user);
            await _context.SaveAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _context != null)
                {
                    _context.Dispose(true);
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
