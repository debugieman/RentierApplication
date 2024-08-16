using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentierApplication.DAL.Entities;
using RentierApplication.Data;
using RentierApplication.ViewModels;
using Exception = System.Exception;

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

                .FirstOrDefaultAsync(tenant => tenant.ID == id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TenantCreateViewModel tenantToAdd)
        {
            var tenant = new Tenant()
            {
                Name = tenantToAdd.Name,
                Email = tenantToAdd.Email,
                Surname = tenantToAdd.Surname,
                MoneyObligation = 0,
                Surety = tenantToAdd.Surety,
                RealEstateID = tenantToAdd.RealEstateID,
            };

            if (ModelState.IsValid)
            {
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RealEstateID"] = new SelectList(_context.RealEstates, "ID", "Name", tenant.RealEstateID);
            return View((TenantCreateViewModel)tenantToAdd);
        }

        // ON GET TYLKO ID !!!
        // GET: Tenants/Edit/5
        [HttpGet]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TenantEditViewModel tenantToEdit)
        {
            var existingTenant = await _context.Tenants.FindAsync(id);
            if (existingTenant == null)
            {
                return NotFound();
            }
            existingTenant.Name = tenantToEdit.Name;
            existingTenant.Email = tenantToEdit.Email;
            existingTenant.Surname = tenantToEdit.Surname;
            existingTenant.MoneyObligation = tenantToEdit.MoneyObligation;
            existingTenant.Surety = tenantToEdit.Surety;
            existingTenant.RealEstateID = tenantToEdit.RealEstateID;

            if (id != tenantToEdit.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return UnprocessableEntity(ex);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RealEstateID"] = new SelectList(_context.RealEstates, "ID", "Name", tenantToEdit.RealEstateID);
            return View();
        }

        // GET: Tenants/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var existingTenant = await _context.Tenants
                .Include(t => t.RealEstateTenant)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (existingTenant == null)
            {
                return NotFound();
            }

            TenantDeleteViewModel tenantToDelete = new TenantDeleteViewModel()
            {
                Surname = existingTenant.Surname,
                Name = existingTenant.Name,
                Email = existingTenant.Email,
                MoneyObligation = existingTenant.MoneyObligation,
                Surety = existingTenant.Surety,
                ID = existingTenant.ID
            };

            //return View(TenantDeleteViewModel tenantToDelete);
            return View(tenantToDelete);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var existingTenant = await _context.Tenants
                .FirstOrDefaultAsync(m => m.ID == id);
            if (existingTenant == null)
            {
                return NotFound();
            }

            _context.Tenants.Remove(existingTenant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}