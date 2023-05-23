using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LostFound.Core.Entities
{
    public class Category
    {
        [Key]
        public string CategoryId { get; set; } = null!;

        [Required]
        [NotNull]
        public string Name { get; set; } = null!;

        public int PostCount { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; } = null!;

    }
}
