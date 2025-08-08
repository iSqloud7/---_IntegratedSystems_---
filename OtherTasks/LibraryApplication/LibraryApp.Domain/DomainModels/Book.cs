using LibraryApp.Domain.Relationship;

namespace LibraryApp.Domain.DomainModels
{
    public class Book  : BaseEntity
    {
        public String Title { get; set; } = String.Empty;

        public String Author { get; set; } = String.Empty;

        public String? Image { get; set; }

        public double Price { get; set; }

        public virtual ICollection<BooksInShoppingCart>? BooksInShoppingCart { get; set; }
    }
}
