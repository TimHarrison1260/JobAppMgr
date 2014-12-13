using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T: class
    {
        internal readonly JobApplicationContext _db;

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork", "No valid UnitOfWork supplied to Repository");
            _db = unitOfWork as JobApplicationContext;
        }

        public virtual IQueryable<T> GetAll()
        {
            var result = _db.Set<T>();
            return result;
        }

        public abstract IQueryable<T> Search(string queryString);

        public virtual T Get(int id)
        {
            var result = _db.Set<T>().Find(id);
            return result;
        }

        public virtual T Create(T obj)
        {
            //  Add the object to 
            var result = _db.Set<T>().Add(obj);
            _db.SaveChanges();
            return result;
        }

        public virtual bool Update(T obj)
        {
            _db.Entry(obj).State = EntityState.Modified;

            _db.SaveChanges();

            return true;
        }

        public virtual bool Delete(int id)
        {
            //  retreive the JobApplication from the underlying datastore
            var result = _db.Set<T>().Find(id);
            if (result == null) return false;
            _db.Entry(result).State = EntityState.Deleted;
            //  Save the changes
            _db.SaveChanges();

            return true;
        }
    }
}
