using MultipleChoise.Server.Contract.Share.Enum;
using MultipleChoise.Server.Data.Models.Entity;

namespace MultipleChoise.Server.Contract.Dto
{
    public class QuizDto: BaseEntityDto
    {
        public string Title { get; set; } // Title of the quiz
        public int DurationInMinutes { get; set; } // Quiz duration
        public ICollection<QuestionDto> Questions { get; set; }

        public int UniqueNumber { get; set; }

        public QuizStatus Status { get; set; }
    }
}
