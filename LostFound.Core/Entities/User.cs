using System.ComponentModel.DataAnnotations;

namespace LostFound.Core.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        public string ProfilePicture { get; set; } = null!;

        public string phoneNumber { get; set; } = null!;

        [Required] public string Password { get; set; } = null!;

        public bool EmailVerified { get; set; }

        public int Age { get; set; }

    }
}
