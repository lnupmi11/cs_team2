using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using xManik.Models;
using xManik.Repositories;

namespace xManik.Managers
{
    public class NewsManager : IDisposable
    {
        private readonly WorkContext _context;
        public const string DEFAULTIMAGENAME = @"default.jpg";
        public const string PATHTOIMAGESFOLDER = @"\Storage\NewsImages\";
        private const string IMAGESDIRECTORYPATH = @"\Storage\NewsImages";
        private const string FOLDERSSTRUCTURE = @"Storage\NewsImages\";

        public NewsManager(WorkContext context)
        {
            _context = context;
        }

        public IEnumerable<News> GetAll()
        {
            return _context.News.GetAll();
        }

        public async Task CreateAsync(News news, IFormFile file, string webRootPath)
        {
            if (file != null)
            {

                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string filePath = Path.Combine(webRootPath, FOLDERSSTRUCTURE + filename);

                if (!Directory.Exists(webRootPath + IMAGESDIRECTORYPATH))
                {
                    Directory.CreateDirectory(webRootPath + IMAGESDIRECTORYPATH);
                }
                if (!File.Exists(filename))
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                }
                news.Image = file.FileName;
            }
            else
            {
                news.Image = DEFAULTIMAGENAME;
            }
            news.DatePublished = DateTime.Now;
            _context.News.Create(news);
            await _context.SaveAsync();
        }

        public News Find(string id)
        {
            return _context.News.Find(id);
        }

        public async Task UpdateAsync(News news)
        {
            _context.News.Update(news);
            await _context.SaveAsync();
        }

        public async Task RemoveAsync(News news, string webRootPath)
        {
            DeleteImage(webRootPath + PATHTOIMAGESFOLDER, news.Image);
            _context.News.Remove(news);
            await _context.SaveAsync();
        }

        public async Task RemoveAsync(string id, string webRootPath)
        {
            var news = Find(id);
            DeleteImage(webRootPath + PATHTOIMAGESFOLDER, news.Image);
            _context.News.Delete(id);
            await _context.SaveAsync();
        }

        public bool IsNewsExists(string id)
        {
            return _context.News.Any(e => e.Id == id);
        }

        #region Helpers
        private void DeleteImage(string path, string name)
        {
            if (name != DEFAULTIMAGENAME && File.Exists(path + name))
            {
                File.Delete(path);
            }
        }
        #endregion

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