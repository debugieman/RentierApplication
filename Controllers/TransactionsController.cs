﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RentierApplication.DAL.Entities;
using RentierApplication.Data;
using RentierApplication.ViewModels;

namespace RentierApplication.Controllers
{
    [Route("payments/{paymentId}/transactions")]
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions

        [Route("/payments/{paymentId}/transactions")]
        public async Task<IActionResult> Index(int? paymentId)
        {
            if (paymentId == null || _context.Transactions == null)
            {
                return NotFound();
            }
            //zrbić metodę parametryzowaną jak create (int? id ) DONE
            //dorzucić where do wczytywania tranzkcji tak aby wyświetlać te które są  powiązane z kontekstem DONE
            //wyrzucać  błąd  jeśli ID nie jest uzupełniane
            //zmienić model używany  na froncie na ekranie index

            var applicationDbContext = _context.Transactions.Where(x => x.PaymentId == paymentId);

            ViewData["paymentId"] = paymentId;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        [HttpGet]
        [Route("/payments/{paymentId}/transactions/details")]
        public async Task<IActionResult> Details(int? id)
        {
            var transaction = await _context.Transactions
            .Include(t => t.Payment)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        //create route attirbute for this action with url pattern
        [Route("/payments/{paymentId}/transactions/create")]
        [HttpGet]
        public IActionResult Create(int? paymentId)
        {
            //wyciągnięcie   paymentu  po id  z URL
            var payment = _context.Payments.Include(i => i.RealEstate).FirstOrDefault(i => i.Id == paymentId);
            //przypisanie do  zmiennej 'payment'  danych na  viewmodel na podastawie payemntu (+domyślny czas )
            TransactionsCreateViewModel viewModel = new TransactionsCreateViewModel();
            viewModel.RealEstateName = payment.RealEstate.Name;
            viewModel.PaymentId = payment.Id;
            viewModel.DateOfTransaction = DateTime.Now;
            //wpisanie do ViewBag.TransactionType opcji które będa wyświedtlane jako select
            ViewData["TransactionType"] = viewModel.TransactionTypes;
            //wyświetlenie formuularza utowrzenia transakcji wraz z wpisanymi wcześniej danymi
            return View(viewModel);
            //return RedirectToAction(nameof(Index));
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //TODO
        //tu fixa trzeba -- apply viewmodel also
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/payments/{paymentId}/transactions/create")]
        public async Task<IActionResult> Create(TransactionsCreateViewModel transactionToAdd)
        {
            try
            {
                Transaction newTransaction = new Transaction()
                {
                    Date = transactionToAdd.DateOfTransaction,
                    Amount = transactionToAdd.Amount,
                    Description = transactionToAdd.Description,
                    Type = MapTransactionType(transactionToAdd.Type),
                    PaymentId = transactionToAdd.PaymentId,
                };

                _context.Add(newTransaction);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }

            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", new { paymentId = transactionToAdd.PaymentId });
        }

        // GET: Transactions/Edit/5
        //create route attirbute for this action with url pattern
        [HttpGet]
        [Route("/payments/{id}/transactions/edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound("edit not found");
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", transaction.PaymentId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/payments/{paymentId}/transactions/edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Amount,Description,Type,PaymentId")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", transaction.PaymentId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        //create route attirbute for this action with url pattern
        [Route("/payments/{paymentId}/transactions/delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Payment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/payments/{paymentId}/transactions/delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private TransactionType MapTransactionType(TransactionsCreateViewModel.TransactionType type)
        {
            switch (type)
            {
                case TransactionsCreateViewModel.TransactionType.OneTimeExpense:
                    return TransactionType.OneTimeExpense;

                case TransactionsCreateViewModel.TransactionType.OneTimeIncome:
                    return TransactionType.OneTimeIncome;

                default:
                    return TransactionType.OneTimeExpense;
            }
        }
    }
}