using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class Exercise
    {
        [Key]
        public Guid ExerciseId { get; set; } = Guid.NewGuid();

        [ForeignKey("Lesson")]
        public Guid? LessonId { get; set; }

        [StringLength(200)]
        public string? Title { get; set; }

        public string? Instructions { get; set; }

        public ExerciseType ExerciseType { get; set; }

        [Column(TypeName = "json")]
        public string? Content { get; set; } // exercise data structure

        [Column(TypeName = "json")]
        public string? CorrectAnswers { get; set; }

        public int Points { get; set; } = 5;

        public int? TimeLimit { get; set; } // seconds

        public Difficulty? Difficulty { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Lesson? Lesson { get; set; }
    }
}
