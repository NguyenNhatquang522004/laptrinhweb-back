using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backapi.Model
{
    public class UserChallenge : BaseEntity
    {
        [Key]
        public Guid UserChallengeId { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Challenge")]
        public Guid ChallengeId { get; set; }

        public int CurrentProgress { get; set; } = 0;

        public bool IsCompleted { get; set; } = false;

        public DateTime? CompletedAt { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User User { get; set; } = null!;
        public Challenge Challenge { get; set; } = null!;
    }
}
