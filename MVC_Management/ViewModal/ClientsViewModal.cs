using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MVC_Management.Models;

namespace MVC_Management.ViewModal
{
    public class ClientsViewModal
    {


        [Key]
        [Required]
        [StringLength(13)]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Alter/Work Number")]
        public string Al_Phone { get; set; }
        [Required]


        public string Gender { get; set; }
        [Required]
        [DisplayName("Total Amount Due")]

        public Decimal Amount_due { get; set; }

        public string? Photo { get; set; }

        public Registration clients { get; set; }

        public List<Registration> Allclients { get; set; }







    }
}
