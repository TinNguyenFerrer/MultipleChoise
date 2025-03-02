namespace MultipleChoise.Server.Contract.Dto.UserDto
{
    public class QuestionUserDto : BaseEntityDto
    {
        public string Content { get; set; } // Question text
        public ICollection<AnswerUserDto> Answers { get; set; }
        public Guid QuizId { get; set; }
    }
}
