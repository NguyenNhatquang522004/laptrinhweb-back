using System.ComponentModel.DataAnnotations;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class User : BaseEntity
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        public string prvider { get; set; } = "Local"; // Google, Facebook, etc.

        public string providerId { get; set; } = string.Empty; // Unique ID from the provider
        public bool EmailVerified { get; set; }

        public string? PhoneNumber { get; set; }
        public DateTime? LastLoginDate { get; set; }
        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        public string code { get; set; } = string.Empty;

        public DateTime CodeExpiration { get; set; }

        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }

        [StringLength(50)]
        public string? NativeLanguage { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(50)]
        public string? Timezone { get; set; }

        public string? ProfileImageUrl { get; set; }

        public string? Bio { get; set; }

        public bool IsPremium { get; set; } = false;

        public CefrLevel CurrentLevel { get; set; } = CefrLevel.A1;

        public int TotalPoints { get; set; } = 0;

        public int StreakDays { get; set; } = 0;

        public DateTime? LastActiveDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        //Navigation Properties
        public UserPreference? UserPreference { get; set; }
        public ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
        public ICollection<UserTestResult> UserTestResults { get; set; } = new List<UserTestResult>();
        public ICollection<DailyProgress> DailyProgresses { get; set; } = new List<DailyProgress>();
        public ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

        public ICollection<UserChallenge> UserChallenges { get; set; } = new List<UserChallenge>();


        public ICollection<PaymentHistory> PaymentHistories { get; set; } = new List<PaymentHistory>();

    }
}

