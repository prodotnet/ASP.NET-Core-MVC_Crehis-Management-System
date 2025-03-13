
using System.ComponentModel.DataAnnotations;

namespace MVC_Management.Models
{
    public class MonthlyTarget
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal TargetAmount { get; set; }
        [Required]
        public DateTime Month { get; set; }
    }
}
