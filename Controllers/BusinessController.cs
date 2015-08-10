using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class BusinessController
    {
        #region Singleton Declarations

        private static BusinessController _instance = null;

        private BusinessController() { }

        public static BusinessController Instance
        {
            get
            {
                return _instance ?? new BusinessController();
            }
        }

        #endregion

        public bool Add<T>(T t) where T : class
        {
            return Model.Provider.Instance.Add<T>(t);
        }

        public bool AddIfDoesntExist<T>(object id, T t) where T : class
        {
            return Model.Provider.Instance.AddIfDoesntExist<T>(id, t);
        }

        public bool AddIfDoesntExist<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate, T t) where T : class
        {
            return Model.Provider.Instance.AddIfDoesntExist<T>(predicate, t);
        }

        public bool Delete<T>(T t) where T : class
        {
            return Model.Provider.Instance.Delete<T>(t);
        }

        public bool Update<T>(T t) where T : class
        {
            return Model.Provider.Instance.Update<T>(t);
        }

        public T FindById<T>(object id) where T : class
        {
            return Model.Provider.Instance.FindById<T>(id);
        }

        public IQueryable<T> FindBy<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class
        {
            return Model.Provider.Instance.FindBy<T>(predicate);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return Model.Provider.Instance.GetAll<T>();
        }

        public void CloseConnection()
        {
            Model.Provider.Instance.CloseConnection();
        }

        public void AddLog(string type, DateTime logDate, Exception e, string className, string methodName)
        {
            Model.Provider.Instance.AddLog(type, logDate, e, className, methodName);
        }
    }
}
