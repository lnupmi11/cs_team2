using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xManik.EF;
using xManik.Extensions;
using xManik.Managers;
using xManik.Models;
using xManik.Repositories;

namespace xManik.Controllers
{
    public class NewsController : Controller
    {
        private readonly WorkContext _context;
        private readonly NewsManager _newsManager;
        private readonly IHostingEnvironment _environment;

        public NewsController(ApplicationDbContext context,
             IHostingEnvironment environment)
        {
            _context = new WorkContext(context);
            _newsManager = new NewsManager(_context);
            _environment = environment;
        }

        // GET: News
        public IActionResult AllNews(int? page)
        {
            int pageSize = 4;
            return View(PaginatedList<News>.Create(_newsManager.GetAll().AsQueryable(), page ?? 1, pageSize));
        }

        // GET: News/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = _newsManager.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Title,Text")] News news, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                await _newsManager.CreateAsync(news, file, _environment.WebRootPath);
                return RedirectToAction(nameof(AllNews));
            }
            return View(news);
        }

        // GET: News/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = _newsManager.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Text,Image,DatePublished")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _newsManager.UpdateAsync(news);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AllNews));
            }
            return View(news);
        }

        // GET: News/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = _newsManager.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _newsManager.RemoveAsync(id, _environment.WebRootPath);
            return RedirectToAction(nameof(AllNews));
        }

        private bool NewsExists(string id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}