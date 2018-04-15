using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using xManik.Models;
using xManik.Repositories;
using xManik.Extensions.IdentityExtensions;
using Microsoft.AspNetCore.Hosting;

namespace xManik.Managers
{
    public class UserProfileManager<TUserProfile> : IDisposable where TUserProfile : class
    {
        private readonly WorkContext _context;

        public UserProfileManager(WorkContext context)
        {
            _context = context;
        }

        #region Assigments management

        public async Task AddAssigmentAsync(ClaimsPrincipal principal, Assigment assigment)
        {
            var userProfile = GetUserProfile(principal);
            userProfile.Assigments.Add(assigment);
            await UpdateSaveAsync(userProfile);
        }

        public IEnumerable<Assigment> GetAllAssigments(ClaimsPrincipal principal)
        {
            return GetUserProfile(principal).Assigments;
        }

        public bool IsUserHasAssigments(ClaimsPrincipal principal, Assigment assigment)
        {
            return GetUserProfileId(principal) == assigment.UserProfileId; // GetAllAssigments(principal).Any(p => p.UserProfileId == assigment.UserProfileId);
        }

        #endregion

        #region Chanels management

        public async Task AddChanelsAsync(ClaimsPrincipal principal, Chanel chanel)
        {
            var userProfile = GetUserProfile(principal);
            userProfile.Chanels.Add(chanel);
            await UpdateSaveAsync(userProfile);
        }

        public IEnumerable<Chanel> GetAllChanels(ClaimsPrincipal principal)
        {
            return GetUserProfile(principal).Chanels;
        }

        #endregion

        #region UserProfile entities management

        public async Task ChangeFirstNameAsync(UserProfile user, string firstName)
        {
            user.FirstName = firstName;
            _context.UserProfiles.Update(user);
            await _context.SaveAsync();
        }

        public async Task ChangeFirstNameAsync(ClaimsPrincipal principal, string firstName)
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

        public async Task ChangeSecondNameAsync(ClaimsPrincipal principal, string secondName)
        {
            var user = GetUserProfile(principal);
            await ChangeSecondNameAsync(user, secondName);
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

        public async Task<bool> ChangeUserProfilePhotoAsync(ClaimsPrincipal principal, IFormFile file, string webRootPath)
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

        public UserProfile GetUserProfile(ClaimsPrincipal principal)
        {
            UserProfile userProfile = _context.UserProfiles.Find(principal.GetUserId());
            return userProfile;
        }

        public string GetUserProfileId(ClaimsPrincipal principal)
        {
            return principal.GetUserId();
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
