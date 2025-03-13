using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Management.Data;

using MVC_Management.Models;
using MVC_Management.ViewModal;
using Newtonsoft.Json.Linq;


namespace MVC_Management.Controllers
{
    public class PaymentsController : Controller
    {
       
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public PaymentsController(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Payments
        public async Task<IActionResult> Index( PaymentLoans paymentLoans)
        {

           



            // paymentLoans.Allpayment = await _context.Payments.ToListAsync();

            //  return View(paymentLoans);

            return View(await _context.Payments.ToListAsync());

        }




        public async Task<IActionResult> Aloan(ClientsViewModal AllClientsList)
        {



            AllClientsList.Allclients = await _context.Registration.ToListAsync();



            return View(AllClientsList);
        }



        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }






        // This Is a function to display Loans on A Create Page

        // GET: Paymentsloan
        public async Task<IActionResult> Create(string id,PaymentLoans paymentLoan)
        {

            try
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
            ViewBag.Number = null;



                paymentLoan.Allloans = await _context.Loans
                .Where(x => x.ClientID == id) // Filter by client ID
                 .ToListAsync();

                // If status is specified, filter by status
                if (!string.IsNullOrEmpty(paymentLoan.Status))
                {
                    paymentLoan.Allloans = paymentLoan.Allloans
                        .Where(x => x.Status == paymentLoan.Status)
                        .ToList();
                }


                return View(paymentLoan);


            }
            catch (Exception ex)
            {

                // Log or handle the exception appropriately
                return StatusCode(500, "An error occurred while processing your request.");
            }
            
            


    }






       
       




        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientID,PhoneNumber,PaymentDue,Balance,LoanDate,Comments")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
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
            return View(payment);
        }




  
                // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }



        public async Task<IActionResult> PerLinePayment(int? id)
        {



            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Number = null;
            ViewBag.No = "No";
            var Allloans = await _context.Loans.FindAsync(id);
            if (Allloans == null)
            {
                return NotFound();
            }
            return View(Allloans);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PerLinePayment(int id, ClientloansP clientloansP)
        {
            // Validate client loan ID
            if (id != clientloansP.Id)
            {
                return NotFound("Client loan ID mismatch.");
            }

            // Retrieve the client registration by phone number
            var registration = await _context.Registration
                .FirstOrDefaultAsync(r => r.Phone == clientloansP.PhoneNumber);

            if (registration == null)
            {
                return NotFound("Client registration not found.");
            }

            decimal totalPaymentAmount = clientloansP.Balance; // Payment made by the client
            decimal amountDue = clientloansP.AmountDue; // Loan amount due
            DateTime currentDate = DateTime.Now;

            // Check if the total payment amount exceeds the amount due
            if (totalPaymentAmount > registration.Amount_due)
            {
                TempData["ErrorMessage"] = "Payment amount exceeds the total amount due.";
                return View();
            }

            // Apply payment to the loan
            if (amountDue <= totalPaymentAmount)
            {
                totalPaymentAmount -= amountDue;
                clientloansP.Status = "Paid";
                clientloansP.PaymentDate = currentDate;
                clientloansP.PaymentAmount = amountDue; // Full payment made
                clientloansP.Oustanding = totalPaymentAmount;
                clientloansP.Name = registration.Name;
                clientloansP.Surname = registration.Surname;
                clientloansP.Oustanding = 0;
                registration.Amount_due -= amountDue;
            }
            else
            {
                // Store original loan details
                decimal originalAmountDue = clientloansP.AmountDue;
                decimal originalLoanAmount = clientloansP.loanAmount;
                DateTime originalLoanDate = clientloansP.LoanDate;
                DateTime originalDueDate = clientloansP.DueDate;
                string originalLoanType = clientloansP.loanType;
                decimal originalInterest = clientloansP.Interest;

                // Partially pay the loan
                clientloansP.AmountDue -= totalPaymentAmount;
                clientloansP.loanAmount = clientloansP.AmountDue;
                clientloansP.LoanDate = currentDate; // Reset loan date to current
                clientloansP.DueDate = currentDate;  // Reset due date
                clientloansP.Interest = 0; // No interest on unpaid amount
                clientloansP.loanType = "Outstanding Balance";
                clientloansP.Status = "Not Paid"; // Loan not fully settled
                clientloansP.Oustanding = clientloansP.AmountDue; // Remaining unpaid balance
                clientloansP.Name = registration.Name;
                clientloansP.Surname = registration.Surname;

                totalPaymentAmount = 0;


                registration.Amount_due -= clientloansP.Balance;

                // Create a rollover loan if there's an outstanding balance
                if (clientloansP.AmountDue > 0 && registration.Amount_due > 0 && totalPaymentAmount == 0)
                {
                    var rolloverLoan = new ClientloansP
                    {
                        ClientID = registration.ID,
                        PhoneNumber = clientloansP.PhoneNumber,
                        loanType = originalLoanType,
                        LoanDate = originalLoanDate,
                        Interest = originalInterest,
                        loanAmount = originalLoanAmount,
                        Name = registration.Name,
                        Surname = registration.Surname,
                        AmountDue = originalAmountDue,
                        DueDate = originalDueDate,
                        Comments = clientloansP.Comments,
                        Status = "Partially Paid",
                        Collateral = "No",
                        Balance = 0,
                        Oustanding = clientloansP.AmountDue, // Outstanding balance
                        PaymentDate = currentDate,
                        PaymentAmount = totalPaymentAmount, // Original payment amount
                    };
                    _context.Add(rolloverLoan); // Add the new rollover loan
                }
            }

            // Update loan and registration records in the database
            _context.Update(clientloansP);
            _context.Update(registration);
            await _context.SaveChangesAsync();

            // Set up ViewBag data for the invoice view
            ViewBag.Name = registration.Name;
            ViewBag.Surname = registration.Surname;
            ViewBag.PhoneNumber = registration.Phone;
            ViewBag.Address = registration.Address;
            ViewBag.Date = currentDate;
            ViewBag.Type = "Payment";
            ViewBag.PaymentAmount = clientloansP.PaymentAmount;
            ViewBag.AmountDue = clientloansP.AmountDue;
            ViewBag.Balance = registration.Amount_due;

            // Return the invoice view
            return View("Invoice");
        }







        public async Task<IActionResult> Invoice()
        {
            return View(await _context.Payments.ToListAsync());
        }


        //The full payment code

        public IActionResult fullpayment()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> fullpayment(Payment payment, ClientloansP clientloansP)
        {
            decimal totalPaymentAmount = clientloansP.Balance;
            decimal PaymentAmount = clientloansP.Balance;
            decimal OriginalloanAmountDue = 0;
            decimal OriginalloanAmount = 0;
            decimal OriginalInterest = 0;
            decimal OriginalPayment = 0;
            DateTime Originalloandate;
            DateTime OriginalDueDate;
            string OriginaLoanType;
            decimal Oustanding_balance;

            // Find the client registration
            var registration = await _context.Registration.FirstOrDefaultAsync(m => m.ID == clientloansP.ClientID);
            if (registration == null)
            {
                TempData["ErrorMessage"] = "The ID number does not exist.";
                return View();
            }

            // Check if the total payment amount exceeds the amount due
            if (totalPaymentAmount > registration.Amount_due)
            {
                TempData["ErrorMessage"] = "Payment amount exceeds the total amount due.";
                return View();
            }

            registration.Amount_due -= totalPaymentAmount;

            // Find and process all loans associated with the client
            var clientRecords = _context.Loans.Where(m => m.ClientID == clientloansP.ClientID && m.Status == "Not Paid")
                                              .OrderBy(m => m.DueDate)
                                              .ToList();

            int index = 0;
            while (index < clientRecords.Count && totalPaymentAmount > 0)
            {
                var loan = clientRecords[index];

                // Check for duplicate payments
                bool duplicatePaymentExists = _context.LoanPaymentStatement.Any(s =>
                    s.ClientID == loan.ClientID &&
                    s.PhoneNumber == loan.PhoneNumber &&
                    s.LoanDate == DateTime.Now.Date &&
                    s.PaymentAmount == loan.AmountDue);

                if (duplicatePaymentExists)
                {
                    TempData["ErrorMessage"] = "Duplicate payment detected.";
                    break;
                }

                // Apply payment to the loan
                if (loan.AmountDue <= totalPaymentAmount)
                {
                    totalPaymentAmount -= loan.AmountDue;
                    loan.Status = "Paid";
                    loan.PaymentDate = DateTime.Now;
                    loan.PaymentAmount = loan.AmountDue;
                    loan.Oustanding = 0;
                }
                else
                {

                    OriginalloanAmountDue = loan.AmountDue;
                    OriginalloanAmount = loan.loanAmount;
                    Originalloandate = loan.LoanDate;
                    OriginalDueDate = loan.DueDate;
                    OriginaLoanType = loan.loanType;
                    OriginalInterest = loan.Interest;



                    OriginalPayment = totalPaymentAmount;
                    loan.AmountDue -= totalPaymentAmount;
                    Oustanding_balance = loan.AmountDue;
                    loan.Status = "Partially Paid";

                    loan.Oustanding = loan.AmountDue;
                    loan.AmountDue = OriginalloanAmountDue;
                    loan.PaymentDate = DateTime.Now;
                    loan.PaymentAmount = OriginalPayment;
                    totalPaymentAmount = 0;



                    // Create a rollover if there's remaining balance
                    if (loan.AmountDue > 0 && registration.Amount_due > 0 && totalPaymentAmount == 0)
                    {
                        var rolloverLoan = new ClientloansP
                        {
                            ClientID = registration.ID,
                            PhoneNumber = loan.PhoneNumber,
                            loanType = "Outstanding Balance",
                            LoanDate = DateTime.Now,
                            Interest = 0,
                            loanAmount = Oustanding_balance,
                            Name = registration.Name,
                            Surname = registration.Surname,
                            AmountDue = Oustanding_balance,
                            DueDate = DateTime.Now,
                            Comments = "This is the money left after payment",
                            Status = "Not Paid",
                            Collateral = "No",



                        };
                        _context.Add(rolloverLoan);
                    }
                }



                index++;
            }

            // If totalPaymentAmount is still greater than 0, it means not all loans were paid fully
            if (totalPaymentAmount > 0)
            {
                TempData["ErrorMessage"] = "Total payment amount is now zero, but not all loans were fully paid.";
            }

            //changing the 
            if (registration.Amount_due == 0)
            {
                // Set all remaining loans to "Paid"
                foreach (var loan in clientRecords)
                {
                    loan.Status = "Paid";
                    loan.Name = registration.Name;
                    loan.Surname = registration.Surname;

                }
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            ViewBag.Name = registration.Name;
            ViewBag.Surname = registration.Surname;
            ViewBag.PhoneNumber = registration.Phone;
            ViewBag.Address = registration.Address;

            ViewBag.Date = DateTime.Now;
            ViewBag.Type = "Payment";
            ViewBag.PaymenAmount = PaymentAmount;
            ViewBag.Balance = registration.Amount_due;


            return View("Invoice");
        }



        /**
         [HttpPost]
         public IActionResult ReversePayment(string id)
         {
             // Find the payment by ID
             var payment = _context.Payments.FirstOrDefault(p => p.ClientID == id);

             if (payment == null)
             {
                 TempData["ErrorMessage"] = "Payment not found!";
                 return RedirectToAction("Index");
             }

             try
             {
                 // Perform the reverse logic (e.g., update balance, set payment status to reversed, etc.)
                 payment.Status = "Reversed";
                 payment.Balance += payment.AmountPaid; // Adjust the balance or amount as per your logic

                 _context.SaveChanges(); // Save changes to the database
                 TempData["SuccessMessage"] = "Payment successfully reversed!";
             }
             catch (Exception ex)
             {
                 TempData["ErrorMessage"] = $"Error reversing payment: {ex.Message}";
             }

             return RedirectToAction("Index");
         }

         **/




    }
}
