﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xManik.Data;
using xManik.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;

namespace xManik.Controllers
{

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


        public async Task<IActionResult> AllServices()
        {
            return View(await _context.Services.ToListAsync());
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
            await InitUser();
            return View(_user.Services.ToList());
        }

        // GET: Services/Details/5
        [Authorize(Roles = "Provider")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await InitUser();
            var service = await _context.Services.Include(p => p.Provider).Where(p => p.Id == id).FirstOrDefaultAsync();

            if (service == null || service.Provider.Id != _user.Id)
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
        [Authorize(Roles = "Provider")]
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
        [Authorize(Roles = "Provider")]
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
        [Authorize(Roles = "Provider")]
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
        [Authorize(Roles = "Provider")]
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
