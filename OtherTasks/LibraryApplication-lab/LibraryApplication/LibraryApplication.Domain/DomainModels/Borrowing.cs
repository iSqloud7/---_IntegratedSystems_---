namespace LibraryApplication.Domain.DomainModels
{
    public class Borrowing : BaseEntity
    {
        public string Name { get; set; }
        public DateOnly DateBorrowed { get; set; }
        public bool IsDamaged { get; set; }
        public DateOnly DateReturned { get; set; }
        public Guid BookId { get; set; }
        public Book? Book { get; set; }
    }
}
