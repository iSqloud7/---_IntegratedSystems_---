using LibraryApp.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Web.Models.Relationship
{
    public class BooksInShoppingCart
    {
        public Guid BookID { get; set; }

        public virtual Book? Book { get; set; }

        public Guid ShoppingCartID { get; set; }

        public virtual ShoppingCart? ShoppingCart { get; set; }

        public int Quantity { get; set; }
    }
}
