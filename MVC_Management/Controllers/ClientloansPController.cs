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
    public class ClientloansPController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public ClientloansPController(IMapper mapper,ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ClientloansP


public async Task<IActionResult> Index(string id,LoansModel Loan)
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


    Loan.Allloans = await _context.Loans.Where(x => x.Status == "Not Paid" || x.Status == " Not Paid" ).OrderByDescending(statement => statement.LoanDate).ToListAsync(); ;
    // paymentLoan.Statements = await _context.LoanPaymentStatement
    // .OrderByDescending(statement => statement.LoanDate)
    // .ToListAsync();


    if (!string.IsNullOrEmpty(id))
    {
        Loan.Allloans = Loan.Allloans
            .Where(x => x.ClientID == id).ToList();
    }



    return View(Loan);


}


       

        public async Task<IActionResult> OustandingLoans(LoansModel Loan)
        {


            Loan.Allloans = await _context.Loans.Where(x => x.Status == "Not Paid" || x.Status == " Not Paid").ToListAsync(); ;



            return View(Loan);
        }

        public async Task<IActionResult> LoansMadeToday(LoansModel Loan)
        {
            DateTime TDate = DateTime.Now;
            DateOnly todayDate = DateOnly.FromDateTime(TDate);

            Loan.Allloans = await _context.Loans
                .Where(x => DateOnly.FromDateTime(x.LoanDate) == todayDate && (x.Status == "Not Paid"))
                .ToListAsync();

            return View(Loan);
        }




        public async Task<IActionResult> Aloan(ClientsViewModal AllClientsList)
        {


            AllClientsList.Allclients = await _context.Registration.ToListAsync();



            return View(AllClientsList);
        }




        public async Task<IActionResult> Reverse(ClientsViewModal AllClientsList)
        {


            AllClientsList.Allclients = await _context.Registration.ToListAsync();



            return View(AllClientsList);
        }

        public async Task<IActionResult> UnLoans(ClientsViewModal AllClientsList)
        {


            AllClientsList.Allclients = await _context.Registration.ToListAsync();



            return View(AllClientsList);
        }


        public async Task<IActionResult> statement( ClientsViewModal AllClientsList)
        {


           

            AllClientsList.Allclients = await _context.Registration.ToListAsync();



            return View(AllClientsList);
        }



        public async Task<IActionResult> LoanPayStateme(string id, LoansModel Loan)
        {
            var regi = await _context.Registration.FindAsync(id);
            if (regi == null)
            {
                return NotFound();
            }

            // Set client information in ViewBag
            ViewBag.ID = regi.ID;
            ViewBag.Name = regi.Name;
            ViewBag.Surname = regi.Surname;
            ViewBag.Address = regi.Address;
            ViewBag.Gender = regi.Gender;
            ViewBag.AmountDue = regi.Amount_due;
            ViewBag.PhoneNumber = regi.Phone;
            ViewBag.AlternativeNumber = regi.Al_Phone;
            ViewBag.RefN = regi.ReferanceName;
            ViewBag.RefNo = regi.ReferanceNumber;
            ViewBag.Number = " ";

            // Load all loans ordered by loan date
            Loan.Allloans = await _context.Loans
                .Where(x => x.ClientID == id)

                .ToListAsync();


            // Filter loans based on ClientID
            if (!string.IsNullOrEmpty(Loan.ClientID))
            {
                Loan.Allloans = Loan.Allloans
                    .Where(x => x.ClientID == Loan.ClientID)
                    .ToList();
            }

            // Filter loans based on Status if specified
            if (!string.IsNullOrEmpty(Loan.Status))
            {
                Loan.Allloans = Loan.Allloans
                    .Where(x => x.Status == Loan.Status)
                    .ToList();
            }

            return View(Loan);
        }


        // GET: ClientloansP/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientloansP = await _context.Loans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientloansP == null)
            {
                return NotFound();
            }

            return View(clientloansP);
        }




        public IActionResult Reports()
        {
            return View();
        }
      

       

        public async Task<IActionResult> CreateAsync(string id)
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
            @ViewBag.LANumber =  regi.Phone;
            return View();
           
        }




        // POST: ClientloansP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientloansP clientloansP)
        {
            decimal loanAmount;
            decimal Interest;
            decimal AmountDue;
            
            DateTime Tdate =  DateTime.Now;
            DateOnly Today = DateOnly.FromDateTime(Tdate);


            var Registration = await _context.Registration
                .FirstOrDefaultAsync(m => m.ID == clientloansP.ClientID);


            var Existing = await _context.Loans
                               .FirstOrDefaultAsync(m => m.ClientID == clientloansP.ClientID &&
                               m.Status != "Paid" &&
                               DateOnly.FromDateTime(m.LoanDate) == Today &&
                               m.loanAmount == clientloansP.loanAmount);

            if (Existing != null)
            {
                return View("ExistingLoan");
            }
            else
            {

         

                 if (Registration == null)
                 {
                   
                    TempData["ErrorMessage"] = "The ID Number You Entered Do not Exist";
                 }
                 else
                 {


                     loanAmount = clientloansP.loanAmount;
                     Interest = loanAmount * 0.5m;
                     AmountDue = loanAmount + Interest;

                     Registration.Amount_due = Registration.Amount_due + AmountDue;

                    clientloansP.ClientID = Registration.ID;
                    clientloansP.Name= Registration.Name;
                    clientloansP.PhoneNumber = Registration.Phone;
                    clientloansP.Surname = Registration.Surname;
                    clientloansP.loanAmount = Convert.ToDecimal(loanAmount) ;
                    clientloansP.Interest = Convert.ToDecimal(Interest)  ;
                    clientloansP.LoanDate = DateTime.Now;
                    clientloansP.AmountDue = Convert.ToDecimal(AmountDue) ;
                    clientloansP.Status = "Not Paid";
                    clientloansP.Balance = 0;
  
             
             
                    _context.Add(clientloansP);
                    await _context.SaveChangesAsync();

                

                    ViewBag.Name = Registration.Name;
                    ViewBag.Surname = Registration.Surname;
                    ViewBag.PhoneNumber = Registration.Phone;
                    ViewBag.Address = Registration.Address;

                    ViewBag.Date = DateTime.Now;
                    ViewBag.loanType = clientloansP.loanType;
                    ViewBag.Amount = clientloansP.loanAmount;
                    ViewBag.Amout_due = clientloansP.AmountDue;
                    ViewBag.PaymenAmount = 0;
                  ViewBag.Balance = Registration.Amount_due;

             
                 }

            }


            return View("Invoice");
        }



        public async Task<IActionResult> UniqueCAsync(string id)
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
            @ViewBag.LANumber = regi.Phone;
            return View();

        }



        // POST: ClientloansP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UniqueC(ClientloansP clientloansP)
        {
            decimal loanAmount;
            decimal Interest;
            decimal AmountDue;

            DateTime Tdate = DateTime.Now;
            DateOnly Today = DateOnly.FromDateTime(Tdate);


            var Registration = await _context.Registration
                .FirstOrDefaultAsync(m => m.ID == clientloansP.ClientID);


            var Existing = await _context.Loans
                .FirstOrDefaultAsync(m => DateOnly.FromDateTime(m.LoanDate) == Today && m.loanAmount == clientloansP.loanAmount && m.ClientID == clientloansP.ClientID);


            if (Existing != null)
            {

                return View("ExistingLoan");

            }




            if (Registration == null)
            {
                TempData["ErrorMessage"] = "The ID Number You Entered Do not Exist";
            }
            else
            {




                loanAmount = clientloansP.loanAmount;
                decimal UniqueInterest = clientloansP.Interest;

                Interest = loanAmount *( UniqueInterest / 100);

                AmountDue = loanAmount + Interest;

                Registration.Amount_due = Registration.Amount_due + AmountDue;

                clientloansP.ClientID = Registration.ID;
                clientloansP.Name = Registration.Name;
                clientloansP.PhoneNumber = Registration.Phone;
                clientloansP.Surname = Registration.Surname;
                clientloansP.loanAmount = Convert.ToDecimal(loanAmount);
                clientloansP.Interest = Convert.ToDecimal(Interest);
                clientloansP.LoanDate = DateTime.Now;
                clientloansP.AmountDue = Convert.ToDecimal(AmountDue);
                clientloansP.Status = "Not Paid";
                clientloansP.Balance = 0;




                _context.Add(clientloansP);


                await _context.SaveChangesAsync();

                ViewBag.Name = Registration.Name;
                ViewBag.Surname = Registration.Surname;
                ViewBag.PhoneNumber = Registration.Phone;
                ViewBag.Address = Registration.Address;

                ViewBag.Date = DateTime.Now;
                ViewBag.loanType = clientloansP.loanType;
                ViewBag.Amount = clientloansP.loanAmount;
                ViewBag.Amout_due = clientloansP.AmountDue;
                ViewBag.PaymenAmount = 0;
                ViewBag.Balance = Registration.Amount_due;


            }




            return View("Invoice");
        }






        // GET: ClientloansP/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientloansP = await _context.Loans.FindAsync(id);
            if (clientloansP == null)
            {
                return NotFound();
            }
            return View(clientloansP);
        }

        // POST: ClientloansP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientloansP clientloansP)
        {
            if (id != clientloansP.Id)
            {
                return NotFound();
            }

             try
                {
                    _context.Update(clientloansP);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientloansPExists(clientloansP.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            return View("Updates");


        }






        // GET: ClientloansP/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientloansP = await _context.Loans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientloansP == null)
            {
                return NotFound();
            }

            return View(clientloansP);
        }

        // POST: ClientloansP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientloansP = await _context.Loans.FindAsync(id);
            if (clientloansP != null)
            {

                var Registration = await _context.Registration
                    .FirstOrDefaultAsync(m => m.ID == clientloansP.ClientID);

                if (clientloansP != null)
                {

                    Registration.Amount_due -= clientloansP.AmountDue;

                }
                _context.Loans.Remove(clientloansP);


            }

            await _context.SaveChangesAsync();
            return View("reverseloan");
        }

        private bool ClientloansPExists(int id)
        {
            return _context.Loans.Any(e => e.Id == id);
        }


        public async Task<IActionResult> Addloan(ClientsViewModal AllClientsList)
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

           

            return View(AllClientsList);
        }


        public async Task<IActionResult> Invoice()
        {
            return View(await _context.Loans.ToListAsync());
        }


        public async Task<IActionResult> ExistingLoan()
        {
            return View(await _context.Loans.ToListAsync());
        }







        /***********************************************************************************************************Collateral section************************************************************************************************************************************************************************************************/
        public async Task<IActionResult> Collateral_index(string id, LoansModel Loan)
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


            Loan.Allloans = await _context.Loans.Where(x => x.Collateral == "Yes"  && x.Model !=null).OrderByDescending(statement => statement.LoanDate).ToListAsync(); ;
            // paymentLoan.Statements = await _context.LoanPaymentStatement
            // .OrderByDescending(statement => statement.LoanDate)
            // .ToListAsync();


            if (!string.IsNullOrEmpty(id))
            {
                Loan.Allloans = Loan.Allloans
                    .Where(x => x.ClientID == id).ToList();
            }



            return View(Loan);


        }
        public async Task<IActionResult> CollateralRegister(ClientsViewModal AllClientsList)
        {


            AllClientsList.Allclients = await _context.Registration.ToListAsync();



            return View(AllClientsList);
        }



        public async Task<IActionResult> CollaterlUpdate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientloansP = await _context.Loans.FindAsync(id);
            if (clientloansP == null)
            {
                return NotFound();
            }
            return View(clientloansP);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CollaterlUpdate(int id, ClientloansP clientloansP)
        {
            if (id != clientloansP.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(clientloansP);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientloansPExists(clientloansP.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return View("Updates");


        }





    }
}
