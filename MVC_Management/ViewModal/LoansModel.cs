using MVC_Management.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Management.ViewModal
{
    public class LoansModel
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

        public decimal PaymentAmount { get; set; }
        public decimal Oustanding { get; set; }
        public DateTime PaymentDate { get; set; }

        public ClientloansP client_Loans { get; set; }

        public List<ClientloansP> Allloans { get; set; }


        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? FullName
        {
            get
            {
                return $"{Name}  {Surname}".Trim();

            }
        }
        public string Model { get; set; }

        [DisplayName("Serial Number")]
        public string SerialNumber { get; set; }


        public string Condition { get; set; }


        public List<Registration> Allclients { get; set; }


        public  decimal TotalAmountDue { get; set; }

    }
}
