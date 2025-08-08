using LibraryApp.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Domain.Identity
{
    public class LibraryApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public virtual ShoppingCart? UserShoppingCart { get; set; }
    }
}

// Import the new user in the application.

// 1.(Program.cs)
// builder.Services.AddDefaultIdentity<IdentityUser> => builder.Services.AddDefaultIdentity<LibraryApplicationUser>

// 2.Migrations/(ApplicationDbContext.cs)
// public class ApplicationDbContext : IdentityDbContext<IdentityUser> => public class ApplicationDbContext : IdentityDbContext<LibraryApplicationUser>

// 3.Views/Shared/(_LoginPartial.cshtml)
// @inject SignInManager<IdentityUser> SignInManager => @inject SignInManager<LibraryApplicationUser> SignInManager
// @inject UserManager<IdentityUser> UserManager => @inject UserManager<LibraryApplicationUser> UserManager

// Microsoft.AspNetCore.Identity.EntityFrameworkCore -> Add in Manage NuGet Packages
