using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class Lesson : BaseEntity
    {
        [Key]
        public Guid LessonId { get; set; } = Guid.NewGuid();

        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Column(TypeName = "longtext")]
        public string? Content { get; set; }

        [Required]
        public CefrLevel Level { get; set; }

        public LessonType? LessonType { get; set; }

        public int? EstimatedDuration { get; set; } // minutes

        public int PointsReward { get; set; } = 10;

        [Column(TypeName = "json")]
        public string? Prerequisites { get; set; } // JSON array of lesson IDs

        public string? ThumbnailUrl { get; set; }

        public string? VideoUrl { get; set; }

        public string? AudioUrl { get; set; }

        public string? Transcript { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal? DifficultyScore { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal PopularityScore { get; set; } = 0;

        public bool IsPremium { get; set; } = false;

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public Category? Category { get; set; }
        public User? CreatedByUser { get; set; }
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
        public ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
    }
}
