using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Model;

namespace ConsoleApp.Interface
{
    public interface ILibrary
    {
        Book? GetMostBorrowedBook(List<Book> books);

        List<LibraryUser> GetUsersWithOverdueBooks(List<LibraryUser> users);
    }
}