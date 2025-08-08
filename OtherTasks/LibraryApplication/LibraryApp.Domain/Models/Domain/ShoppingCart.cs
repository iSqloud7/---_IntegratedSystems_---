using LibraryApp.Web.Models.Identity;
using LibraryApp.Web.Models.Relationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Web.Models.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerID { get; set; }

        public virtual LibraryApplicationUser? LibraryUser { get; set; }

        public virtual ICollection<BooksInShoppingCart>? BooksInShoppingCarts { get; set; }
    }
}
