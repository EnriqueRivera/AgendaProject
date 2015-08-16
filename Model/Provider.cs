using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                AddLog("ERROR", DateTime.Now, e, this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public bool AddIfDoesntExist<T>(object id, T entity) where T : class
        {
            try
            {
                if (FindById<T>(id) == null)
                {
                    Add<T>(entity);
                    _db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                AddLog("ERROR", DateTime.Now, e, this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public bool AddIfDoesntExist<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, T entity) where T : class
        {
            try
            {
                T query = _db.Set<T>().Where(predicate).FirstOrDefault();

                if (query == null)
                {
                    Add<T>(entity);
                    _db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                AddLog("ERROR", DateTime.Now, e, this.GetType().Name, MethodBase.GetCurrentMethod().Name);
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
                AddLog("ERROR", DateTime.Now, e, this.GetType().Name, MethodBase.GetCurrentMethod().Name);
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
                AddLog("ERROR", DateTime.Now, e, this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }

        public T FindById<T>(object id) where T : class
        {
            try
            {
                return _db.Set<T>().Find(id);
            }
            catch (Exception e)
            {
                AddLog("ERROR", DateTime.Now, e, this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return null;
            }
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
                AddLog("ERROR", DateTime.Now, e, this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return Enumerable.Empty<T>().AsQueryable();
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
                AddLog("ERROR", DateTime.Now, e, this.GetType().Name, MethodBase.GetCurrentMethod().Name);
                return Enumerable.Empty<T>().AsQueryable();
            }
        }

        public void CloseConnection()
        {
            using (_db) { }
        }

        public void AddLog(string type, DateTime logDate, Exception e, string className, string methodName)
        {
            Log logToAdd = new Log()
            {
                Type = type,
                LogDate = logDate,
                ErrorDetail = e.Message + ". Inner exception" + (e.InnerException == null ? string.Empty : e.InnerException.Message),
                MethodName = "Class: " + className + ". Method: " + methodName
            };

            _db.Logs.Add(logToAdd);
            _db.SaveChanges();
        }
    }
}
