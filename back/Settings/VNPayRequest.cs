using System.ComponentModel.DataAnnotations;

namespace backapi.Settings
{
    public class VNPayRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid? SubscriptionId { get; set; }

        [Required]
        [Range(1000, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 1000 VND")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(255)]
        public string OrderInfo { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
