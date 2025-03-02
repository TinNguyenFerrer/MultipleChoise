using System.ComponentModel.DataAnnotations.Schema;

namespace MultipleChoise.Server.Data.Models.Entity
{
    public class Quiz : BaseEntity
    {
        public string Title { get; set; } // Title of the quiz
        public TimeSpan Duration { get; set; } // Quiz duration
        public ICollection<Question> Questions { get; set; } = new List<Question>();

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UniqueNumber { get; set; }
    }
}
