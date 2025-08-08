using LibraryApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Relationship
{
    public class BooksInOrder : BaseEntity
    {
        public Guid BookID { get; set; }

        public virtual Book? Book { get; set; }

        public Guid OrderID { get; set; }

        public virtual Order? Order { get; set; }

        public int Quantity { get; set; }
    }
}
