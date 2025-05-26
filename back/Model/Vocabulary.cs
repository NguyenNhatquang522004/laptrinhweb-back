using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static backapi.Enums.enums;

namespace backapi.Model
{
    public class Vocabulary : BaseEntity
    {
        [Key]
        public Guid WordId { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string Word { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Pronunciation { get; set; }

        [StringLength(200)]
        public string? Phonetic { get; set; }

        public PartOfSpeech? PartOfSpeech { get; set; }

        [Required]
        public string DefinitionEn { get; set; } = string.Empty;

        public string? DefinitionNative { get; set; }

        public string? ExampleSentence { get; set; }

        public string? ImageUrl { get; set; }

        public string? AudioUrl { get; set; }

        public CefrLevel? Level { get; set; }

        public int? FrequencyRank { get; set; }

        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }

        [Column(TypeName = "json")]
        public string? Synonyms { get; set; }

        [Column(TypeName = "json")]
        public string? Antonyms { get; set; }

        [Column(TypeName = "json")]
        public string? Collocations { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Category? Category { get; set; }

    }
}
