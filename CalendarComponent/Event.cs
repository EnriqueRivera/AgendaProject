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
        public Events EventInfo { get; set; }
        public Brush _color;

        public Event()
        {
            Id = Guid.NewGuid();
        }

        public Brush Color
        {
            get
            {
                if (EventInfo.IsCanceled)
                {
                    return Brushes.Blue;
                }

                if (EventInfo.IsCompleted)
                {
                    return (EventInfo.PatientCame) ? Brushes.Green : Brushes.Red;
                }

                return Brushes.Orange;
            }
        }
    }

    public class Events
    {
        public int EventId { get; set; }
        public DateTime StartEvent { get; set; }
        public DateTime EndEvent { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsCompleted { get; set; }
        public bool PatientCame { get; set; }
        public int PatientId { get; set; }
        public int TreatmentId { get; set; }
    }
}
