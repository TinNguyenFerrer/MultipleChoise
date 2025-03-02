using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Contract.Dto.UserDto;
using MultipleChoise.Server.Data.Models.Entity;

namespace MultipleChoise.Server.Service.Interface
{
    public interface IQuizService
    {
        Task<QuizUserDto?> GetByNumberAsync(int number);

        Task<QuizDto> UpdateQuizAsync(QuizDto quizDto);

        Task<QuizDto?> GetByIdAsync(Guid id);

        Task<QuizDto> CreateAndReturnAsync(QuizDto quiz);
    }
}
