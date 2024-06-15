using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentierApplication.Data;
using RentierApplication.Data.Entities;

namespace RentierApplication.Controllers
{
    public class TenantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TenantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tenants
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tenants.Include(t => t.RealEstateTenant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tenants == null)
            {
                return NotFound();
            }

            var tenants = await _context.Tenants
                .Include(t => t.RealEstateTenant)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tenants == null)
            {
                return NotFound();
            }

            return View(tenants);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            ViewData["RealEstateID"] = new SelectList(_context.RealEstates, "ID", "Name");
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Surname,Email,MoneyObligation,Surety,RealEstateID")] Tenants tenants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RealEstateID"] = new SelectList(_context.RealEstates, "ID", "Name", tenants.RealEstateID);
            return View(tenants);
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tenants == null)
            {
                return NotFound();
            }

            var tenants = await _context.Tenants.FindAsync(id);
            if (tenants == null)
            {
                return NotFound();
            }
            ViewData["RealEstateID"] = new SelectList(_context.RealEstates, "ID", "Name", tenants.RealEstateID);
            return View(tenants);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Surname,Email,MoneyObligation,Surety,RealEstateID")] Tenants tenants)
        {
            if (id != tenants.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantsExists(tenants.ID))
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
            ViewData["RealEstateID"] = new SelectList(_context.RealEstates, "ID", "Name", tenants.RealEstateID);
            return View(tenants);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tenants == null)
            {
                return NotFound();
            }

            var tenants = await _context.Tenants
                .Include(t => t.RealEstateTenant)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tenants == null)
            {
                return NotFound();
            }

            return View(tenants);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tenants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tenants'  is null.");
            }
            var tenants = await _context.Tenants.FindAsync(id);
            if (tenants != null)
            {
                _context.Tenants.Remove(tenants);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantsExists(int id)
        {
          return (_context.Tenants?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
