using MultipleChoise.Server.Data.Models;
using MultipleChoise.Server.Data.Models.Entity;

namespace MultipleChoise.Server.Data.Repositorys
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(MultipleChoiseDbContext context) : base(context)
        {
        }
    }
}
