using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Management.Data;
using MVC_Management.Models;
using MVC_Management.ViewModal;

namespace MVC_Management.Controllers
{
    public class StatementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Statements
        public async Task<IActionResult> Index(string id, PaymentLoans paymentLoan)
        {

            var regi = await _context.Registration.FindAsync(id);
            if (regi == null)
            {
                return NotFound();
            }
            ViewBag.ID = regi.ID;
            ViewBag.Name = regi.Name;
            ViewBag.Surname = regi.Surname;
            ViewBag.Address = regi.Address;
            ViewBag.Gender = regi.Gender;
            ViewBag.AmountDue = regi.Amount_due;
            ViewBag.PhoneNumber = regi.Phone;
            ViewBag.AlternativeNumber = regi.Al_Phone;
            ViewBag.Number = " ";





            paymentLoan.Statements = await _context.LoanPaymentStatement.ToListAsync();
          //  paymentLoan.Statements = await _context.LoanPaymentStatement
                           // .OrderByDescending(statement => statement.LoanDate)
                           // .ToListAsync();

            if (!string.IsNullOrEmpty(paymentLoan.PhoneNumber))
            {
                paymentLoan.Statements = paymentLoan.Statements
                    .Where(x => x.PhoneNumber == paymentLoan.PhoneNumber).ToList();
            }



            // Pass the sorted data to the view
            return View(paymentLoan);


        }


        public async Task<IActionResult> Aloan(ClientsViewModal AllClientsList)
        {
            // Fetch all registrations
            AllClientsList.Allclients = await _context.Registration.ToListAsync();

            // Filter registrations if a phone number is specified
            if (!string.IsNullOrEmpty(AllClientsList.Phone))
            {
                AllClientsList.Allclients = AllClientsList.Allclients
                    .Where(x => x.Phone == AllClientsList.Phone)
                    .ToList();
            }

            // Loop through each client registration to update Amount_due
            foreach (var client in AllClientsList.Allclients)
            {
                // Calculate total amount due for the client's "Not Paid" loans
                var totalAmountDue = await _context.Loans
                    .Where(l => l.ClientID == client.ID && l.Status == "Not Paid")
                    .SumAsync(l => l.AmountDue);

                // Update the Amount_due in Registration
                client.Amount_due = totalAmountDue;

                // Save changes to the database
                _context.Update(client);
            }

            await _context.SaveChangesAsync();

            return View(AllClientsList);
        }




        // GET: Statements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statement = await _context.LoanPaymentStatement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statement == null)
            {
                return NotFound();
            }

            return View(statement);
        }

        // GET: Statements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Statements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientID,PhoneNumber,LoanDate,loanType,loanAmount,Interest,AmountDue,DueDate,PaymentAmount,Balance,Comments")] Statement statement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statement);
        }

        // GET: Statements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statement = await _context.LoanPaymentStatement.FindAsync(id);
            if (statement == null)
            {
                return NotFound();
            }
            return View(statement);
        }

        // POST: Statements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientID,PhoneNumber,LoanDate,loanType,loanAmount,Interest,AmountDue,DueDate,PaymentAmount,Balance,Comments")] Statement statement)
        {
            if (id != statement.Id)
            {
                return NotFound();
            }

            
                try
                {
                    _context.Update(statement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatementExists(statement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           
            return View(statement);
        }

        // GET: Statements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statement = await _context.LoanPaymentStatement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statement == null)
            {
                return NotFound();
            }

            return View(statement);
        }

        // POST: Statements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statement = await _context.LoanPaymentStatement.FindAsync(id);
            if (statement != null)
            {
                _context.LoanPaymentStatement.Remove(statement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatementExists(int id)
        {
            return _context.LoanPaymentStatement.Any(e => e.Id == id);
        }
    }
}
