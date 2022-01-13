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
            SaveChanges();
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
            SaveChanges();
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
            var entries = _applicationDbContext.ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Detached:
                        case EntityState.Unchanged:
                        case EntityState.Deleted:
                            break;
                        case EntityState.Modified:

                            trackable.ModifiedDate = DateTime.UtcNow;
                            entry.Property(nameof(trackable.CreatedDate)).IsModified = false;
                            break;

                        case EntityState.Added:

                            trackable.CreatedDate = DateTime.UtcNow;
                            trackable.ModifiedDate= DateTime.UtcNow;
                            break;
                    }
                }
            }

            _applicationDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Update(entity);
            SaveChanges();
        }
    }
}
