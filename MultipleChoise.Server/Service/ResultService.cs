using AutoMapper;
using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Data.UnitOfWork;
using MultipleChoise.Server.Service.Interface;
using System.Net.WebSockets;

namespace MultipleChoise.Server.Service
{
    public class ResultService(IUnitOfWork unitOfWork, IMapper mapper) : ICrudService<ResultDto>, IResultService
    {
        public async Task<ResultDto?> GetByIdAsync(Guid id)
        {
            return mapper.Map<ResultDto>(await unitOfWork.ResultRepo.GetByIdAsync(id));
        }

        public async Task<IEnumerable<ResultDto>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<ResultDto>>(await unitOfWork.ResultRepo.GetAllAsync());
        }

        public async Task CreateAsync(ResultDto result)
        {
            await unitOfWork.ResultRepo.AddAsync(mapper.Map<Result>(result));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ResultDto result)
        {
            unitOfWork.ResultRepo.Update(mapper.Map<Result>(result));
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var result = await unitOfWork.ResultRepo.GetByIdAsync(id);
            unitOfWork.ResultRepo.Delete(result);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<ResultDto> GetResult(ResultDto resultDto)
        {
            if (resultDto?.Quiz?.Id == null)
            {
                throw new ArgumentException("Quiz data is invalid.");
            }

            var quizDto = resultDto.Quiz;
            var dbQuiz = await unitOfWork.QuizRepo.GetQuizWithQuestionsAndAnswersAsync(quizDto.Id);
            if (dbQuiz?.Questions == null || !dbQuiz.Questions.Any())
            {
                throw new ArgumentException("Quiz data is invalid.");
            }

            int correctCount = 0;

            foreach (var question in dbQuiz.Questions)
            {
                var userQuestion = quizDto.Questions?.FirstOrDefault(q => q.Id == question.Id);
                if (userQuestion == null) continue;

                var correctAnswer = question.Answers?.FirstOrDefault(a => a.IsCorrect);
                var selectedAnswer = userQuestion.Answers?.FirstOrDefault(a => a.IsSelected);

                if (correctAnswer != null && selectedAnswer?.Id == correctAnswer.Id)
                {
                    correctCount++;
                }
            }

            resultDto.CorrectAnswers = correctCount;
            resultDto.Score = Math.Round((double)correctCount / dbQuiz.Questions.Count() * 10, 2); // Calculate score with 10 as max
            resultDto.QuizId = resultDto.Quiz.Id;
            resultDto.Quiz = null;
            var result = mapper.Map<Result>(resultDto);

            await unitOfWork.ResultRepo.AddAsync(result);
            await unitOfWork.SaveChangesAsync();

            return resultDto;
        }

        public async Task<IEnumerable<ResultDto>> GetByQuizId(Guid quizId)
        {
            return mapper.Map<ICollection<ResultDto>>(await unitOfWork.ResultRepo.GetByQuizId(quizId));
        }
    }
}
