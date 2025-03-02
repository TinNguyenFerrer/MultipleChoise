using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultipleChoise.Server.Data.Models.Entity
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; } // Answer text
        public bool IsCorrect { get; set; } // Correctness of the answer

        // Foreign key to Question
        [Required]
        [ForeignKey(nameof(Question))]
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
