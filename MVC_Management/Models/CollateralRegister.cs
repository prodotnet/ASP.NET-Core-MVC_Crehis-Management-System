using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVC_Management.Models
{
    public class CollateralRegister
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


        public string Model { get; set; }

        [DisplayName("Serial Number")]
        public string SerialNumber { get; set; }

        public string Condition { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        // 
        public string Comments { get; set; }
    }
}
