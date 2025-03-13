using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Management.Data;
using MVC_Management.Models;

namespace MVC_Management.Controllers
{
    public class CollateralRegistersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollateralRegistersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CollateralRegisters
        public async Task<IActionResult> Index()
        {
            return View(await _context.CollateralRegistration.ToListAsync());
        }

        // GET: CollateralRegisters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collateralRegister = await _context.CollateralRegistration
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collateralRegister == null)
            {
                return NotFound();
            }

            return View(collateralRegister);
        }

        // GET: CollateralRegisters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CollateralRegisters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientID,PhoneNumber,Model,SerialNumber,Condition,Date,Comments")] CollateralRegister collateralRegister)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collateralRegister);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collateralRegister);
        }

        // GET: CollateralRegisters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collateralRegister = await _context.CollateralRegistration.FindAsync(id);
            if (collateralRegister == null)
            {
                return NotFound();
            }
            return View(collateralRegister);
        }

        // POST: CollateralRegisters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientID,PhoneNumber,Model,SerialNumber,Condition,Date,Comments")] CollateralRegister collateralRegister)
        {
            if (id != collateralRegister.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collateralRegister);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollateralRegisterExists(collateralRegister.Id))
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
            return View(collateralRegister);
        }

        // GET: CollateralRegisters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collateralRegister = await _context.CollateralRegistration
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collateralRegister == null)
            {
                return NotFound();
            }

            return View(collateralRegister);
        }

        // POST: CollateralRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collateralRegister = await _context.CollateralRegistration.FindAsync(id);
            if (collateralRegister != null)
            {
                _context.CollateralRegistration.Remove(collateralRegister);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollateralRegisterExists(int id)
        {
            return _context.CollateralRegistration.Any(e => e.Id == id);
        }
    }
}
