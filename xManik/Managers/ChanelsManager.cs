using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using xManik.Models;
using xManik.Repositories;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System.IO;
using System.Threading;

namespace xManik.Managers
{
    public class ChanelsManager<TChanel> : IDisposable where TChanel : class
    {
        private readonly WorkContext _context;

        public ChanelsManager(WorkContext context)
        {
            _context = context;
        }

        public IEnumerable<Chanel> GetAll()
        {
            return _context.Chanels.GetAll();
        }

        public async Task CreateAsync(Chanel service)
        {
            _context.Chanels.Create(service);
            await _context.SaveAsync();
        }

        public Chanel Find(string id)
        {
            return _context.Chanels.Find(id);
        }

        public async Task UpdateAsync(Chanel service)
        {
            _context.Chanels.Update(service);
            await _context.SaveAsync();
        }

        public async Task RemoveAsync(Chanel service)
        {
            _context.Chanels.Remove(service);
            await _context.SaveAsync();
        }

        public bool IsChanelExists(string id)
        {
            return _context.Chanels.Any(e => e.ChanelId == id);
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

        #region ChanelConfirmation
        public async Task ConfirmChanel(Chanel chanel)
        {
            switch (chanel.Network)
            {
                case SocialNetworks.YouTube:
                    await YouTubeConfirm(chanel);
                    break;
                default:
                    break;
            }

            await _context.SaveAsync();
        }

        private async Task YouTubeConfirm(Chanel chanel)
        {
            UserCredential credential;
            using (var stream = new FileStream("secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                      GoogleClientSecrets.Load(stream).Secrets,
                      // This OAuth 2.0 access scope allows for full read/write access to the
                      // authenticated user's account.
                      new[] { YouTubeService.Scope.Youtube },
                      "user",
                      CancellationToken.None,
                      new FileDataStore(this.GetType().ToString())
                  );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "xManik"
            });

            var channelsListRequest = youtubeService.Channels.List("snippet,contentDetails,statistics");
            channelsListRequest.Mine = true;

            // Retrieve the contentDetails part of the channel resource for the authenticated user's channel.
            var channelsListResponse = await channelsListRequest.ExecuteAsync();

            foreach (var channel in channelsListResponse.Items)
            {
                //TODO
                //Resolve this conversion
                chanel.SubscribersNum = (long)channel.Statistics.SubscriberCount;
                chanel.AvgViewNum = (long)channel.Statistics.ViewCount;
            }
        }
        #endregion
    }
}
