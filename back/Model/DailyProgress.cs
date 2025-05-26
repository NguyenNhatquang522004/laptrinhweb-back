using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backapi.Model
{
    public class DailyProgress : BaseEntity
    {
        [Key]
        public Guid DailyId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int LessonsCompleted { get; set; } = 0;

        public int ExercisesCompleted { get; set; } = 0;

        public int TimeStudied { get; set; } = 0; // minutes

        public int PointsEarned { get; set; } = 0;

        public int WordsLearned { get; set; } = 0;

        public bool StreakMaintained { get; set; } = false;

        public bool GoalsMet { get; set; } = false;

        // Navigation Properties
        public User User { get; set; } = null!;
    }
}
