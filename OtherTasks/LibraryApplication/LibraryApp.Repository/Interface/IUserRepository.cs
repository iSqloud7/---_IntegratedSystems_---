using LibraryApp.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Repository.Interface
{
    public interface IUserRepository
    {
        LibraryApplicationUser GetUserByID(string ID);
    }
}
