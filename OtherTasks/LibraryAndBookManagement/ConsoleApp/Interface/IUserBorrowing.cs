using ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interface
{
    public interface IUserBorrowing
    {
        void BorrowBook(Book book);

        void ReturnBook(Book book);
    }
}