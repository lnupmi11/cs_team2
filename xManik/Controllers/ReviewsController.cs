using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xManik.Data;
using xManik.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace xManik.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public IActionResult Index(string id)
        {
            var provider = _context.Providers.Include(p => p.Reviews).Where(p => p.Id == id).FirstOrDefault();
            if (provider == null)
            {
                throw new ApplicationException();
            }
            return PartialView(Tuple.Create(provider.Reviews, id));

        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Create([FromBody]dynamic data)
        {
            var clientId = (string)data["ClientId"];

            if (clientId == null || clientId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                throw new ApplicationException("Invalid user");
            }
            var providerId = (string)data["ProviderId"];
            var review = new Review()
            {
                DatePosted = DateTime.Now,
                Message = data["Message"],
                Rating = data["Rating"],
                ClientId = clientId
            };
            var provider = await _context.Providers.Include(p => p.Reviews).Where(p => p.Id == providerId).FirstOrDefaultAsync();
            if (provider == null)
            {
                throw new ApplicationException("Cannot add review for this provider");
            }

            provider.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ReviewId,DatePosted,Message")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
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
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> DeleteConfirmed([FromBody]string id)
        {
            var review = await _context.Reviews.SingleOrDefaultAsync(m => m.ReviewId == id);
            if (review.ClientId != User.FindFirstValue(ClaimTypes.NameIdentifier) || review == null)
            {
                throw new ApplicationException("invalid id");
            }
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool ReviewExists(string id)
        {
            return _context.Reviews.Any(e => e.ReviewId == id);
        }
    }
}
