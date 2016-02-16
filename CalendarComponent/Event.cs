using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfScheduler
{
    public class Event
    {
        public Guid Id { get; set; }
        public Model.Event EventInfo { get; set; }
        public Brush _color;

        public Event()
        {
            Id = Guid.NewGuid();
        }

        public Brush Color
        {
            get
            {
                string configurationName = Controllers.Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.ToString();
                Model.Configuration eventColor = Controllers.BusinessController.Instance.FindBy<Model.Configuration>(c => c.Name == configurationName).FirstOrDefault();

                return eventColor != null ? (SolidColorBrush)(new BrushConverter().ConvertFrom(eventColor.Value)) : Brushes.Transparent;
            }
        }

        public Controllers.EventStatus EventStatus
        {
            get
            {
                if (EventInfo.IsCanceled)
                {
                    return Controllers.EventStatus.CANCELED;
                }

                if (EventInfo.IsCompleted)
                {
                    return (EventInfo.PatientSkips) ? Controllers.EventStatus.PATIENT_SKIPS : Controllers.EventStatus.COMPLETED;
                }

                if (EventInfo.IsException)
                {
                    return Controllers.EventStatus.EXCEPTION;
                }

                if (EventInfo.IsConfirmed)
                {
                    return Controllers.EventStatus.CONFIRMED;
                }

                return Controllers.EventStatus.PENDING;
            }
        }

        public string EventStatusString
        {
            get
            {
                return Controllers.Utils.EventStatusString(EventStatus);
            }
        }
    }
}
