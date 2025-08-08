using LibraryApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Relationship
{
    public class BooksInShoppingCart : BaseEntity
    {
        public Guid BookID { get; set; }

        public virtual Book? Book { get; set; }

        public Guid ShoppingCartID { get; set; }

        public virtual ShoppingCart? ShoppingCart { get; set; }

        public int Quantity { get; set; }
    }
}
