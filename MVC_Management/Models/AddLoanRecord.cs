using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Management.Models
{
    public class AddLoanRecord
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Client Id")]
        [Required]

        [StringLength(13)]
        public string ClientID { get; set; }
        public Registration clients { get; set; }

        [StringLength(13)]
        [DisplayName("loan Type")]
        public string PhoneNumber {  get; set; }



        [DisplayName("loan Type")]
        public string loanType { get; set; }

        [DisplayName("loan Amount")]
        public Decimal loanAmount { get; set; }

        [DisplayName("loan Date")]
        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; } = DateTime.Now;
      
        public Decimal Interest { get; set; }

        [DisplayName("Amount Due")]
        
        public Decimal AmountDue { get; set; } 
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        public string Status { get; set; }

        public string Comments { get; set; }
    }
}
