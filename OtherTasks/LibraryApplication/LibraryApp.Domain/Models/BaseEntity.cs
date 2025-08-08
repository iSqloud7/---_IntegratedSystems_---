namespace LibraryApp.Web.Models
{
    public class BaseEntity
    {
        public Guid ID { get; set; } // public Guid ID { get; set; } = Guid.NewGuid();

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
