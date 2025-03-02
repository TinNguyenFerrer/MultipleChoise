using MultipleChoise.Server.Data.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MultipleChoise.Server.Contract.Dto
{
    public class QuestionDto : BaseEntityDto
    {
        public string Content { get; set; } // Question text
        public ICollection<AnswerDto> Answers { get; set; }
        public Guid QuizId { get; set; }
    }
}
