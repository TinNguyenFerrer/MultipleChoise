using System.ComponentModel.DataAnnotations;

namespace MultipleChoise.Server.Data.Models.Entity
{
    public class BaseEntity
    {
        private Guid _id;

        [Key]
        public Guid Id // Automatically generate GUID
        {
            get => _id == Guid.Empty ? Guid.NewGuid() : _id;
            set => _id = value;
        }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow; // Set default to now
        public DateTime? ModifiedOn { get; set; } // Nullable for tracking updates
    }
}
