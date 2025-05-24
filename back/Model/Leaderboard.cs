using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class Leaderboard
    {
        [Key]
        public Guid LeaderboardId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public LeaderboardPeriod Period { get; set; }

        public DateTime? PeriodStart { get; set; }

        public DateTime? PeriodEnd { get; set; }

        public int TotalPoints { get; set; } = 0;

        public int LessonsCompleted { get; set; } = 0;

        public int TimeStudied { get; set; } = 0;

        public int? RankPosition { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User User { get; set; } = null!;
    }
}
