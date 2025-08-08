using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Interface;

namespace ConsoleApp.Model
{
    public class Library : ILibrary
    {
        public Book? GetMostBorrowedBook(List<Book> books)
        {
            return books.MaxBy(book => book.TotalNumOfSamples);

            // return books.OrderByDescending(book => book.TotalNumOfSamples).FirstOrDefault();
        }

        public List<LibraryUser> GetUsersWithOverdueBooks(List<LibraryUser> users)
        {
            return new List<LibraryUser>();
        }
    }
}