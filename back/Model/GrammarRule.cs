using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class GrammarRule : BaseEntity
    {
        [Key]
        public Guid RuleId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Column(TypeName = "longtext")]
        public string? Explanation { get; set; }

        public CefrLevel? Level { get; set; }

        [StringLength(100)]
        public Guid? CategoryId { get; set; } // tenses, conditionals, etc.

        [Column(TypeName = "json")]
        public string? Examples { get; set; }

        [Column(TypeName = "json")]
        public string? CommonMistakes { get; set; }

        [Column(TypeName = "json")]
        public string? Tips { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Navigation Properties
        public Category? Category { get; set; } // Navigation to Category entity
    }
}
