using Microsoft.EntityFrameworkCore;
using MultipleChoise.Server.Data.Models;
using MultipleChoise.Server.Data.Models.Entity;

namespace MultipleChoise.Server.Data.Repositorys
{
    public class ResultRepository : BaseRepository<Result>, IResultRepository
    {
        public ResultRepository(MultipleChoiseDbContext context) : base(context)
        {

        }

        public async Task<ICollection<Result>> GetByQuizId(Guid id)
        {
            return await _dbSet
                .Where(q => q.QuizId == id)
                .ToListAsync();
        }
    }
}
