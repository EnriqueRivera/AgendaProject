using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class Utils
    {
        public const string SCHEDULER_COLOR_CONFIGURATION_PREFIX = "SCHEDULER_COLOR_";
        public const string PATIENT_MAX_SKIPPED_EVENTS_CONFIGURATION = "PATIENT_MAX_SKIPPED_EVENTS";

        public static bool IsOverlappedTime(DateTime event1Start, DateTime event1End, DateTime event2Start, DateTime event2End)
        {
            return ((event1Start >= event2Start && event1Start < event2End) || (event1End > event2Start && event1End <= event2End))
                    || ((event2Start >= event1Start && event2Start < event1End) || (event2End > event1Start && event2End <= event1End));
        }

        public static bool AddEventStatusChanges(string oldEventStatus, string newEventStatus, int eventId, int userId)
        {
            Model.EventStatusChanx eventStatusChanged = new Model.EventStatusChanx()
            {
                OldStatus = oldEventStatus,
                NewStatus = newEventStatus,
                ChangeDate = DateTime.Now,
                EventId = eventId,
                StatusChangerId = userId
            };

            return BusinessController.Instance.Add<Model.EventStatusChanx>(eventStatusChanged);
        }

        public static string EventStatusString(EventStatus es)
        {
            switch (es)
            {
                case EventStatus.CANCELED: return "Cancelada";
                case EventStatus.COMPLETED: return "Completada";
                case EventStatus.EXCEPTION: return "Excepción";
                case EventStatus.PATIENT_SKIPS: return "Paciente no asisitó";
                case EventStatus.PENDING: return "Sin concretar";
                default: return string.Empty;
            }
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class CustomViewModel<T> where T : class
    {
        private ObservableCollection<T> _allData = new ObservableCollection<T>();

        public CustomViewModel(System.Linq.Expressions.Expression<Func<T, bool>> findBy, string sortBy, string sortDirection)
        {
            var param = Expression.Parameter(typeof(T), "item");
            var sortExpression = Expression.Lambda<Func<T, object>>
                                    (Expression.Convert(Expression.Property(param, sortBy), typeof(object)), param);

            var allData = Controllers.BusinessController.Instance.FindBy<T>(findBy).ToList();

            switch (sortDirection.ToLower())
            {
                case "asc":
                    allData = allData.AsQueryable<T>().OrderBy<T, object>(sortExpression).ToList();
                    break;
                default:
                    allData = allData.AsQueryable<T>().OrderByDescending<T, object>(sortExpression).ToList();
                    break;
            }

            _allData = new ObservableCollection<T>(allData.ToList());
        }

        public ObservableCollection<T> ObservableData
        {
            get { return _allData; }
        }
    }

    public enum EventStatus
    {
        CANCELED = 1,
        COMPLETED = 2,
        EXCEPTION = 3,
        PATIENT_SKIPS = 4,
        PENDING = 5
    }
}
