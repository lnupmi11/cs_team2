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
using Stripe;


namespace xManik.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AllServices(string sortOrder, string currentFilter, string searchString, int? page)
        {
            const int pageSize = 5;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
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
                .Include(s => s.Provider).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToLower();
                services = services.Where(s => s.Description.ToLower().Contains(searchString));
            }

            double maxPrice = services.Max(p => p.Price);
            double maxRate = services.Max(p => p.Provider.Rate);
            double longest = services.Max(p => p.Duration);
            DateTime latest = services.Max(p => p.DatePublished);
            TimeSpan oneYear = DateTime.Now.AddYears(1) - DateTime.Now;

            switch (sortOrder)
            {
                case "price_desc":
                    services = services.OrderByDescending(p=> p.IsPromoted ? maxPrice + p.Price : p.Price);
                    break;
                case "rate_asc":
                    services = services.OrderBy(p => p.IsPromoted ? p.Provider.Rate - maxRate - 1 : p.Provider.Rate);
                    break;
                case "rate_desc":
                    services = services.OrderByDescending(p => p.IsPromoted ? maxRate + p.Provider.Rate + 1 : p.Provider.Rate);
                    break;
                case "date_asc":
                    services = services.OrderBy(p => p.IsPromoted ? p.DatePublished.Subtract(oneYear) : p.DatePublished);
                    break;
                case "date_desc":
                    services = services.OrderByDescending(p => p.IsPromoted ? p.DatePublished.Add(oneYear) : p.DatePublished);
                    break;
                case "duration_desc":
                    services = services.OrderByDescending(p => p.IsPromoted ? longest + p.Duration : p.Duration);
                    break;
                case "duration_asc":
                    services = services.OrderBy(p => p.IsPromoted ? p.Duration - longest : p.Duration);
                    break;
                default:
                    services = services.OrderBy(p => p.IsPromoted ? p.Price - maxPrice : p.Price);
                    break;
            }

            return View(PaginatedList<Service>.Create(services, page ?? 1, pageSize));
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

        // GET: Services
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Index()
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ApplicationException("cannot find item with this id");
            }
            var user = await _context.Providers.Include(p => p.Services).Where(p => p.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }

            return View(user.Services.ToList());
        }

        // GET: Services/Details/5
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ApplicationException("cannot find item with this id");
            }
            var user = await _context.Providers.Include(p => p.Services).Where(p => p.Id == userId).FirstOrDefaultAsync()
            var service = user?.Services.SingleOrDefault(p => p.Id == id);

            if (service == null || service.Provider.Id != user.Id)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        [Authorize(Roles = "Provider")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Create([Bind("ServiceId,Description,Price,Duration")] Service service)
        {
            if (ModelState.IsValid)
            {
                var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    throw new ApplicationException("cannot find item with this id");
                }
                var user = await _context.Providers.Include(p => p.Services).Where(p => p.Id == userId).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }
                service.DatePublished = DateTime.Now;
                user.Services.Add(service);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }


        // GET: Services/Edit/5
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ApplicationException("cannot find item with this id");
            }
            var user = await _context.Providers.Include(p => p.Services).Where(p => p.Id == userId).FirstOrDefaultAsync();
            var service = user?.Services.SingleOrDefault(p => p.Id == id);
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
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Description,Price,Duration")] Service service)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ApplicationException("cannot find item with this id");
            }

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
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ApplicationException("cannot find item with this id");
            }
            var user = await _context.Providers.Include(p => p.Services).Where(p => p.Id == userId).FirstOrDefaultAsync();
            var service = user?.Services.SingleOrDefault(p => p.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ApplicationException("cannot find item with this id");
            }
            var user = await _context.Providers.Include(p => p.Services).Where(p => p.Id == userId).FirstOrDefaultAsync();
            var service = user?.Services.SingleOrDefault(p => p.Id == id);
            if (service == null)
            {
                throw new ApplicationException("Can not delete this item");
            }
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
