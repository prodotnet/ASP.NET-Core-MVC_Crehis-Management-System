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
    public class ClientsLoansRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsLoansRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientsLoansRecords
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientRecords.ToListAsync());
        }

        // GET: ClientsLoansRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientsLoansRecords = await _context.ClientRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientsLoansRecords == null)
            {
                return NotFound();
            }

            return View(clientsLoansRecords);
        }

        // GET: ClientsLoansRecords/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientsLoansRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientID,PhoneNumber,loanType,loanAmount,LoanDate,Interest,AmountDue,DueDate,Status,Comments,Balance")] ClientsLoansRecords clientsLoansRecords)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientsLoansRecords);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientsLoansRecords);
        }

        // GET: ClientsLoansRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientsLoansRecords = await _context.ClientRecords.FindAsync(id);
            if (clientsLoansRecords == null)
            {
                return NotFound();
            }
            return View(clientsLoansRecords);
        }

        // POST: ClientsLoansRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientID,PhoneNumber,loanType,loanAmount,LoanDate,Interest,AmountDue,DueDate,Status,Comments,Balance")] ClientsLoansRecords clientsLoansRecords)
        {
            if (id != clientsLoansRecords.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientsLoansRecords);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsLoansRecordsExists(clientsLoansRecords.Id))
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
            return View(clientsLoansRecords);
        }

        // GET: ClientsLoansRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientsLoansRecords = await _context.ClientRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientsLoansRecords == null)
            {
                return NotFound();
            }

            return View(clientsLoansRecords);
        }

        // POST: ClientsLoansRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientsLoansRecords = await _context.ClientRecords.FindAsync(id);
            if (clientsLoansRecords != null)
            {
                _context.ClientRecords.Remove(clientsLoansRecords);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientsLoansRecordsExists(int id)
        {
            return _context.ClientRecords.Any(e => e.Id == id);
        }
    }
}
