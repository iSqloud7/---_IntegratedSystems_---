using BookApplication.Domain.DomainModels;
using BookApplication.Repository.Interface;
using BookApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Service.Implementation
{
    public class ChapterService : IChapterService
    {
        private readonly IRepository<Chapter> _repository;

        public ChapterService(IRepository<Chapter> repository)
        {
            _repository = repository;
        }

        public Chapter DeleteById(Guid id)
        {
            var entity = GetById(id);

            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            return _repository.Delete(entity);
        }

        public List<Chapter> GetAll()
        {
            return _repository.GetAll(selector: x => x, include: x => x.Include(y => y.Book)).ToList();
        }

        public Chapter? GetById(Guid id)
        {
            return _repository.Get(selector: x => x,
                predicate: x => x.Id == id,
                include: x => x.Include(y => y.Book));
        }

        public Chapter Insert(Chapter entity)
        {
            return _repository.Insert(entity);
        }

        public Chapter Update(Chapter entity)
        {
            return _repository.Update(entity);
        }
    }
}
