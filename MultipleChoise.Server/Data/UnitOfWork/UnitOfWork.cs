using Microsoft.EntityFrameworkCore;
using MultipleChoise.Server.Data.Models;
using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Data.Repositorys;

namespace MultipleChoise.Server.Data.UnitOfWork
{
    public class UnitOfWork(MultipleChoiseDbContext context,
                      IAnswerRepository answerRepository,
                      IQuestionRepository questionRepository,
                      IQuizRepository quizRepository,
                      IResultRepository resultRepository) : IUnitOfWork
    {
        public readonly DbContext _context = context;

        public IAnswerRepository AnswerRepo { get; } = answerRepository;
        public IQuestionRepository QuestionRepo { get; } = questionRepository;
        public IQuizRepository QuizRepo { get; } = quizRepository;
        public IResultRepository ResultRepo { get; } = resultRepository;

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();
    }
}
