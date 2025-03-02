using Microsoft.EntityFrameworkCore;
using MultipleChoise.Server.Data.Models;
using MultipleChoise.Server.Data.Models.Entity;

namespace MultipleChoise.Server.Data.Repositorys
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(MultipleChoiseDbContext context) : base(context)
        {
        }
    }
}
