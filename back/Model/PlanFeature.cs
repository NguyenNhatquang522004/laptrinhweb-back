using System.ComponentModel.DataAnnotations;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class PlanFeature : BaseEntity
    {
        [Key]
        public Guid FeatureId { get; set; } = Guid.NewGuid();

        public PlanType PlanType { get; set; }

        [Required]
        [StringLength(100)]
        public string FeatureName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsEnabled { get; set; } = true;

        public int? LimitValue { get; set; } // For features with limits (e.g., max lessons per day)

        [StringLength(50)]
        public string? LimitType { get; set; } // daily, monthly, total

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
