using BookApplication.Domain.DomainModels;
using BookApplication.Repository.Interface;
using BookApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Service.Implementation
{
    public class ApiDataFetchService : IApiDataFetchService
    {
        private readonly IRepository<Book> repository;
        private readonly IRepository<Chapter> chapterrepository;

        public ApiDataFetchService(IRepository<Book> repository, IRepository<Chapter> chapterrepository)
        {
            this.repository = repository;
            this.chapterrepository = chapterrepository;
        }

        public async Task<List<Book>> FetchBooksFromApi()
        {
            HttpClient client = new HttpClient();

            string URL = "http://is-lab4.ddns.net:8080/books";
            HttpResponseMessage message = await client.GetAsync(URL);
            var data = await message.Content.ReadFromJsonAsync<List<BookDTO>>();
            List<Book> books = new List<Book>();
            foreach (BookDTO b in data)
            {
                Book tmp = new Book
                {
                    Author = b.AuthorFirstName + b.AuthorLastName,
                    Description = b.ShortDescription,
                    ISBN = b.IsbnCode,
                    Title = b.Name,
                    PublishedYear = b.PublishedYear,
                    Id = Guid.NewGuid(),

                };
                books.Add(tmp);
                repository.Insert(tmp);
            }

            return books;
        }

        public async Task<List<Chapter>> FetchChaptersFromApi(Guid bookId, string index)
        {
            Book b = repository.Get(selector: x => x, predicate: y => y.Id == bookId);
            HttpClient client = new HttpClient();
            string URL = $"http://is-lab4.ddns.net:8080/chapters?bookId={bookId.ToString()}&studentIndex={index}";
            HttpResponseMessage message = await client.PostAsync(URL, null);
            var data = await message.Content.ReadFromJsonAsync<List<ChapterDTO>>();
            List<Chapter> chapters = new List<Chapter>();
            foreach (ChapterDTO c in data)
            {
                Chapter tmp = new Chapter
                {
                    Id = Guid.NewGuid(),
                    Book = b,
                    BookId = bookId,
                    ChapterNumber = 1,
                    DifficultyLevel = c.level,
                    HasExercises = c.includesexercises,
                    KeyConcept = c.keyConcept,
                    LastUpdated = c.lastupdated,
                    PageCount = c.totalpages,
                    Summary = c.overview,
                    Title = c.title
                };
                chapterrepository.Insert(tmp);
                chapters.Add(tmp);
            }
            return chapters;
        }
    }
}
