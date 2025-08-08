using Microsoft.AspNetCore.Identity;

namespace LibraryApplication.Domain.IdentityModels
{
    public class LibraryApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
