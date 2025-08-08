using LibraryApp.Repository.Interface;
using LibraryApp.Web.Data;
using LibraryApp.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> entities;

        public Repository(ApplicationDbContext context, DbSet<T> entities)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }

        public T Create(T entity)
        {
            this.entities.Add(entity);
            this.context.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            this.entities.Update(entity);
            this.context.SaveChanges();

            return entity;
        }

        public T Delete(T entity)
        {
            this.entities.Remove(entity);
            this.context.SaveChanges();

            return entity;
        }

        public E? Get<E>(Expression<Func<T, E>> selector, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var query = this.entities.AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.Select(selector).FirstOrDefault();
        }

        public IEnumerable<E> GetAll<E>(Expression<Func<T, E>> selector, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var query = this.entities.AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.Select(selector).AsEnumerable();
        }
    }
}
