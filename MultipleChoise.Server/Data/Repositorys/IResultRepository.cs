using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Data.Models.Entity;

namespace MultipleChoise.Server.Data.Repositorys
{
    public interface IResultRepository: IBaseRepository<Result>
    {
        Task<ICollection<Result>> GetByQuizId(Guid id);
    }
}
