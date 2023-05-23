using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LostFound.Core.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [ForeignKey("CategoryId")]
        public string CategoryId { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [NotNull]
        public string Title { get; set; } = null!;

        [Required]
        public string Text { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        [Required, NotNull]
        public DateTime? Date { get; set; }

        public bool IsFound { get; set; }

        [Required] public string City { get; set; } = null!;

        [Required]
        [NotNull]
        public string Address { get; set; } = null!;

    }
}
