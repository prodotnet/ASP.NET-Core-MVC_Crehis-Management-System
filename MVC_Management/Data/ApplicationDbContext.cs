using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Management.Models;

namespace MVC_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public DbSet<Registration> Registration {  get; set; }
       

        public DbSet<AddLoanRecord> AddLoanRecords { get; set; }

        public DbSet<ClientloansP> Loans{ get; set; }

        public DbSet<ClientsLoansRecords> ClientRecords { get; set; }

        public DbSet<Payment> Payments { get; set; }



        public DbSet<MonthlyTarget> MonthlyTargets { get; set; }

        public DbSet<Statement> LoanPaymentStatement { get; set; }

        public DbSet<CollateralRegister> CollateralRegistration { get; set; }

    }
}
