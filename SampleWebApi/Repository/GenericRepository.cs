using Microsoft.EntityFrameworkCore;
using SampleWebApi.EfCoreDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PenaltyCalculationApp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private SampleDbContext context;
        private DbSet<T> dbSet;

        public GenericRepository(SampleDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            try
            {
                context.Set<T>().Add(entity);
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                return await Task.FromResult(false);
            }
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var result = context.Set<T>().Where(i => true);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.ToListAsync();
        }


        public virtual async Task<List<T>> SearchByAsync(Expression<Func<T, bool>> searchBy, params Expression<Func<T, object>>[] includes)
        {
            var result = context.Set<T>().Where(searchBy);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.ToListAsync();
        }

        /// <summary>
        /// Finds by predicate.
        /// http://appetere.com/post/passing-include-statements-into-a-repository
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        public virtual async Task<T> FindByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = context.Set<T>().Where(predicate);

            foreach (var includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.FirstOrDefaultAsync();
        }

        public virtual async Task<bool> Any(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = await context.Set<T>().AnyAsync(predicate);
            return result;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                context.Set<T>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;

                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                return await Task.FromResult(false);
            }
        }

        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> identity, params Expression<Func<T, object>>[] includes)
        {
            var results = context.Set<T>().Where(identity);

            foreach (var includeExpression in includes)
                results = results.Include(includeExpression);
            try
            {
                context.Set<T>().RemoveRange(results);
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                return await Task.FromResult(false);
            }
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            return await Task.FromResult(true);
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await context.Set<T>().FindAsync(id);
        }

    }
}
