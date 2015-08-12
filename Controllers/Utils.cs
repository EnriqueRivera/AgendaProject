using System;
using System.Collections.Generic;
using System.Linq;
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
