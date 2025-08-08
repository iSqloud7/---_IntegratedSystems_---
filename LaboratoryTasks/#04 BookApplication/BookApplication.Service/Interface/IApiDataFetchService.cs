using BookApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Service.Interface
{
    public interface IApiDataFetchService
    {
        Task<List<Chapter>> FetchChaptersFromApi(Guid bookId, string index);
        Task<List<Book>> FetchBooksFromApi();
    }
}
