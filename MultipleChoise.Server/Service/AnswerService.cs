using AutoMapper;
using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Data.UnitOfWork;
using MultipleChoise.Server.Service.Interface;

namespace MultipleChoise.Server.Service
{
    public class AnswerService(IUnitOfWork unitOfWork, IMapper mapper) : ICrudService<AnswerDto>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<AnswerDto?> GetByIdAsync(Guid id)
        {
            return mapper.Map<AnswerDto>(await _unitOfWork.AnswerRepo.GetByIdAsync(id)); ;
        }

        public async Task<IEnumerable<AnswerDto>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<AnswerDto>>( await _unitOfWork.AnswerRepo.GetAllAsync());
        }

        public async Task CreateAsync(AnswerDto answer)
        {
            await _unitOfWork.AnswerRepo.AddAsync(mapper.Map<Answer>(answer));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnswerDto answer)
        {
            _unitOfWork.AnswerRepo.Update(mapper.Map<Answer>(answer));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var answer = await _unitOfWork.AnswerRepo.GetByIdAsync(id);
            _unitOfWork.AnswerRepo.Delete(answer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
