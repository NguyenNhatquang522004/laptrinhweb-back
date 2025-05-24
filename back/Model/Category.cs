using System.ComponentModel.DataAnnotations;

namespace backapi.Model
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? IconUrl { get; set; }

        [StringLength(7)]
        public string? ColorCode { get; set; }

        public int? SortOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Vocabulary> Vocabularies { get; set; } = new List<Vocabulary>();
    }
}
