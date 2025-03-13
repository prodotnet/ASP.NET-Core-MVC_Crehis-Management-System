

using Microsoft.AspNetCore.Mvc;
using MVC_Management.Data;
using MVC_Management.Models;
using System.Diagnostics;

namespace MVC_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;





        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            DateTime TDate = DateTime.Now;
            DateOnly todayDate = DateOnly.FromDateTime(TDate);

            ViewBag.NewLoans = _context.Loans.Where(x => x.Status == "Not Paid" || x.Status == " Not Paid").Count();
            ViewBag.TodayLoans = _context.Loans.Where(x => DateOnly.FromDateTime(x.LoanDate) == todayDate && x.Status == "Not Paid" || x.Status == " Not Paid").Count();
            ViewBag.LoansdueToday = _context.Loans.Where(x => DateOnly.FromDateTime(x.DueDate) == todayDate && x.Status == "Not Paid" || x.Status == " Not Paid").Count();
            ViewBag.Registrations = _context.Registration.Count();




            //montly goals
            var currentMonth = TDate.Month;
            var currentYear = TDate.Year;
            var monthlyTarget = _context.MonthlyTargets
                .FirstOrDefault(mt => mt.Month.Month == currentMonth && mt.Month.Year == currentYear);


            ViewBag.IsTargetNotSet = monthlyTarget == null;


            ViewBag.MonthlyTarget = monthlyTarget?.TargetAmount ?? 0;
            ViewBag.LoansDistributed = _context.Loans
                .Where(x => x.LoanDate.Month == currentMonth && x.LoanDate.Year == currentYear)
                .Sum(x => x.AmountDue);


            ViewBag.loansNeeded = ViewBag.monthlyTarget - ViewBag.loansDistributed;
            // Loans Due Today with Address
            var loansDueToday = _context.Loans
                .Where(x => DateOnly.FromDateTime(x.DueDate) == todayDate && x.Status == "Not Paid" || x.Status == " Not Paid")
                .Select(loan => new
                {
                    loan.ClientID,
                    loan.PhoneNumber,
                    loan.FullName,
                    loan.loanAmount,
                    loan.AmountDue,
                    loan.DueDate,
                    Address = _context.Registration
                        .Where(r => r.ID == loan.ClientID)
                        .Select(r => r.Address)
                        .FirstOrDefault(),


                    AlternativeNumber = _context.Registration
                        .Where(r => r.ID == loan.ClientID)
                        .Select(r => r.Al_Phone)
                        .FirstOrDefault(),

                    Balance = _context.Registration
                        .Where(r => r.ID == loan.ClientID)
                        .Select(r => r.Amount_due)
                        .FirstOrDefault()

                })
                .ToList();

            ViewBag.LoansDueTodayList = loansDueToday;

            return User.Identity.IsAuthenticated ? this.Redirect("~/identity/account/login") : View();


        }

        public class MonthlyData
        {
            public string Month { get; set; }
            public decimal TargetAmount { get; set; }
            public decimal DistributedAmount { get; set; }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetTarget()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SetTarget(MonthlyTarget target)
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var existingTarget = _context.MonthlyTargets
                .FirstOrDefault(mt => mt.Month.Month == currentMonth && mt.Month.Year == currentYear);

            if (existingTarget == null)
            {
                target.Month = new DateTime(currentYear, currentMonth, 1);
                _context.MonthlyTargets.Add(target);
                _context.SaveChanges();
            }
            else
            {
                existingTarget.TargetAmount = target.TargetAmount;
                _context.MonthlyTargets.Update(existingTarget);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }


    }
}


