using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyDentApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            calendar.SelectedDate = DateTime.Now;
            rcCanceledEvents.Fill = Brushes.OrangeRed;
            rcExceptionEvents.Fill = Brushes.Yellow;
            rcPatientNotCameEvents.Fill = Brushes.Red;
            rcCompletedEvents.Fill = Brushes.Green;
            rcNotCompletedEvents.Fill = Brushes.Orange;
        }

        #region Control's events

        private void scheduler_OnEventMouseLeftButtonDown(object sender, WpfScheduler.Event e)
        {
            LoadEventInfo(e);
        }

        private void LoadEventInfo(WpfScheduler.Event e)
        {
            lblEventStartTime.Text = e.EventInfo.StartEvent.ToString("HH:mm") + " hrs";
            lblEventEndTime.Text = e.EventInfo.EndEvent.ToString("HH:mm") + " hrs";
            lblExpNo.Text = e.EventInfo.Patient.PatientId.ToString();
            lblPacientName.Text = e.EventInfo.Patient.FirstName + " " + e.EventInfo.Patient.LastName;
            lblCellPhone.Text = e.EventInfo.Patient.CellPhone;
            lblHomePhone.Text = e.EventInfo.Patient.HomePhone;
            lblEmail.Text = e.EventInfo.Patient.Email;
            lblEventStatus.Text = e.EventStatus;
        }

        private void scheduler_OnScheduleAddEvent(object sender, System.DateTime e)
        {
            AddEvent(e);
        }        

        private void scheduler_OnScheduleCancelEvent(object sender, WpfScheduler.Event e)
        {
            CancelEvent(e);
        }

        private void calendar_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadScheduler(calendar.SelectedDate.Value);
        }

        #endregion

        private void AddEvent(System.DateTime eventToAdd)
        {
            Model.Event ev = new Model.Event()
            {
                StartEvent = eventToAdd,
                EndEvent = eventToAdd.AddMinutes(60.0 / scheduler.HourIntervals)
            };

            Model.Event overlappedEvent = OverlappedWithExistingEvent(ev, scheduler.Events.ToList());
            if (overlappedEvent != null)
            {
                MessageBox.Show(string.Format("No se puede agendar una cita a las {0} hrs."
                                    +"\nExiste una cita para el tratamiento de '{1}' que inicia a las {2} hrs"
                                    + " y termina a las {3} hrs.",
                                    eventToAdd.ToString("HH:mm"),
                                    overlappedEvent.Treatment.Name,
                                    overlappedEvent.StartEvent.ToString("HH:mm"),
                                    overlappedEvent.EndEvent.ToString("HH:mm")), 
                                "Advertencia", 
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                return;
            }
                
            new AddEventModal(scheduler, eventToAdd).ShowDialog();   
        }

        public static Model.Event OverlappedWithExistingEvent(Model.Event eventToAdd, List<WpfScheduler.Event> events)
        {
            foreach (WpfScheduler.Event e in events.Where(e => e.EventInfo.IsCanceled == false))
            {
                if (Controllers.Utils.IsOverlappedTime(eventToAdd.StartEvent, eventToAdd.EndEvent, e.EventInfo.StartEvent, e.EventInfo.EndEvent))
                {
                    return e.EventInfo;
                }
            }

            return null;
        }

        private void LoadScheduler(DateTime dateToLoad)
        {
            scheduler.Events.Clear();
            scheduler.SelectedDate = dateToLoad;

            List<Model.Event> events = Controllers.BusinessController.Instance.FindBy<Model.Event>(
                                                e => EntityFunctions.TruncateTime(e.StartEvent) == EntityFunctions.TruncateTime(dateToLoad)
                                        ).ToList();

            foreach (Model.Event ev in events)
            {
                scheduler.AddEvent(new WpfScheduler.Event() { EventInfo = ev });
            }
        }

        private void CancelEvent(WpfScheduler.Event e)
        {
            if (e.EventInfo.IsCanceled || e.EventInfo.IsCompleted)
            {
                MessageBox.Show("Esta cita ya no puede ser cancelada", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                e.EventInfo.IsCanceled = true;

                if (Controllers.BusinessController.Instance.Update<Model.Event>(e.EventInfo))
                {
                    scheduler.RepaintEvents();
                }
                else
                {
                    MessageBox.Show("No pudo ser cancelada la cita", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void filterEvents_CheckedUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox chk = (sender as CheckBox);

            if (chk.Tag == null)
            {
                return;
            }

            switch (chk.Tag.ToString())
            {
                case "1":
                    if (scheduler.CanceledEventsVisible != chk.IsChecked.Value)
                    {
                        scheduler.CanceledEventsVisible = chk.IsChecked.Value;
                        scheduler.RepaintEvents();
                    }
                    break;
                case "2":
                    if (scheduler.ExceptionEventsVisible != chk.IsChecked.Value)
                    {
                        scheduler.ExceptionEventsVisible = chk.IsChecked.Value;
                        scheduler.RepaintEvents();
                    }
                    break;
                case "3":
                    if (scheduler.PatientNotCameEventsVisible != chk.IsChecked.Value)
                    {
                        scheduler.PatientNotCameEventsVisible = chk.IsChecked.Value;
                        scheduler.RepaintEvents();
                    }
                    break;
                case "4":
                    if (scheduler.NotCompletedEventsVisible != chk.IsChecked.Value)
                    {
                        scheduler.NotCompletedEventsVisible = chk.IsChecked.Value;
                        scheduler.RepaintEvents();
                    }
                    break;
                case "5":
                    if (scheduler.PatientCameEventsVisible != chk.IsChecked.Value)
                    {
                        scheduler.PatientCameEventsVisible = chk.IsChecked.Value;
                        scheduler.RepaintEvents();
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnToday_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            calendar.SelectedDate = DateTime.Now;
        }
    }
}
