﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xManik.Data;
using Microsoft.AspNetCore.Authorization;

namespace xManik.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProvidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Providers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Providers.Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .Include(p => p.User).Include(p=>p.Portfolio).Include(p=>p.Services)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }    

        // GET: Providers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .Include(p => p.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var provider = await _context.Providers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(string id)
        {
            return _context.Providers.Any(e => e.Id == id);
        }
    }
}