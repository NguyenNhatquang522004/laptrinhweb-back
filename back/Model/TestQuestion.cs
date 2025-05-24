using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class TestQuestion
    {
        [Key]
        public Guid QuestionId { get; set; } = Guid.NewGuid();

        [ForeignKey("Test")]
        public Guid TestId { get; set; }

        public int? QuestionNumber { get; set; }

        [Required]
        public string QuestionText { get; set; } = string.Empty;

        public QuestionType QuestionType { get; set; }

        [Column(TypeName = "json")]
        public string? Options { get; set; }

        public string? CorrectAnswer { get; set; }

        public string? Explanation { get; set; }

        public int Points { get; set; } = 1;

        public SkillFocus? SkillFocus { get; set; }

        public Difficulty? Difficulty { get; set; }

        public string? AudioUrl { get; set; }

        public string? ImageUrl { get; set; }

        // Navigation Properties
        public Test Test { get; set; } = null!;
    }
}
