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
    public class AssigmentsController : Controller
    {
        private readonly WorkContext _context;
        private readonly UserProfileManager<UserProfile> _userProfileManager;
        private readonly AssigmentsManager<Assigment> _assigmentsManager;

        public AssigmentsController(ApplicationDbContext context)
        {
            _context = new WorkContext(context);
            _userProfileManager = new UserProfileManager<UserProfile>(_context);
            _assigmentsManager = new AssigmentsManager<Assigment>(_context);
        }

        // GET: Assigments
        public IActionResult Index()
        {
            return View(_context.Assigments.GetAll());
        }

        [Authorize(Roles = "Blogger")]
        public IActionResult UserAssigments()
        {
            return View(_userProfileManager.GetAllAssigments(User));
        }

        // GET: Assigments/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assigment = _assigmentsManager.Find(id);
            if (assigment == null)
            {
                return NotFound();
            }

            return View(assigment);
        }

        // GET: Assigments/Create
        [Authorize(Roles = "Blogger")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Assigments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Blogger")]
        public async Task<IActionResult> Create([Bind("AssigmentId,ClientId,Network,Type,Format,ShortDescription,DetailedDescription,MaxBudget,Deadline,Orders")] Assigment assigment)
        {
            if (ModelState.IsValid)
            {
                assigment.ClientId = _userProfileManager.GetUserProfileId(User);
                await _assigmentsManager.CreateAsync(assigment);

                return RedirectToAction(nameof(Index));
            }
            return View(assigment);
        }

        // GET: Assigments/Edit/5
        [Authorize(Roles = "Blogger")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assigment = _assigmentsManager.Find(id);
            if (assigment == null)
            {
                return NotFound();
            }
            return View(assigment);
        }

        // POST: Assigments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Blogger")]
        public async Task<IActionResult> Edit(string id, [Bind("AssigmentId,ClientId,Network,Type,Format,ShortDescription,DetailedDescription,MaxBudget,Deadline,Orders")] Assigment assigment)
        {
            if (id != assigment.AssigmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _assigmentsManager.UpdateAsync(assigment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_assigmentsManager.IsAssigmentExists(assigment.AssigmentId))
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
            return View(assigment);
        }

        // GET: Assigments/Delete/5
        [Authorize(Roles = "Blogger")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assigment = _assigmentsManager.Find(id);
            if (assigment == null)
            {
                return NotFound();
            }

            return View(assigment);
        }

        // POST: Assigments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Blogger")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var assigment = _assigmentsManager.Find(id);
            await _assigmentsManager.RemoveAsync(assigment);

            return RedirectToAction(nameof(Index));
        }
    }
}
