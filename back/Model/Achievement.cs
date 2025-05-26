using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class Achievement : BaseEntity
    {
        [Key]
        public Guid AchievementId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? IconUrl { get; set; }

        [StringLength(7)]
        public string? BadgeColor { get; set; }

        public AchievementCategory? Category { get; set; }

        [Column(TypeName = "json")]
        public string? Criteria { get; set; } // conditions to unlock

        public int PointsReward { get; set; } = 0;

        public AchievementRarity? Rarity { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
        public ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();
    }
}
