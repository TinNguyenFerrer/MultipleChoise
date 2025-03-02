using MultipleChoise.Server.Data.Models.Entity;

namespace MultipleChoise.Server.Data.Repositorys
{
    public interface IQuizRepository: IBaseRepository<Quiz>
    {
        Task<Quiz> CreateQuizAsync(Quiz quiz);
        Task<Quiz?> GetQuizWithQuestionsAndAnswersAsync(Guid id);
        Task<IEnumerable<Quiz>> GetQuizzesWithQuestionsAsync();
        Task<Quiz?> GetQuizByNumberAsync(int number);
    }
}
