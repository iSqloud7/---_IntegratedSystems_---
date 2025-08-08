using Microsoft.AspNetCore.Identity;

namespace AthletesApplication.Domain.IdentityModels
{
    public class AthletesApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
