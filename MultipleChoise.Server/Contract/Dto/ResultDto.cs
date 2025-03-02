using MultipleChoise.Server.Data.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultipleChoise.Server.Contract.Dto
{
    public class ResultDto
    {
        public string StudentName { get; set; } // Student's name
        public string StudentId { get; set; } // Student's ID
        public int CorrectAnswers { get; set; } // Number of correct answers
        public double Score { get; set; } // Final score
        public Guid QuizId { get; set; }
        public QuizDto Quiz { get; set; }
    }
}
