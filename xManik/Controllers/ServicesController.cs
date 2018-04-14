using System.Collections.Generic;
using System.Linq;
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
    public class ServicesController : Controller
    {
        private readonly WorkContext _context;
        private readonly UserProfileManager<UserProfile> _userProfileManager;
        private readonly ServicesManager<Service> _servicesManager;

        public ServicesController(ApplicationDbContext context)
        {
            _context = new WorkContext(context);
            _userProfileManager = new UserProfileManager<UserProfile>(_context);
            _servicesManager = new ServicesManager<Service>(_context);
        }

        // GET: Services
        public IActionResult Index()
        {
            return View(_context.Services.GetAll());
        }

        [Authorize(Roles = "Provider")]
        public IActionResult UserServices()
        {
            return View(_userProfileManager.GetAllServices(User));
        }

        // GET: Services/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = _servicesManager.Find(id);
            if (service == null)
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
        public async Task<IActionResult> Create([Bind("Id,UserProfileId,Description,Price,Duration,DatePublished,IsPromoted")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.UserProfileId = _userProfileManager.GetUserProfileId(User);
                await _servicesManager.CreateAsync(service);

                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        [Authorize(Roles = "Provider")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = _servicesManager.Find(id);
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
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserProfileId,Description,Price,Duration,DatePublished,IsPromoted")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _servicesManager.UpdateAsync(service);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_servicesManager.IsServiceExists(service.Id))
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
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = _servicesManager.Find(id);
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
            var service = _servicesManager.Find(id);
            await _servicesManager.RemoveAsync(service);

            return RedirectToAction(nameof(Index));
        }
    }
}
