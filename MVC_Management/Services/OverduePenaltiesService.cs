using Humanizer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MVC_Management.Data;
using MVC_Management.Models;
using System.Xml.Linq;

namespace MVC_Management.Services
{
    public class OverduePenaltiesService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<OverduePenaltiesService> _logger;
        private readonly TimeSpan _interval;

        public OverduePenaltiesService(IServiceScopeFactory serviceScopeFactory, ILogger<OverduePenaltiesService> logger, IConfiguration configuration)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _interval = TimeSpan.FromHours(double.Parse(configuration["OverduePenalties:IntervalInHours"] ?? "1"));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OverduePenaltiesService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ApplyOverduePenaltiesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while applying overdue penalties.");
                }

                await Task.Delay(_interval, stoppingToken);
            }

            _logger.LogInformation("OverduePenaltiesService is stopping.");
        }


        private async Task ApplyOverduePenaltiesAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                decimal New_AmountDue;
                decimal LAmount;
                decimal Rollover_Interest;
                decimal oldloan_inter;
                string Comment;
                string type;
                DateTime Tdate;
                DateTime TDate;

                var overdueLoans = await _context.Loans
                    .Where(x => x.DueDate.AddDays(2) < DateTime.Now && (x.Status == "Not Paid"))
                    .ToListAsync(stoppingToken);

                foreach (var loan in overdueLoans)
                {
                    var registration = await _context.Registration
                        .FirstOrDefaultAsync(r => r.ID == loan.ClientID, stoppingToken);

                    if (registration != null)
                    {
                        New_AmountDue = loan.AmountDue;
                        LAmount = loan.AmountDue;
                        Comment = loan.Comments;
                        Tdate = loan.DueDate;
                        TDate = loan.LoanDate;
                        type = loan.loanType;



                        Rollover_Interest = loan.AmountDue * 0.5m;
                        oldloan_inter = loan.Interest;
                        New_AmountDue += Rollover_Interest;

                      
                        loan.Status = "Rollover";
                        registration.Amount_due += Rollover_Interest;


                        var rolloverLoan = new ClientloansP
                        {
                            ClientID = registration.ID,
                            PhoneNumber = loan.PhoneNumber,
                            loanType = "Rollover",
                            LoanDate = TDate,
                            loanAmount = LAmount, // Reflects the original loan amount
                            Name = registration.Name,
                            Surname = registration.Surname,
                            Interest = Rollover_Interest,
                            AmountDue = New_AmountDue,
                            DueDate = DateTime.Now.AddMonths(1),
                            Comments = "Rollover due to Missed Payment.",
                            Status = "Not Paid",
                            Collateral = loan.Collateral,
                            Balance = 0
                        };

                        _context.Add(rolloverLoan);
                        _context.Update(loan);
                    }
                }

                await _context.SaveChangesAsync(stoppingToken);
            }
        }
    }
}
