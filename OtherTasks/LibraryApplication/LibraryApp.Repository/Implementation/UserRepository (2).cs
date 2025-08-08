using LibraryApp.Domain.DomainModels;
using LibraryApp.Domain.Identity;
using LibraryApp.Repository.Data;
using LibraryApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<LibraryApplicationUser> _entities;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<LibraryApplicationUser>();
        }

        public LibraryApplicationUser GetUserByID(string ID)
        {
            return _entities.First(user => user.Id.Equals(ID));
        }
    }
}
