using ConsoleApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model
{
    // abstract class with basic functionalities
    public abstract class LibraryUser : IUserBorrowing
    {
        public long ID { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public List<Book>? BorrowedBooks { get; set; } = new(); // initialization

        public abstract int BorrowLimit();

        public void BorrowBook(Book book)
        {
            if (BorrowedBooks?.Count >= BorrowLimit())
            {
                Console.WriteLine($"{Name} - You have reached the borrow limit!");
                return;
            }

            if (!book.IsAvailable())
            {
                Console.WriteLine($"{Name} - Book is not available!");
                return;
            }

            BorrowedBooks?.Add(book);
            book.BorrowedSamples++;
            Console.WriteLine($"{Name + Surname} borrowed {book.Title}");
            Console.WriteLine($"{Name} has {BorrowedBooks?.Count} remaining samples!");
        }

        public void ReturnBook(Book book)
        {
            var borrowedBook = BorrowedBooks?.FirstOrDefault(b => b.IsbnCode == book.IsbnCode);

            if (borrowedBook == null)
            {
                Console.WriteLine("Book is not borrowed!");
                return;
            }

            BorrowedBooks?.Remove(book);
            book.BorrowedSamples--;
            Console.WriteLine($"{Name + Surname} returned {book.Title}");
            Console.WriteLine($"{Name} has {BorrowedBooks?.Count} remaining samples!");
        }
    }
}