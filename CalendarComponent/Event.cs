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
                if (EventInfo.IsCanceled)
                {
                    return Brushes.OrangeRed;
                }

                if (EventInfo.IsException)
                {
                    return Brushes.Yellow;
                }

                if (EventInfo.IsCompleted)
                {
                    return (EventInfo.PatientCame) ? Brushes.Green : Brushes.Red;
                }

                return Brushes.Orange;
            }
        }

        public string EventStatus
        {
            get
            {
                if (EventInfo.IsCanceled)
                {
                    return "Cancelada";
                }

                if (EventInfo.IsException)
                {
                    return "Excepción";
                }

                if (EventInfo.IsCompleted)
                {
                    return (EventInfo.PatientCame) ? "Completada" : "Paciente no asisitó";
                }

                return "Sin concretar";
            }
        }
    }
}
