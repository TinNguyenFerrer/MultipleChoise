using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultipleChoise.Server.Data.Models.Entity
{
    public class Result : BaseEntity
    {
        public string StudentName { get; set; } // Student's name
        public string StudentId { get; set; } // Student's ID
        public int CorrectAnswers { get; set; } // Number of correct answers
        public double Score { get; set; } // Final score

        // Foreign key to Quiz
        [Required]
        [ForeignKey(nameof(Quiz))]
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
