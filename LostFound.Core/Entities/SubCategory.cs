using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LostFound.Core.Entities
{
    public class SubCategory
    {
        [Key]
        public string SubCategoryId { get; set; } = null!;

        [NotNull]
        public string SubCategoryName { get; set; } = null!;

        public string categoryId { get; set; } = null!;

    }
}
