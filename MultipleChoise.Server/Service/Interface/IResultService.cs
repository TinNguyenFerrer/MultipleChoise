using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Data.Models.Entity;

namespace MultipleChoise.Server.Service.Interface
{
    public interface IResultService
    {
        Task<ResultDto> GetResult(ResultDto resultDto);
        Task<IEnumerable<ResultDto>> GetAllAsync();
        Task<IEnumerable<ResultDto>> GetByQuizId(Guid quizId);
    }
}
