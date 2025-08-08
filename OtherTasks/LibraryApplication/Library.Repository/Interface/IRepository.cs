using LibraryApp.Web.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);

        // books.Where(book => book.Equals(ID));
        // books.Where(book => book.Equals(ID)).Select(book => book.ID);
        E? Get<E>(
            Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        IEnumerable<E> GetAll<E>(
            Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    }
}
