using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LegalEazeRewrite.Models.DataModels;

namespace LegalEazeRewrite.Controllers
{
    public class LawyersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LawyersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lawyers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lawyers.Include(l => l.LawFirm).Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Lawyers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyer = await _context.Lawyers
                .Include(l => l.LawFirm)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LawyerID == id);
            if (lawyer == null)
            {
                return NotFound();
            }

            return View(lawyer);
        }

        // GET: Lawyers/Create
        public IActionResult Create()
        {
            ViewData["LawFirmID"] = new SelectList(_context.LawFirms, "LawFirmID", "LawFirmID");
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Lawyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LawyerID,UserID,LawFirmID")] Lawyer lawyer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lawyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LawFirmID"] = new SelectList(_context.LawFirms, "LawFirmID", "LawFirmID", lawyer.LawFirmID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", lawyer.UserID);
            return View(lawyer);
        }

        // GET: Lawyers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyer = await _context.Lawyers.FindAsync(id);
            if (lawyer == null)
            {
                return NotFound();
            }
            ViewData["LawFirmID"] = new SelectList(_context.LawFirms, "LawFirmID", "LawFirmID", lawyer.LawFirmID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", lawyer.UserID);
            return View(lawyer);
        }

        // POST: Lawyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LawyerID,UserID,LawFirmID")] Lawyer lawyer)
        {
            if (id != lawyer.LawyerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lawyer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LawyerExists(lawyer.LawyerID))
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
            ViewData["LawFirmID"] = new SelectList(_context.LawFirms, "LawFirmID", "LawFirmID", lawyer.LawFirmID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", lawyer.UserID);
            return View(lawyer);
        }

        // GET: Lawyers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyer = await _context.Lawyers
                .Include(l => l.LawFirm)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LawyerID == id);
            if (lawyer == null)
            {
                return NotFound();
            }

            return View(lawyer);
        }

        // POST: Lawyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lawyer = await _context.Lawyers.FindAsync(id);
            if (lawyer != null)
            {
                _context.Lawyers.Remove(lawyer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LawyerExists(string id)
        {
            return _context.Lawyers.Any(e => e.LawyerID == id);
        }
    }
}
