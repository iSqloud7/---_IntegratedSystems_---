using LibraryApp.Domain.DTOs;
using LibraryApp.Domain.Relationship;
using LibraryApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryApp.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userID == null)
            {
                throw new Exception("User not found!");
            }

            var cart = _shoppingCartService.GetByOwnerIncludeBooks(userID);

            if (cart == null)
            {
                throw new Exception("Shopping cart not found!");
            }

            var booksInShoppingCart = cart.BooksInShoppingCart?.ToList() ?? new List<BooksInShoppingCart>();

            var viewModel = new ShoppingCartDTO
            {
                BooksInShoppingCart = booksInShoppingCart,
                TotalPrice = booksInShoppingCart.Sum(b => (b.Book?.Price ?? 0) * b.Quantity)
            };

            return View(viewModel);
        }

        public IActionResult Order()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userID == null)
            {
                throw new Exception("User not found!");
            }

            _shoppingCartService.OrderShoppingCart(userID);

            return RedirectToAction("Index", "Home");
        }
    }
}
