using ConsoleApp.Interface;
using ConsoleApp.Model;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var book1 = new Book()
            {
                IsbnCode = 123456789,
                Title = "C# Programming",
                Author = "John Doe",
                TotalNumOfSamples = 10
            };

            var book2 = new Book()
            {
                IsbnCode = 987654321,
                Title = "Data Structures",
                Author = "Jane Smith",
                TotalNumOfSamples = 1
            };

            var user1 = new RegularUser()
            {
                ID = 11111,
                Name = "Mark",
                Surname = "Johnson",
            };

            var user2 = new StudentUser()
            {
                ID = 22222,
                Name = "Lisa",
                Surname = "Brown",
                Faculty = "Faculty Of Computer Science And Engeneering"
            };

            var user3 = new ResearcherUser()
            {
                ID = 33333,
                Name = "Emily",
                Surname = "Clark"
            };

            user1.BorrowBook(book1);
            user1.BorrowBook(book2);

            user2.BorrowBook(book1);
            user2.BorrowBook(book2);

            user3.BorrowBook(book1);



            user1.ReturnBook(book1);

            user3.ReturnBook(book1);



            var listOfBooks = new List<Book>();
            listOfBooks.Add(book1);
            listOfBooks.Add(book1);

            ILibrary iLibrary = new Library();

            var mostBorrowedBook = iLibrary.GetMostBorrowedBook(listOfBooks);

            if (mostBorrowedBook == null)
            {
                Console.WriteLine("No books borrowed!");
                return;
            }

            Console.WriteLine("=========");
            Console.WriteLine($"Most borrowed book is: {mostBorrowedBook?.Title}");
            Console.WriteLine("=========");
        }
    }
}