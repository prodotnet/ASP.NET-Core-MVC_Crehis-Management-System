using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Management.Data;
using MVC_Management.Models;
using MVC_Management.ViewModal;

namespace MVC_Management.Controllers
{
    public class AddLoanRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public AddLoanRecordsController(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: AddLoanRecords
        public async Task<IActionResult> Index(LoansModel Loans)
        {


            Loans.Allloans = await _context.Loans.Where(x => x.Status == "Not Paid").ToListAsync();

            return View(Loans);
           
        }

        // GET: AddLoanRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addLoanRecord = await _context.AddLoanRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addLoanRecord == null)
            {
                return NotFound();
            }

            return View(addLoanRecord);
        }

        // GET: AddLoanRecords/Create
        public async Task<IActionResult> CreateAsync(LoansModel Loans)
        {

            Loans.Allloans = await _context.Loans.Where(x => x.Status == "Not Paid").ToListAsync();

            return View(Loans);


        }

        // POST: AddLoanRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientID,PhoneNumber,loanType,loanAmount,LoanDate,Interest,AmountDue,DueDate,Status,Comments")] AddLoanRecord addLoanRecord)
        {

           




            if (ModelState.IsValid)
            {
                _context.Add(addLoanRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addLoanRecord);
        }

        // GET: AddLoanRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addLoanRecord = await _context.AddLoanRecords.FindAsync(id);
            if (addLoanRecord == null)
            {
                return NotFound();
            }
            return View(addLoanRecord);
        }

        // POST: AddLoanRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientID,PhoneNumber,loanType,loanAmount,LoanDate,Interest,AmountDue,DueDate,Status,Comments")] AddLoanRecord addLoanRecord)
        {
            if (id != addLoanRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addLoanRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddLoanRecordExists(addLoanRecord.Id))
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
            return View(addLoanRecord);
        }

        // GET: AddLoanRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addLoanRecord = await _context.AddLoanRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addLoanRecord == null)
            {
                return NotFound();
            }

            return View(addLoanRecord);
        }

        // POST: AddLoanRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addLoanRecord = await _context.AddLoanRecords.FindAsync(id);
            if (addLoanRecord != null)
            {
                _context.AddLoanRecords.Remove(addLoanRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddLoanRecordExists(int id)
        {
            return _context.AddLoanRecords.Any(e => e.Id == id);
        }
    }
}
