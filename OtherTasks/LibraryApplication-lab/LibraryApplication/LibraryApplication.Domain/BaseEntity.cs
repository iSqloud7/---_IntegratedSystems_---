using System.ComponentModel.DataAnnotations;

namespace LibraryApplication.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
