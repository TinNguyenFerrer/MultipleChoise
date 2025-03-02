using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultipleChoise.Server.Data.Models.Entity
{
    public class Question : BaseEntity
    {
        public string Content { get; set; } // Question text
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

        // Foreign key to Quiz
        [Required]
        [ForeignKey(nameof(Quiz))]
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
