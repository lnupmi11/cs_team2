using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using xManik.DAL.Entities;
using xManik.DAL.Repositories;

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

        public async Task ChangeSecondNameAsync(UserProfile user, string secondName)
        {
            user.SecondName = secondName;
            _context.UserProfiles.Update(user);
            await _context.SaveAsync();
        }

        public async Task ChangeDescriptionAsync(UserProfile user, string description)
        {
            user.Description = description;
            _context.UserProfiles.Update(user);
            await _context.SaveAsync();
        }

        public bool ChangeProfilePhoto(UserProfile user, IFormFile file)
        {
            if (file == null)
            {
                return false;
            }
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                //var item = new Artwork
                //{
                //    Description = model.Description,
                //    Image = memoryStream.ToArray()
                //};
                //provider.Portfolio.Add(item);
                //_context.Update(provider);
            }

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
