using System.ComponentModel.DataAnnotations;

namespace MultipleChoise.Server.Contract.Dto
{
    public class BaseEntityDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
