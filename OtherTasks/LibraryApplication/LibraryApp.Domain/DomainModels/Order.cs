using LibraryApp.Domain.Identity;
using LibraryApp.Domain.Relationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string? OwnerID { get; set; }

        public virtual LibraryApplicationUser? LibraryApplicationUser { get; set; }

        public virtual ICollection<BooksInOrder>? BooksInOrder { get; set; }
    }
}
