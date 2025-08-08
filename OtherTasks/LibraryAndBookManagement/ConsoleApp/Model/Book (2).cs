using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Interface;

namespace ConsoleApp.Model
{
    public class Book : IBookManagement
    {
        public long IsbnCode { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public int TotalNumOfSamples { get; set; }

        public int BorrowedSamples { get; set; }

        public bool IsAvailable()
        {
            return TotalNumOfSamples > BorrowedSamples;
        }
    }
}