using AutoMapper;
using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Data.UnitOfWork;
using MultipleChoise.Server.Service.Interface;

namespace MultipleChoise.Server.Service
{
    public class QuestionService(IUnitOfWork unitOfWork, IMapper mapper) : ICrudService<QuestionDto>
    {
        public async Task<QuestionDto?> GetByIdAsync(Guid id)
        {
            return mapper.Map<QuestionDto>(await unitOfWork.QuestionRepo.GetByIdAsync(id));
        }

        public async Task<IEnumerable<QuestionDto>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<QuestionDto>>(await unitOfWork.QuestionRepo.GetAllAsync());
        }

        public async Task CreateAsync(QuestionDto question)
        {
            await unitOfWork.QuestionRepo.AddAsync(mapper.Map<Question>(question));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuestionDto question)
        {
            unitOfWork.QuestionRepo.Update(mapper.Map<Question>(question));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var question = await unitOfWork.QuestionRepo.GetByIdAsync(id);
            unitOfWork.QuestionRepo.Delete(question);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
