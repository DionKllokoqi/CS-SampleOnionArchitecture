using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainEntityLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.RepositoryPattern
{
    public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        #region Private Fields
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> _entities;
        #endregion

        #region CTOR & Init
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _entities = _applicationDbContext.Set<T>();
        } 
        #endregion

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Remove(entity);
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public T Get(int id)
        {
            return _entities.SingleOrDefault(x => x.Id == id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Add(entity);
            _applicationDbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Remove(entity);
        }

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Update(entity);
            _applicationDbContext.SaveChanges();
        }
    }
}
