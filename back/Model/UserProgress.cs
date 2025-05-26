using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class UserProgress : BaseEntity
    {
        [Key]
        public Guid ProgressId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Lesson")]
        public Guid LessonId { get; set; }

        public ProgressStatus Status { get; set; } = ProgressStatus.NotStarted;

        [Column(TypeName = "decimal(5,2)")]
        public decimal CompletionPercentage { get; set; } = 0;

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Score { get; set; }

        public int? TimeSpent { get; set; } // minutes

        public int Attempts { get; set; } = 0;

        public DateTime? FirstAttemptAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public DateTime LastAccessed { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User User { get; set; } = null!;
        public Lesson Lesson { get; set; } = null!;
    }
}
