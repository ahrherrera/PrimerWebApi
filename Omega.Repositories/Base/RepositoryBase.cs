using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Omega.Common.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Omega.Repositories.Base
{
    public abstract class RepositoryBase<T, U> : IDisposable, IRepository<T>
        where T : class
        where U : DbContext
    {
        public RepositoryBase(U context)
        {
            Context = context;
        }

        public U Context { get; set; }


        public abstract List<long> GetIds(Expression<Func<T, bool>> predicate);

        public long GetCount(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).Count();
        }

        public T GetByID(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public abstract long GetId(Expression<Func<T, bool>> predicate);

        //public abstract Expression<Func<T, bool>> GetPredicateFromFindExpression(FindExpression findExpression);

        public virtual List<T> GetAll([Optional] bool lazyLoadingEnabled)
        {
            List<T> result = null;
            IQueryable<T> query = null;
            List<T> queryData = null;

            try
            {
                query = Context.Set<T>();
                queryData = query.AsNoTracking().ToList();
                result = queryData;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public virtual List<T> FindWhere(Expression<Func<T, bool>> predicate, List<string> dependencies = null)
        {
            List<T> result = null;
            IQueryable<T> query = null;
            List<T> queryData = null;

            try
            {
                if (predicate != null)
                {
                    query = Context.Set<T>().AsNoTracking().Where(predicate);
                }
                else
                {
                    query = Context.Set<T>().AsNoTracking();
                }

                if (dependencies.IsNotNullAndHasAny())
                {
                    foreach (var item in dependencies)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            query = query.Include(item).AsQueryable();
                        }
                    }
                }

                queryData = query.ToList();
                result = queryData;
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public virtual IQueryable<T> FindWhereQuery(Expression<Func<T, bool>> predicate,
            List<string> dependencies = null)
        {
            IQueryable<T> query = null;

            try
            {
                if (predicate != null)
                {
                    query = Context.Set<T>().AsNoTracking().Where(predicate);
                }
                else
                {
                    query = Context.Set<T>().AsNoTracking();
                }

                if (dependencies.IsNotNullAndHasAny())
                {
                    foreach (var item in dependencies)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            query = query.Include(item).AsQueryable();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return query;
        }

        public virtual T FindSingle(Expression<Func<T, bool>> predicate, List<string> dependencies = null)
        {
            return FindSingle(predicate, true, dependencies);
        }

        public virtual T FindSingle(Expression<Func<T, bool>> predicate, bool AsNoTracking, List<string> dependencies = null)
        {
            T result = null;
            IQueryable<T> query = null;

            query = Context.Set<T>().AsQueryable();
            if (AsNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (dependencies.IsNotNullAndHasAny())
            {
                foreach (var item in dependencies)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        query = query.Include(item).AsQueryable();
                    }
                }
            }

            result = query.FirstOrDefault(predicate);

            return result;
        }

        public virtual T LastOrDefault(Expression<Func<T, bool>> predicate, [Optional] bool lazyLoadingEnabled)
        {
            T result = null;

            try
            {
                result = Context.Set<T>().AsNoTracking().LastOrDefault(predicate);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public virtual bool HasAny(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).Any();
        }

        public virtual T Add(T entity)
        {
            T result = null;

            if (entity != null)
            {
                //if (entity is IAuditDate)
                //{
                //    (entity as IAuditDate).UpdatedAt = DateTime.Now;
                //    (entity as IAuditDate).CreatedAt = DateTime.Now;
                //}
                //if (entity is IAuditUser)
                //{
                //    (entity as IAuditUser).CreatedBy = Convert.ToInt32(Context.UserId);
                //}
                result = Context.Set<T>().Add(entity).Entity;
            }

            return result;
        }

        public async virtual Task<T> AddAsync(T entity)
        {
            T result = null;
            EntityEntry<T> resultAsync = null;

            if (entity != null)
            {
                //if (entity is IAuditDate)
                //{
                //    (entity as IAuditDate).UpdatedAt = DateTime.Now;
                //    (entity as IAuditDate).CreatedAt = DateTime.Now;
                //}
                //if (entity is IAuditUser)
                //{
                //    (entity as IAuditUser).CreatedBy = Convert.ToInt32(Context.UserId);
                //}
                resultAsync = await Context.Set<T>().AddAsync(entity);

                if (resultAsync != null)
                {
                    result = resultAsync.Entity;
                }
            }

            return result;
        }

        public virtual List<T> AddRange(List<T> entities)
        {
            List<T> result = new List<T>();

            if (entities.IsNotNullAndHasAny())
            {
                foreach (var item in entities)
                {
                    result.Add(this.Add(item));
                }
            }

            return result;
        }

        public virtual async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            List<T> result = new List<T>();
            if (entities.IsNotNullAndHasAny())
            {
                foreach (var item in entities)
                {
                    result.Add(await AddAsync(item));
                }
            }

            return result;
        }

        public virtual T Delete(T entity)
        {
            EntityEntry<T> result = null;

            if (entity != null)
            {
                result = Context.Set<T>().Remove(entity);
            }

            return result.Entity;
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = null;
            List<T> queryData = null;

            query = Context.Set<T>().Where(predicate);
            queryData = query.ToList();
            Context.Set<T>().RemoveRange(queryData);
        }

        public virtual T Update(T entity)
        {
            EntityEntry<T> result = null;

            if (entity != null)
            {
                //if (entity is IAuditDate)
                //{
                //    (entity as IAuditDate).UpdatedAt = DateTime.Now;
                //}
                //if (entity is IAuditUser)
                //{
                //    (entity as IAuditUser).UpdatedBy = Convert.ToInt32(Context.UserId);
                //}
                Context.Entry(entity).State = EntityState.Detached;
                result = Context.Set<T>().Update(entity);
            }

            return result.Entity;
        }

        public virtual List<T> UpdateMany(IEnumerable<T> entities)
        {
            List<T> result = new List<T>();

            if (entities.IsNotNullAndHasAny())
            {
                foreach (var item in entities)
                {
                    result.Add(Update(item));
                }
            }

            return result;
        }

        public virtual T PartialUpdate(int id, List<KeyValuePair<string, string>> propertiesToUpdate)
        {
            T result = null;
            T originalData = Context.Set<T>().Find(id);

            var typeFromEntity = originalData.GetType();
            var entityProperties = typeFromEntity.GetProperties();

            foreach (var modifiedProperty in propertiesToUpdate)
            {
                var propertyToModified = entityProperties.FirstOrDefault(a => a.Name.Equals(modifiedProperty.Key.FirstLetterToUpperCase()));

                if (propertyToModified != null)
                {
                    Type propertyType = propertyToModified.PropertyType;
                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        propertyType = Nullable.GetUnderlyingType(propertyType);
                    }

                    if (modifiedProperty.Value != null)
                    {
                        propertyToModified.SetValue(originalData, Convert.ChangeType(modifiedProperty.Value, propertyType));
                    }
                    else
                    {
                        propertyToModified.SetValue(originalData, null);
                    }
                }
            }

            result = Update(originalData);
            return result;
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
            List<EntityEntry> entries;

            entries = Context.ChangeTracker.Entries().ToList();
            for (int i = 0; i < entries.Count(); i++)
            {
                EntityEntry dbEntityEntry = entries[i];
                dbEntityEntry.State = EntityState.Detached;
            }
        }

        public virtual async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
            List<EntityEntry> entries;

            entries = Context.ChangeTracker.Entries().ToList();
            if (entries != null)
            {
                for (int i = 0; i < entries.Count(); i++)
                {
                    EntityEntry dbEntityEntry = entries[i];
                    dbEntityEntry.State = EntityState.Detached;
                }
            }
        }

        public virtual void Dispose()
        {
            //Context.Dispose();
            //Context = null;
        }

        public T UpdateProperties(T model, Dictionary<string, string> keyValuePairs)
        {
            PropertyInfo propertyInfo = null;
            foreach (KeyValuePair<string, string> item in keyValuePairs)
            {
                propertyInfo = model.GetType().GetProperty(item.Key);
                if (propertyInfo == null)
                    throw new Exception($"No se encontró la propiedad {item.Key} en la clase {model.GetType().FullName}.");
                if (propertyInfo.PropertyType == typeof(DateTimeOffset))
                {
                    propertyInfo.SetValue(model, DateTimeOffset.Parse(item.Value), null);
                }
                else
                {
                    propertyInfo.SetValue(model, Convert.ChangeType(item.Value, propertyInfo.PropertyType), null);
                }
            }
            model = Update(model);
            SaveChanges();
            return model;
        }
    }
}
