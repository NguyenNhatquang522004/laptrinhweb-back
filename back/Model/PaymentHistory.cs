using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class PaymentHistory
    {
        [Key]
        public Guid PaymentId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Subscription")]
        public Guid? SubscriptionId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [StringLength(3)]
        public string Currency { get; set; } = "USD";

        [StringLength(50)]
        public string? PaymentMethod { get; set; }

        [StringLength(100)]
        public string? TransactionId { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }

        // Payment Gateway specific fields
        [StringLength(100)]
        public string? GatewayTransactionId { get; set; }

        [StringLength(50)]
        public string? Gateway { get; set; } // Stripe, PayPal, etc.

        public string? GatewayResponse { get; set; }

        public DateTime? ProcessedAt { get; set; }

        public string? FailureReason { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public Subscription? Subscription { get; set; }
    }
}
