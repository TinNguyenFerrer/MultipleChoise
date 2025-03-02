using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Data.Repositorys;

namespace MultipleChoise.Server.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAnswerRepository AnswerRepo { get; }
        IQuestionRepository QuestionRepo { get; }
        IQuizRepository QuizRepo { get; }
        IResultRepository ResultRepo { get; }
        Task SaveChangesAsync();
    }
}
