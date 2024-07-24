using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentierApplication.DAL.Entities;
using RentierApplication.Data;
using RentierApplication.ViewModels;


namespace RentierApplication.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payments.Include(p => p.RealEstate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.RealEstate)
                .Include(p => p.Transactions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["RealEstateId"] = new SelectList(_context.RealEstates, "ID", "Name");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(PaymentCreateViewModel paymentToAdd)
        {
            var payment = new Payment()
            {
                Id = paymentToAdd.Id,
                RealEstateId = paymentToAdd.RealEstateId,
                MonthlyIncome = paymentToAdd.MonthlyIncome,
            };
         
                        
            
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["RealEstateId"] = new SelectList(_context.RealEstates, "ID", "Name", payment.RealEstateId);
            return View();
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["RealEstateId"] = new SelectList(_context.RealEstates, "ID", "Name", payment.RealEstateId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaymentEditViewModel paymentToEdit)
        {
            var existingPayment = await _context.Payments.FindAsync(id);
            
            
            existingPayment.MonthlyIncome = paymentToEdit.MonthlyIncome;




            if (id != existingPayment.Id)
            {
                return NotFound();
            }            
                
                
            
            await _context.SaveChangesAsync();
                
                
                
                    if (!PaymentExists(existingPayment.Id))
                    {
                        return NotFound();
                    }
                    
                
                return RedirectToAction(nameof(Index));
            
            ViewData["RealEstateId"] = new SelectList(_context.RealEstates, "ID", "Name", existingPayment.RealEstateId);
            return View(existingPayment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var payment = await _context.Payments
                .Include(p => p.RealEstate)
                .FirstOrDefaultAsync(m => m.Id == id);

            PaymentDeleteViewModel paymentToDelete = new PaymentDeleteViewModel()
            {
                Id = payment.Id,
                RealEstateId = payment.RealEstateId,
                MonthlyIncome = payment.MonthlyIncome,

            };
            if (id == null || _context.Payments == null)
            {
                return NotFound();
            }

            
            if (payment == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (_context.Payments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Payments'  is null.");
            }
            
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
          return (_context.Payments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
