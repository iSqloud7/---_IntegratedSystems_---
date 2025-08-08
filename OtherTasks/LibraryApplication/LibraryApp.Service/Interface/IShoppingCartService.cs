using LibraryApp.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart? GetByOwner(string ownerID);

        ShoppingCart? GetByOwnerIncludeBooks(string ownerID);

        void OrderShoppingCart(string ownerID);
    }
}
