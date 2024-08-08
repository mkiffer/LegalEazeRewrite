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
    public class LawFirmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LawFirmsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LawFirms
        public async Task<IActionResult> Index()
        {
            return View(await _context.LawFirms.ToListAsync());
        }

        // GET: LawFirms/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawFirm = await _context.LawFirms
                .FirstOrDefaultAsync(m => m.LawFirmID == id);
            if (lawFirm == null)
            {
                return NotFound();
            }

            return View(lawFirm);
        }

        // GET: LawFirms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LawFirms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LawFirmID,Name,Address,Phone")] LawFirm lawFirm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lawFirm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lawFirm);
        }

        // GET: LawFirms/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawFirm = await _context.LawFirms.FindAsync(id);
            if (lawFirm == null)
            {
                return NotFound();
            }
            return View(lawFirm);
        }

        // POST: LawFirms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("LawFirmID,Name,Address,Phone")] LawFirm lawFirm)
        {
            if (id != lawFirm.LawFirmID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lawFirm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LawFirmExists(lawFirm.LawFirmID))
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
            return View(lawFirm);
        }

        // GET: LawFirms/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawFirm = await _context.LawFirms
                .FirstOrDefaultAsync(m => m.LawFirmID == id);
            if (lawFirm == null)
            {
                return NotFound();
            }

            return View(lawFirm);
        }

        // POST: LawFirms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lawFirm = await _context.LawFirms.FindAsync(id);
            if (lawFirm != null)
            {
                _context.LawFirms.Remove(lawFirm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LawFirmExists(string id)
        {
            return _context.LawFirms.Any(e => e.LawFirmID == id);
        }
    }
}
