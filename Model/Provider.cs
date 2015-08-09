using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Provider
    {
        #region Singleton Declarations

        private static Provider _instance = null;
        private static MyDentDBEntities _db;

        private Provider() { }

        public static Provider Instance
        {
            get
            {
                _db = _db ?? new MyDentDBEntities();

                return _instance ?? new Provider();
            }
        }

        #endregion

        public bool Add<T>(T t) where T : class
        {
            try
            {
                _db.Set<T>().Add(t);
                _db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete<T>(T t) where T : class
        {
            try
            {
                _db.Set<T>().Remove(t);
                _db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update<T>(T entity) where T : class
        {
            try
            {
                _db.Entry(entity).State = System.Data.EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public T FindById<T>(object id) where T : class
        {
            return _db.Set<T>().Find(id);
        }

        public IQueryable<T> FindBy<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                IQueryable<T> query = _db.Set<T>().Where(predicate);
                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            try
            {
                IQueryable<T> query = _db.Set<T>();
                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
