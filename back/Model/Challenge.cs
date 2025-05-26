using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class Challenge : BaseEntity
    {
        [Key]
        public Guid ChallengeId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ChallengeType ChallengeType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? TargetValue { get; set; }

        public ChallengeMetric? Metric { get; set; }

        public int? RewardPoints { get; set; }

        [ForeignKey("RewardBadge")]
        public Guid? RewardBadgeId { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Achievement? RewardBadge { get; set; }
        public ICollection<UserChallenge> UserChallenges { get; set; } = new List<UserChallenge>();
    }
}
