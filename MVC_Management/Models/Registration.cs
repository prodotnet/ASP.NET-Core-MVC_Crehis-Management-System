using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Management.Models
{
    public class Registration
    {
        [Key]
        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "The Length of the ID Number must be 13")]
        public string ID { get; set; }
        
        
        public string? Photo {  get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The Length of the Phone Number must be 10")]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        [DisplayName("Alternative Number")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The Length of the Phone Number must be 10")]
        public string Al_Phone { get; set; }
      

        [DisplayName("Referance Name")]
        public string? ReferanceName { get; set; }

        [DisplayName("Referance Number")]
        public string? ReferanceNumber { get; set; }





        [DisplayName("Work Details")]
        public string? Work { get; set; }



        public string Gender { get; set; }
        [Required]
        [DisplayName("Total Amount Due")]
        
        public Decimal Amount_due { get; set; }

      
    }
}
