using LibraryApp.Domain.DomainModels;
using LibraryApp.Domain.Email;
using LibraryApp.Domain.Relationship;
using LibraryApp.Repository.Interface;
using LibraryApp.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BooksInOrder> _booksInOrderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository, IRepository<BooksInOrder> booksInOrderRepository, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _orderRepository = orderRepository;
            _booksInOrderRepository = booksInOrderRepository;
            _userRepository = userRepository;
        }

        public ShoppingCart? GetByOwner(string ownerID)
        {
            return _shoppingCartRepository.Get(selector: cart => cart,
                filter: cart => cart.OwnerID != null && cart.OwnerID.Equals(ownerID));
        }

        public ShoppingCart? GetByOwnerIncludeBooks(string ownerID)
        {
            return _shoppingCartRepository.Get(selector: cart => cart,
                filter: cart => cart.OwnerID != null && cart.OwnerID.Equals(ownerID),
                include: cart => cart.Include(i => i.BooksInShoppingCart)
                .ThenInclude(b => b.Book));
        }

        public void OrderShoppingCart(string ownerID)
        {
            var shoppingCart = GetByOwnerIncludeBooks(ownerID);

            if (shoppingCart == null)
            {
                throw new Exception("Shopping cart not found!");
            }

            var order = new Order
            {
                ID = Guid.NewGuid(),
                OwnerID = ownerID,
            };

            _orderRepository.Insert(order);

            var booksInOrder = shoppingCart.BooksInShoppingCart
                ?.Select(bs => new BooksInOrder()
                {
                    ID = Guid.NewGuid(),
                    BookID = bs.BookID,
                    Book = bs.Book,
                    OrderID = order.ID,
                    Order = order,
                    Quantity = bs.Quantity,
                }).ToList();

            if (booksInOrder == null)
            {
                throw new Exception("Books in order not found!");
            }

            foreach (var bookInOrder in booksInOrder)
            {
                _booksInOrderRepository.Insert(bookInOrder);
            }

            shoppingCart.BooksInShoppingCart?.Clear();
            _shoppingCartRepository.Update(shoppingCart);

            var user = _userRepository.GetUserByID(ownerID);

            if(user != null && user.Email != null)
            {
                var emailMessage = new EmailMessage
                {
                    Subject = "Order Confirmation",
                    Body = $"Your order has been placed successfully. Order ID: {order.ID}",
                    SendTo = ownerID,
                };
            }
        }
    }
}
