using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Management.Models
{
    public class Payment
    {

        [Key]
        public int Id { get; set; }

        [DisplayName("Client Id")]
        [Required]

        [StringLength(13)]
        public string ClientID { get; set; }
        public Registration clients { get; set; }

        [DisplayName("Reference number")]
        public int RefNum { get; set; }


        [StringLength(10)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DisplayName("Payment due")]
        [Required]
        public Decimal  PaymentDue { get; set; }


        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DisplayName("Payment Amount")]
        [Required]
        public Decimal PaymentAmount { get; set; }


        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [DisplayName("Balance")]
        [Required]
        public Decimal Balance { get; set; } 



        [DisplayName("Date")]
        [DataType(DataType.Date)]
        public DateTime LoanDate { get; set; } = DateTime.Now;


        public string Comments { get; set; }



    }
}
