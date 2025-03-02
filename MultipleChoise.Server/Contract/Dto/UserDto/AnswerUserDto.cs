namespace MultipleChoise.Server.Contract.Dto.UserDto
{
    public class AnswerUserDto: BaseEntityDto
    {
        public string Content { get; set; } // Answer text
        public Guid QuestionId { get; set; }
        public bool IsSelected { get; set; }
    }
}
