using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Interface;

namespace ConsoleApp.Model
{
    public class RegularUser : LibraryUser
    {
        public override int BorrowLimit() => 5;
    }
}