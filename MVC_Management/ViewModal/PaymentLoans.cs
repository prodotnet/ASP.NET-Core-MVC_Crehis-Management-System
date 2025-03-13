using MVC_Management.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Management.ViewModal
{
    public class PaymentLoans
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

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DisplayName("loan Amount")]
        public Decimal loanAmount { get; set; }

        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public Decimal Interest { get; set; } 

        [DisplayName("Amount Due")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public Decimal AmountDue { get; set; } 
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        public string Status { get; set; }

        public string Comments { get; set; }


        [DisplayName("Payment Amount")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public Decimal PaymentDue { get; set; }




        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DisplayName("Payment Amount")]
        [Required]
        public Decimal PaymentAmount { get; set; }

        [DisplayName("Balance")]
        [Required]
        public Decimal Balance { get; set; } 


        [DisplayName("Reference number")]
        public int RefNum { get; set; }

       
       

        public ClientloansP client_Loans { get; set; }

        public List<ClientloansP> Allloans { get; set; }



        public ClientsLoansRecords LoansRecords { get; set; }

        public List<ClientsLoansRecords> LoanRecords { get; set; }

        public Payment Moda_Payments { get; set; }
        public List<Payment> Allpayment { get; set; }

        public Statement State { get; set; }
        public List<Statement> Statements{ get; set; }


    }
}
