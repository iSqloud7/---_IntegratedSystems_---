namespace LibraryApplication.Domain.DomainModels
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int FloorNumber { get; set; }
        public virtual ICollection<BookCategory>? BookCategories { get; set; }
    }
}
