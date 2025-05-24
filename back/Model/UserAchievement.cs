using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backapi.Model
{
    public class UserAchievement
    {
        [Key]
        public Guid UserAchievementId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Achievement")]
        public Guid AchievementId { get; set; }

        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

        public bool IsDisplayed { get; set; } = true;

        // Navigation Properties
        public User User { get; set; } = null!;
        public Achievement Achievement { get; set; } = null!;

    }
}
