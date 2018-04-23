using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xManik.EF;
using xManik.Managers;
using xManik.Models;
using xManik.Repositories;

namespace xManik.Controllers
{
    public class NewsController : Controller
    {
        private readonly WorkContext _context;
        private readonly NewsManager _newsManager;

        public NewsController(ApplicationDbContext context)
        {
            _context = new WorkContext(context);
            _newsManager = new NewsManager(_context);
        }

        // GET: News
        public IActionResult Index()
        {
            return View(_newsManager.GetAll());
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
        [Authorize(Roles="Admin")]
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
        public async Task<IActionResult> Create([Bind("Title,Text")] News news)
        {
            if (ModelState.IsValid)
            {
                await _newsManager.CreateAsync(news);
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Text,DatePublished")] News news)
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
                return RedirectToAction(nameof(Index));
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
            await _newsManager.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(string id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
