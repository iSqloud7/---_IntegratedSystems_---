using LibraryApp.Domain.Relationship;
using LibraryApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerID { get; set; }

        public virtual LibraryApplicationUser? LibraryApplicationUser { get; set; }

        public virtual ICollection<BooksInShoppingCart>? BooksInShoppingCart { get; set; }
    }
}
