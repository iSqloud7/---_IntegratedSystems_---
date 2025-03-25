using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model
{
    public class StudentUser : LibraryUser
    {
        public string? Faculty { get; set; }
        public override int BorrowLimit() => 7;
    }
}