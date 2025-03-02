using Microsoft.EntityFrameworkCore;
using MultipleChoise.Server.Contract.Dto;
using MultipleChoise.Server.Data.Models;
using MultipleChoise.Server.Data.Models.Entity;

namespace MultipleChoise.Server.Data.Repositorys
{
    public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(MultipleChoiseDbContext context) : base(context) { }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz)
        {
            // Add the quiz to the DbSet
            await AddAsync(quiz);
            await SaveChangesAsync(); // Save changes to the database
            return await GetQuizWithQuestionsAndAnswersAsync(quiz.Id);
        }

        public async Task<Quiz?> GetQuizWithQuestionsAndAnswersAsync(Guid id)
        {
            return await _dbSet
                .Include(q => q.Questions.OrderBy(q => q.CreatedOn))
                    .ThenInclude(q => q.Answers.OrderBy(q => q.CreatedOn))
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesWithQuestionsAsync()
        {
            return await _dbSet.Include(q => q.Questions).ToListAsync();
        }

        public async Task<Quiz?> GetQuizByNumberAsync(int number)
        {
            return await _dbSet
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.UniqueNumber == number);
        }

        public async Task<Quiz> UpdateAndGetAsync(Quiz quiz)
        {
            this.Update(quiz);



            return await this.GetByIdAsync(quiz.Id);
        }

    }
}
