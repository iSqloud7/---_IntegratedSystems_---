using BookApplication.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication.Service.Interface
{
    public interface IChapterService
    {
        List<Chapter> GetAll();
        Chapter? GetById(Guid id);
        Chapter Insert(Chapter chapter);
        Chapter Update(Chapter chapter);
        Chapter DeleteById(Guid id);
    }
}
