namespace LibraryApp.Domain.DomainModels
{
    public class BaseEntity
    {
        public Guid ID { get; set; } // public Guid ID { get; set; } = Guid.NewGuid();

        public DateTime createdOn { get; set; } = DateTime.Now;
    }
}
