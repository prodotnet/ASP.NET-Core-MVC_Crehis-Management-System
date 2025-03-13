using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Management.Models
{
    public class ClientsLoansRecords
    {
        [Key]
        [DisplayName("Reference Number")]

        public int Id { get; set; }

        public int? LoanId { get; set; }

        public ClientloansP loansDetails { get; set; }

        public int? PaymentId { get; set; }

        public Payment paymentDeatials { get; set; }

       


        [DisplayName("Client Id")]
        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "The Length of the Id Number must be 13")]
        public string? ClientID { get; set; }

       
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The Length of the Id Number must be 10")]
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }


        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime? LoanDate { get; set; } = DateTime.Now;



        [DisplayName("loan Type")]
        public string? loanType { get; set; }



        [DisplayName("loan Amount")]
        public decimal? loanAmount { get; set; }

        

        public Decimal? Interest { get; set; } 
        [DisplayName("Amount Due")]

        public Decimal? AmountDue { get; set; }
        [DisplayName("Due Date")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Payment Amount")]
        public Decimal? PaymentAmount { get; set; }

        public Decimal? Balance { get; set; }

        public string? Comments { get; set; }


       


    }
}
