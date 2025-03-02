namespace MultipleChoise.Server.Contract.Dto.UserDto
{
    public class QuizUserDto : BaseEntityDto
    {
        public string Title { get; set; } // Title of the quiz
        public int DurationInMinutes { get; set; } // Quiz duration
        public ICollection<QuestionUserDto> Questions { get; set; }

        public int UniqueNumber { get; set; }
    }
}
