using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model
{
    public class ResearcherUser : LibraryUser
    {
        public List<string>? ResearchPapers { get; set; } = new List<string>();
        public override int BorrowLimit() => int.MaxValue;
    }
}