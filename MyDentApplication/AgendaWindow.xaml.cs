using Controllers;
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
    public partial class AgendaWindow : Window
    {
        private Model.User _userLoggedIn;

        public AgendaWindow(Model.User userLoggedIn)
        {
            CheckGlobalConfigurations();

            InitializeComponent();

            InitializeEventStatusFilters();

            _userLoggedIn = userLoggedIn;
            scheduler.UserLoggedIn = _userLoggedIn;
            lblLoggedIn.ToolTip = lblLoggedIn.Content = _userLoggedIn.FirstName + " " + _userLoggedIn.LastName;
            lblLoggedIn.FontWeight = _userLoggedIn.IsAdmin ? FontWeights.Bold : lblLoggedIn.FontWeight;

            calendar.SelectedDate = DateTime.Now;

            SetSchedulerColors();
        }

        private void InitializeEventStatusFilters()
        {
            scheduler.CanceledEventsVisible = chkCanceledEvents.IsChecked.Value;
            scheduler.ExceptionEventsVisible = chkExceptionEvents.IsChecked.Value;
            scheduler.PatientSkipsEventsVisible = chkPatientSkipsEvents.IsChecked.Value;
            scheduler.PendingEventsVisible = chkPendingEvents.IsChecked.Value;
            scheduler.CompletedEventsVisible = chkCompletedEvents.IsChecked.Value;
        }

        private void SetSchedulerColors()
        {
            var scheduleColorsResult = BusinessController.Instance.FindBy<Model.Configuration>(c => c.Name.Contains(Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX));

            if (scheduleColorsResult != null)
	        {
                List<Model.Configuration> scheduleColors = scheduleColorsResult.ToList();
                Model.Configuration canceledEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.CANCELED.ToString()).FirstOrDefault();
                Model.Configuration exceptionEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.EXCEPTION.ToString()).FirstOrDefault();
                Model.Configuration patientSkipsEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.PATIENT_SKIPS.ToString()).FirstOrDefault();
                Model.Configuration completedEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.COMPLETED.ToString()).FirstOrDefault();
                Model.Configuration pendingEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.PENDING.ToString()).FirstOrDefault();

                FillRectangleColor(cpCanceledEvents, canceledEventColor);
                FillRectangleColor(cpExceptionEvents, exceptionEventColor);
                FillRectangleColor(cpPatientSkipsEvents, patientSkipsEventColor);
                FillRectangleColor(cpCompletedEvents, completedEventColor);
                FillRectangleColor(cpPendingEvents, pendingEventColor);
	        }
        }

        private void FillRectangleColor(Xceed.Wpf.Toolkit.ColorPicker cpEvents, Model.Configuration eventColor)
        {
            if (eventColor != null)
            {
                cpEvents.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(eventColor.Value));
            }
        }

        private void CheckGlobalConfigurations()
        {
            bool configurationAddedSuccessfully = CheckSchedulerColorsConfiguration() && CheckMaxSkipsEventsConfiguration();
            if (configurationAddedSuccessfully == false)
            {
                MessageBox.Show("Alguna configuración inicial de la aplicación no pudo ser creada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckMaxSkipsEventsConfiguration()
        {
            return BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == Utils.PATIENT_MAX_SKIPPED_EVENTS_CONFIGURATION, new Model.Configuration() { Name = Utils.PATIENT_MAX_SKIPPED_EVENTS_CONFIGURATION, Value = "3" });
        }

        private bool CheckSchedulerColorsConfiguration()
        {
            string cancelEventConfigName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.CANCELED.ToString();
            string exceptionEventConfigName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.EXCEPTION.ToString();
            string completedEventConfigName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.COMPLETED.ToString();
            string patientSkipsEventConfigName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.PATIENT_SKIPS.ToString();
            string pendingEventConfigName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.PENDING.ToString();

            return
                BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == cancelEventConfigName, new Model.Configuration() { Name = cancelEventConfigName, Value = Brushes.OrangeRed.ToString() })
                & BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == exceptionEventConfigName, new Model.Configuration() { Name = exceptionEventConfigName, Value = Brushes.Yellow.ToString() })
                & BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == completedEventConfigName, new Model.Configuration() { Name = completedEventConfigName, Value = Brushes.Green.ToString() })
                & BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == patientSkipsEventConfigName, new Model.Configuration() { Name = patientSkipsEventConfigName, Value = Brushes.Red.ToString() })
                & BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == pendingEventConfigName, new Model.Configuration() { Name = pendingEventConfigName, Value = Brushes.Orange.ToString() });
        }

        #region Control's events

        private void scheduler_OnEventMouseLeftButtonDown(object sender, WpfScheduler.Event e)
        {
            LoadEventInfo(e);
        }

        private void LoadEventInfo(WpfScheduler.Event e)
        {
            lblEventId.ToolTip = lblEventId.Text = e.EventInfo.EventId.ToString();
            lblEventStartTime.ToolTip = lblEventStartTime.Text = e.EventInfo.StartEvent.ToString("HH:mm") + " hrs";
            lblEventEndTime.ToolTip = lblEventEndTime.Text = e.EventInfo.EndEvent.ToString("HH:mm") + " hrs";
            lblExpNo.ToolTip = lblExpNo.Text = e.EventInfo.Patient.PatientId.ToString();
            lblPacientName.ToolTip = lblPacientName.Text = e.EventInfo.Patient.FirstName + " " + e.EventInfo.Patient.LastName;
            lblCellPhone.ToolTip = lblCellPhone.Text = e.EventInfo.Patient.CellPhone;
            lblEventStartTime.ToolTip = lblEventStartTime.ToolTip = lblHomePhone.Text = e.EventInfo.Patient.HomePhone;
            lblEmail.ToolTip = lblEmail.Text = e.EventInfo.Patient.Email;
            lblEventStatus.ToolTip = lblEventStatus.Text = e.EventStatusString;
            lblEventCapturer.ToolTip = lblEventCapturer.Text = e.EventInfo.User.FirstName + " " + e.EventInfo.User.LastName;
        }

        private void scheduler_OnScheduleAddEvent(object sender, System.DateTime e)
        {
            AddEvent(e);
        }

        private void scheduler_OnScheduleContextMenuEvent(object sender, WpfScheduler.Event e)
        {
            bool? eventModified = ModifyEventStatus(e, (EventStatus)Enum.Parse(typeof(EventStatus), (sender as MenuItem).Tag.ToString(), true), _userLoggedIn.UserId);
            if (eventModified == true)
            {
                scheduler.RepaintEvents();
            }
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
                EndEvent = eventToAdd.AddMinutes(1.0)
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
                
            new AddEventModal(scheduler, eventToAdd, _userLoggedIn).ShowDialog();   
        }

        public static Model.Event OverlappedWithExistingEvent(Model.Event eventToAdd, List<WpfScheduler.Event> events)
        {
            events = events.OrderBy(e => e.EventInfo.StartEvent).ToList();

            foreach (WpfScheduler.Event e in events.Where(e => e.EventInfo.IsCanceled == false))
            {
                if (Utils.IsOverlappedTime(eventToAdd.StartEvent, eventToAdd.EndEvent, e.EventInfo.StartEvent, e.EventInfo.EndEvent))
                {
                    return e.EventInfo;
                }
            }
            
            return null;
        }

        private void LoadScheduler(DateTime dateToLoad)
        {
            calendar.DisplayDate = dateToLoad;

            scheduler.Events.Clear();
            scheduler.SelectedDate = dateToLoad;

            List<Model.Event> events = BusinessController.Instance.FindBy<Model.Event>(
                                                e => EntityFunctions.TruncateTime(e.StartEvent) == EntityFunctions.TruncateTime(dateToLoad)
                                        ).ToList();

            if (events != null)
            {
                foreach (Model.Event ev in events)
                {
                    scheduler.AddEventWithoutRepaint(new WpfScheduler.Event() { EventInfo = ev });
                }
            }

            scheduler.RepaintEvents();
        }

        public static bool? ModifyEventStatus(WpfScheduler.Event e, EventStatus es, int userLoggedInId)
        {
            string oldEventStatus = e.EventStatus.ToString();

            switch (es)
            {
                case EventStatus.CANCELED:
                    if (e.EventInfo.IsCanceled && e.EventInfo.IsCompleted == false && e.EventInfo.PatientSkips == false)
                    {
                        return null;
                    }

                    e.EventInfo.IsCanceled = true;
                    e.EventInfo.IsCompleted = false;
                    e.EventInfo.PatientSkips = false;
                    break;
                case EventStatus.COMPLETED:
                    if (e.EventInfo.IsCanceled == false && e.EventInfo.IsCompleted && e.EventInfo.PatientSkips == false)
                    {
                        return null;
                    }

                    e.EventInfo.IsCanceled = false;
                    e.EventInfo.IsCompleted = true;
                    e.EventInfo.PatientSkips = false;
                    break;
                case EventStatus.PATIENT_SKIPS:
                    if (e.EventInfo.IsCanceled == false && e.EventInfo.IsCompleted && e.EventInfo.PatientSkips)
                    {
                        return null;
                    }

                    e.EventInfo.IsCanceled = false;
                    e.EventInfo.IsCompleted = true;
                    e.EventInfo.PatientSkips = true;
                    break;
                default:
                    return null;
            }

            if (es == EventStatus.PATIENT_SKIPS)
            {
                if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea indicar que el paciente '{0}' "
                                    + "no asistió a su cita del día '{1}' que inició a las {2} hrs?",
                                        e.EventInfo.Patient.FirstName + " " + e.EventInfo.Patient.LastName,
                                        e.EventInfo.StartEvent.ToString("D"),
                                        e.EventInfo.StartEvent.ToString("HH:mm")),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.No)
                {
                    e.EventInfo.IsCanceled = false;
                    e.EventInfo.IsCompleted = false;
                    e.EventInfo.PatientSkips = false;

                    return false;
                }
            }

            if (BusinessController.Instance.Update<Model.Event>(e.EventInfo))
            {
                bool eventStatusChangeRegistered = Utils.AddEventStatusChanges(oldEventStatus, e.EventStatus.ToString(), e.EventInfo.EventId, userLoggedInId);
                if (eventStatusChangeRegistered == false)
                {
                    MessageBox.Show("No se pudo guardar un registro del cambio registrado en el estado de la cita", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                return true;
            }
            
            MessageBox.Show("No pudo ser modificado el estado de la cita", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
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
                    if (scheduler.PatientSkipsEventsVisible != chk.IsChecked.Value)
                    {
                        scheduler.PatientSkipsEventsVisible = chk.IsChecked.Value;
                        scheduler.RepaintEvents();
                    }
                    break;
                case "4":
                    if (scheduler.PendingEventsVisible != chk.IsChecked.Value)
                    {
                        scheduler.PendingEventsVisible = chk.IsChecked.Value;
                        scheduler.RepaintEvents();
                    }
                    break;
                case "5":
                    if (scheduler.CompletedEventsVisible != chk.IsChecked.Value)
                    {
                        scheduler.CompletedEventsVisible = chk.IsChecked.Value;
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
		
        private void cpStatusEvents_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Xceed.Wpf.Toolkit.ColorPicker colorPicker = sender as Xceed.Wpf.Toolkit.ColorPicker;
            string eventStatusSelected = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + colorPicker.Tag.ToString();
            Model.Configuration statusEventColor = BusinessController.Instance.FindBy<Model.Configuration>(c => c.Name == eventStatusSelected).FirstOrDefault();
            string selectedColor = colorPicker.SelectedColor.Value.ToString();
            bool colorAddedSuccessfully;

            if (statusEventColor == null)
            {
                colorAddedSuccessfully = BusinessController.Instance.Add<Model.Configuration>(new Model.Configuration() { Name = eventStatusSelected, Value = selectedColor });
            }
            else
            {
                statusEventColor.Value = selectedColor;
                colorAddedSuccessfully = BusinessController.Instance.Update<Model.Configuration>(statusEventColor);
            }

            if (colorAddedSuccessfully == false)
            {
                MessageBox.Show("El color seleccionado no pudo ser guardado en la configuración", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            scheduler.RepaintEvents();
        }

        public void RepaintSchedulerFromAnotherThread(List<DateTime> datesUpdates)
        {
            if (datesUpdates.Count(du => du.Date == scheduler.SelectedDate.Date) > 0)
            {
                LoadScheduler(scheduler.SelectedDate.Date);
            }
        }
    }
}
