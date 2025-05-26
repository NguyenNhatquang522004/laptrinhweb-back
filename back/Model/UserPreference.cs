using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class UserPreference : BaseEntity
    {
        [Key]
        public Guid PreferenceId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public LearningGoal? LearningGoal { get; set; }

        public int DailyGoalMinutes { get; set; } = 30;

        public bool NotificationsEnabled { get; set; } = true;

        public bool EmailNotifications { get; set; } = true;

        public bool PushNotifications { get; set; } = true;

        public TimeSpan? StudyReminderTime { get; set; }

        public PreferredDifficulty PreferredDifficulty { get; set; } = PreferredDifficulty.Adaptive;

        public bool DarkMode { get; set; } = false;

        public FontSize FontSize { get; set; } = FontSize.Medium;

        [Column(TypeName = "decimal(2,1)")]
        public decimal AudioSpeed { get; set; } = 1.0m;

        public bool SubtitlesEnabled { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User User { get; set; } = null!;


        public UserPreference()
        {
            // Default constructor
        }
    }

}
