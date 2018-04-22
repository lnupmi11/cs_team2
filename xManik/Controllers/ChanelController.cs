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
    public class ChanelController : Controller
    {
        private readonly WorkContext _context;
        private readonly UserProfileManager<UserProfile> _userProfileManager;
        private readonly ChanelsManager<Chanel> _chanelsManager;

        public ChanelController(ApplicationDbContext context)
        {
            _context = new WorkContext(context);
            _userProfileManager = new UserProfileManager<UserProfile>(_context);
            _chanelsManager = new ChanelsManager<Chanel>(_context);
       }

        public IActionResult Index()
        {
            return View(_chanelsManager.GetAll());
        }

        [Authorize(Roles = "Blogger")]
        public IActionResult UserChanels()
        {
            return View(_userProfileManager.GetAllChanels(User));
        }

        // GET: Chanels/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chanel = _chanelsManager.Find(id);
            if (chanel == null)
            {
                return NotFound();
            }

            return View(chanel);
        }
               
        // GET: Chanels/Create
        [Authorize(Roles = "Blogger")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chanels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Blogger")]
        public async Task<IActionResult> Create([Bind("Network,Category,Description")] Chanel chanel)
        {
            if (ModelState.IsValid)
            {
                chanel.UserProfileId = _userProfileManager.GetUserProfileId(User);
                await _chanelsManager.CreateAsync(chanel);
                await _chanelsManager.ConfirmChanel(chanel);

                return RedirectToAction(nameof(Index));
            }
            return View(chanel);
        }

        // GET: Chanels/Edit/5
        [Authorize(Roles = "Blogger")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chanel = _chanelsManager.Find(id);
            if (chanel == null || !_userProfileManager.IsUserHasChanel(User,chanel))
            {
                return NotFound();
            }
            return View(chanel);
        }

        // POST: Chanels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Blogger")]
        public async Task<IActionResult> Edit(string id, [Bind("ChanelId,UserProfileId,Network,Category,Description")] Chanel chanel)
        {
            if (id != chanel.ChanelId || !_userProfileManager.IsUserHasChanel(User, chanel))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _chanelsManager.UpdateAsync(chanel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_chanelsManager.IsChanelExists(chanel.ChanelId))
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
            return View(chanel);
        }

        // GET: Chanels/Delete/5
        [Authorize(Roles = "Blogger")]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chanel = _chanelsManager.Find(id);
            if (chanel == null || !_userProfileManager.IsUserHasChanel(User, chanel))
            {
                return NotFound();
            }

            return View(chanel);
        }

        // POST: Chanels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Blogger")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chanel = _chanelsManager.Find(id);
            if(chanel == null || !_userProfileManager.IsUserHasChanel(User, chanel))
            {
                return NotFound();
            }
            await _chanelsManager.RemoveAsync(chanel);
            return RedirectToAction(nameof(Index));
        }
    }
}