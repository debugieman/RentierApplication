﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentierApplication.Data;
using RentierApplication.Data.Entities;
using RentierApplication.Data.Migrations;

namespace RentierApplication.Controllers
{
    [Authorize]
    public class RealEstatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RealEstatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RealEstates
        public async Task<IActionResult> Index()
        {
            var userEmail = User.Identity.Name;

            var userRealEstates = _context.RealEstates.Include(t => t.Tenants);//Where(r => r.UserId == userEmail);
            
            return View(await userRealEstates.ToListAsync());
        }

        // GET: RealEstates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RealEstates == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstates.Include(t => t.Tenants)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (realEstate == null)
            {
                return NotFound();
            }

            return View(realEstate);
        }

        // GET: RealEstates/Create
        public IActionResult Create()
        {
            var user = _context.Users.Where(r => r.Email == User.Identity.Name).First();
            StaticData.UserId = user.Id;

            return View();
        }

        //consider moving this method to tenant controller 
        //https://localhost:7179/RealEstates/Details/1013
        [Route("/RealEstates/Details/RealEstateID/Tenants")]
        public async Task<IActionResult> Tenants(int? id)
        {
            if (id == null || _context.Tenants == null)
            {
                return NotFound();
            }

            var tenants = _context.Tenants
                .Include(t => t.RealEstateTenant)
                .Where(m => m.RealEstateID == id);
            if (tenants == null)
            {
                return NotFound();
            }

            return View(tenants);
        }



        // POST: RealEstates/Create

        [HttpPost]
        [ValidateAntiForgeryToken]

        
        public async Task<IActionResult> Create([Bind("ID,Name,Description")] RealEstate realEstate)
        {



            if (ModelState.IsValid)
            {
                
                return RedirectToAction(nameof(Index));
            }
           
            realEstate.UserId =  StaticData.UserId;
            _context.RealEstates.Add(realEstate);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }

        // GET: RealEstates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RealEstates == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstates.FindAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", realEstate.UserId);
            return View(realEstate);
        }

        // POST: RealEstates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,UserId")] RealEstate realEstate)
        {
            if (id != realEstate.ID)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(RealEstate.UserId));


            var userEmail = User.Identity.Name;
            realEstate.UserId = userEmail;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realEstate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealEstateExists(realEstate.ID))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", realEstate.UserId);
            return View(realEstate);
        }

        // GET: RealEstates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RealEstates == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstates                
                .FirstOrDefaultAsync(m => m.ID == id);
            if (realEstate == null)
            {
                return NotFound();
            }

            return View(realEstate);
        }
        
       

        // POST: RealEstates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RealEstates == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RealEstates'  is null.");
            }
            var realEstate = await _context.RealEstates.FindAsync(id);
            if (realEstate != null)
            {
                _context.RealEstates.Remove(realEstate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealEstateExists(int id)
        {
            return _context.RealEstates.Any(e => e.ID == id);
        }

        






    }
}
