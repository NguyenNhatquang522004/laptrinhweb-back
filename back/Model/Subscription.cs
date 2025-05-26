using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class Subscription : BaseEntity
    {
        [Key]
        public Guid SubscriptionId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public PlanType PlanType { get; set; } = PlanType.Free;

        public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public DateTime? EndDate { get; set; }

        public bool AutoRenew { get; set; } = true;

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Amount { get; set; }

        [StringLength(3)]
        public string Currency { get; set; } = "USD";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User User { get; set; } = null!;
        public ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();
    }
}
