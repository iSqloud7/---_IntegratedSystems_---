using LibraryApp.Web.Models.Relationship;

namespace LibraryApp.Web.Models.Domain
{
    public class Book : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string? Image { get; set; }

        public virtual ICollection<BooksInShoppingCart>? BooksInShoppingCarts { get; set; }
    }
}
