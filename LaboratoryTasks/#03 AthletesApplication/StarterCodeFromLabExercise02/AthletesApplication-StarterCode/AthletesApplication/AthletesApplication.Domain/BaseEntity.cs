using System.ComponentModel.DataAnnotations;

namespace AthletesApplication.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
