using LibraryApp.Web.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Web.Models.Identity
{
    public class LibraryApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public virtual ShoppingCart? UserShoppingCart { get; set; }
    }
}
