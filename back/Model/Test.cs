using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class Test : BaseEntity
    {
        [Key]
        public Guid TestId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public TestType TestType { get; set; }

        public CefrLevel? Level { get; set; }

        public int? TotalQuestions { get; set; }

        public int? TimeLimit { get; set; } // minutes

        [Column(TypeName = "decimal(5,2)")]
        public decimal? PassingScore { get; set; }

        public int MaxAttempts { get; set; } = 3;

        public bool IsPremium { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
        public ICollection<UserTestResult> UserTestResults { get; set; } = new List<UserTestResult>();
    }
}
