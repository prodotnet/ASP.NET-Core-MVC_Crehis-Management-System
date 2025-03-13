using MVC_Management.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Management.ViewModal
{
    public class PerLinePaymentViewModel
    {

        [Key]
        public int Id { get; set; }

        [DisplayName("Client Id")]
        [Required]

        [StringLength(13)]
        public string ClientID { get; set; }
        public Registration clients { get; set; }



        [StringLength(10)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }



        [DisplayName("loan Type")]
        public string loanType { get; set; }

        [DisplayName("loan Amount")]
        public decimal loanAmount { get; set; }

        [DisplayName("loan Date")]
        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        public decimal Interest { get; set; } = decimal.Zero;

        [DisplayName("Amount Due")]

        public decimal AmountDue { get; set; } = decimal.Zero;
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        public string Status { get; set; }

        public string? Comments { get; set; }


        [DisplayName("Payment Amount")]
        [Required]
        public decimal PaymentDue { get; set; } = decimal.Zero;


        [DisplayName("Balance")]
        [Required]
        public decimal Balance { get; set; } = decimal.Zero;


        public ClientloansP client_Loans { get; set; }

        public List<ClientloansP> Allloans { get; set; }


        public Payment Payments { get; set; }
        public List<Payment> Allpayment { get; set; }
    }
}
