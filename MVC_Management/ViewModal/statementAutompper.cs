using MVC_Management.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Management.ViewModal
{
    public class statementAutompper
    {



        [Key]
        public int Id { get; set; }



        [DisplayName("Client Id")]
        [Required]
        [StringLength(13)]
        public string ClientID { get; set; }
        public Registration clients { get; set; }




        [StringLength(10, MinimumLength = 10, ErrorMessage = "The Length of the Id Number must be 10")]
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }


        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime? LoanDate { get; set; }



        [DisplayName("loan Type")]
        public string? loanType { get; set; }



        [DisplayName("loan Amount")]
        public Decimal? loanAmount { get; set; }



        public Decimal? Interest { get; set; } = decimal.Zero;

        [DisplayName("Amount Due")]

        public Decimal? AmountDue { get; set; } = decimal.Zero;
        [DisplayName("Due Date")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Payment Amount")]
        public Decimal? PaymentAmount { get; set; }

        public Decimal? Balance { get; set; }

        public string? Comments { get; set; }


    }
}
