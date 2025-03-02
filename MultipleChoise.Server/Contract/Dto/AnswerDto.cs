using MultipleChoise.Server.Data.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultipleChoise.Server.Contract.Dto
{
    public class AnswerDto : BaseEntityDto
    {
        public string Content { get; set; } // Answer text
        public bool IsCorrect { get; set; } // Correctness of the answer
        public Guid QuestionId { get; set; }
        public bool IsSelected { get; set; }
    }
}
