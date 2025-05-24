using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backapi.Model
{
    public class UserTestResult
    {
        [Key]
        public Guid ResultId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Test")]
        public Guid TestId { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Score { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? MaxScore { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Percentage { get; set; }

        public int? TimeTaken { get; set; } // minutes

        [Column(TypeName = "json")]
        public string? Answers { get; set; }

        [Column(TypeName = "json")]
        public string? SkillBreakdown { get; set; } // scores by skill

        [Column(TypeName = "json")]
        public string? Recommendations { get; set; }

        public int? AttemptNumber { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public bool? IsPassed { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public Test Test { get; set; } = null!;
    }
}
