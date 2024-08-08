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
    public class MattersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MattersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Matters.Include(m => m.Client).Include(m => m.Court);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Matters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matter = await _context.Matters
                .Include(m => m.Client)
                .Include(m => m.Court)
                .FirstOrDefaultAsync(m => m.MatterID == id);
            if (matter == null)
            {
                return NotFound();
            }

            return View(matter);
        }

        // GET: Matters/Create
        public IActionResult Create()
        {
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID");
            ViewData["CourtID"] = new SelectList(_context.Courts, "CourtID", "CourtID");
            return View();
        }

        // POST: Matters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatterID,ClientID,CourtID,Date,Location,Description")] Matter matter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", matter.ClientID);
            ViewData["CourtID"] = new SelectList(_context.Courts, "CourtID", "CourtID", matter.CourtID);
            return View(matter);
        }

        // GET: Matters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matter = await _context.Matters.FindAsync(id);
            if (matter == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", matter.ClientID);
            ViewData["CourtID"] = new SelectList(_context.Courts, "CourtID", "CourtID", matter.CourtID);
            return View(matter);
        }

        // POST: Matters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MatterID,ClientID,CourtID,Date,Location,Description")] Matter matter)
        {
            if (id != matter.MatterID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatterExists(matter.MatterID))
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
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientID", matter.ClientID);
            ViewData["CourtID"] = new SelectList(_context.Courts, "CourtID", "CourtID", matter.CourtID);
            return View(matter);
        }

        // GET: Matters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matter = await _context.Matters
                .Include(m => m.Client)
                .Include(m => m.Court)
                .FirstOrDefaultAsync(m => m.MatterID == id);
            if (matter == null)
            {
                return NotFound();
            }

            return View(matter);
        }

        // POST: Matters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var matter = await _context.Matters.FindAsync(id);
            if (matter != null)
            {
                _context.Matters.Remove(matter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatterExists(string id)
        {
            return _context.Matters.Any(e => e.MatterID == id);
        }
    }
}
