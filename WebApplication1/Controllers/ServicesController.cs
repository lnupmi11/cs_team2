using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xManik.Data;
using xManik.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using xManik.Extensions;

namespace xManik.Controllers
{
    [Authorize(Roles = "Provider")]
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private Provider _user;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task InitUser()
        {
            string _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _user = _user ?? await _context.Providers.Include(p => p.Services).Where(p => p.Id == _userId).FirstOrDefaultAsync();
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            await InitUser();
            return View(_user.Services.ToList());
        }

        public async Task<IActionResult> AllServices(string sortOrder,string currentFilter,string searchString,int? page)
        {
            int pageSize = 3;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewData["RateSortParm"] = sortOrder == "rate_desc" ? "rate_asc" : "rate_desc";
            ViewData["DateSortParm"] = sortOrder == "date_desc" ? "date_asc" : "date_desc";
            ViewData["DurationSortParam"] = sortOrder == "duration_desc" ? "duration_asc" : "duration_desc";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var services = _context.Services
                .Include(s => s.Provider).Include(s => s.Provider.User).AsQueryable();



            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToLower();
                services = services.Where(s => s.Description.ToLower().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "price_desc":
                    services = services.OrderByDescending(p => p.Price);
                    break;
                case "rate_asc":
                    services = services.OrderBy(s => s.Provider.Rate);
                    break;
                case "rate_desc":
                    services = services.OrderByDescending(s => s.Provider.Rate);
                    break;
                case "date_asc":
                    services = services.OrderBy(s => s.DatePublished);
                    break;
                case "date_desc":
                    services = services.OrderByDescending(s => s.DatePublished);
                    break;
                case "duration_desc":
                    services = services.OrderByDescending(s => s.Duration);
                    break;
                case "duration_asc":
                    services = services.OrderBy(s => s.Duration);
                    break;
                default:
                    services = services.OrderBy(s => s.Price);
                    break;
            }

            return View(await PaginatedList<Service>.CreateAsync(services, page ?? 1, pageSize));
        }

        public async Task<IActionResult> Information(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.Include(p => p.Provider).Where(p => p.Id == id).FirstOrDefaultAsync();

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await InitUser();
            var service = await _context.Services
                .SingleOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,Description,Price,Duration")] Service service)
        {
            if (ModelState.IsValid)
            {
                await InitUser();
                if (_user != null)
                {
                    service.DatePublished = DateTime.Now;
                    _user.Services.Add(service);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await InitUser();
            var service = _user.Services.SingleOrDefault(p => p.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Description,Price,Duration")] Service service)
        {

            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await InitUser();
            var service = _user.Services.SingleOrDefault(p => p.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await InitUser();
            var service = _user.Services.SingleOrDefault(p => p.Id == id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(string id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
