using System.Linq.Expressions;
using CoursesApplication.Domain.DomainModels;
using Microsoft.EntityFrameworkCore.Query;

namespace CoursesApplication.Repository.Interface;


public interface IRepository<T> where T : BaseEntity
{
    T Insert(T entity);
    ICollection<T> InsertMany(ICollection<T> entity);
    T Update(T entity);
    T Delete(T entity);
    E? Get<E>(Expression<Func<T, E>> selector,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    IEnumerable<E> GetAll<E>(Expression<Func<T, E>> selector,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
}