using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostFound.Core.Entities
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [ForeignKey("UserId")]
        [Required(ErrorMessage = "Admin UserId is empty!")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Admin Power is empty!")]
        public int Power { get; set; }
    }
}
