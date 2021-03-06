﻿using Controllers;
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
        #region Instance variables
        private Model.User _userLoggedIn;
        #endregion

        #region Constructors
        public AgendaWindow(Model.User userLoggedIn)
        {
            InitializeComponent();

            InitializeEventStatusFilters();

            _userLoggedIn = userLoggedIn;
            scheduler.UserLoggedIn = _userLoggedIn;
            lblLoggedIn.ToolTip = lblLoggedIn.Content = _userLoggedIn.FirstName + " " + _userLoggedIn.LastName;
            lblLoggedIn.FontWeight = _userLoggedIn.IsAdmin ? FontWeights.Bold : lblLoggedIn.FontWeight;

            calendar.SelectedDate = DateTime.Now;

            SetSchedulerColors();
        }
        #endregion

        #region Window event handlers
        private void scheduler_OnEventMouseLeftButtonDown(object sender, WpfScheduler.Event e)
        {
            LoadEventInfo(e);
        }

        private void LoadEventInfo(WpfScheduler.Event e)
        {
            lblEventId.ToolTip = lblEventId.Text = e.EventInfo.EventId.ToString();
            lblEventStartTime.ToolTip = lblEventStartTime.Text = e.EventInfo.StartEvent.ToString("HH:mm") + " hrs";
            lblEventEndTime.ToolTip = lblEventEndTime.Text = e.EventInfo.EndEvent.ToString("HH:mm") + " hrs";
            lblExpNo.ToolTip = lblExpNo.Text = e.EventInfo.Patient.AssignedId.ToString();
            lblPacientName.ToolTip = lblPacientName.Text = e.EventInfo.Patient.FirstName + " " + e.EventInfo.Patient.LastName;
            lblCellPhone.ToolTip = lblCellPhone.Text = e.EventInfo.Patient.CellPhone;
            lblHomePhone.ToolTip = lblHomePhone.Text = e.EventInfo.Patient.HomePhone;
            lblEmail.ToolTip = lblEmail.Text = e.EventInfo.Patient.Email;
            lblEventStatus.ToolTip = lblEventStatus.Text = e.EventStatusString;
            lblEventCapturer.ToolTip = lblEventCapturer.Text = e.EventInfo.User.FirstName + " " + e.EventInfo.User.LastName;

            lblUpdateClinicHistoryMessage.Visibility = MainWindow.HasPatientToUpdateClinicHistory(e.EventInfo.Patient)
                                                                ? System.Windows.Visibility.Visible
                                                                : System.Windows.Visibility.Hidden;

            lblNextEvent.ToolTip = lblNextEvent.Text = GetNextPendingEvent(e);
        }

        private void scheduler_OnScheduleAddEvent(object sender, System.DateTime e)
        {
            AddEvent(e);
        }

        private void scheduler_OnScheduleContextMenuEvent(object sender, WpfScheduler.Event e)
        {
            string menuEventAction = (sender as MenuItem).Tag.ToString();

            if (menuEventAction == "VIEW_EVENT_STATUS_CHANGES")
            {
                new EventStatusChangesWindow(e).ShowDialog();
                return;
            }

            bool? eventModified = ModifyEventStatus(e, (EventStatus)Enum.Parse(typeof(EventStatus), menuEventAction, true), _userLoggedIn.UserId);
            if (eventModified == true)
            {
                scheduler.RepaintEvents();
            }
        }

        private void calendar_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LoadScheduler(calendar.SelectedDate.Value);
        }

        private void btnToday_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            calendar.SelectedDate = DateTime.Now;
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
                case "6":
                    if (scheduler.ConfirmedEventsVisible != chk.IsChecked.Value)
                    {
                        scheduler.ConfirmedEventsVisible = chk.IsChecked.Value;
                        scheduler.RepaintEvents();
                    }
                    break;
                default:
                    break;
            }
        }

        private void cpStatusEvents_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Xceed.Wpf.Toolkit.ColorPicker colorPicker = sender as Xceed.Wpf.Toolkit.ColorPicker;
            string selectedColor = colorPicker.SelectedColor.Value.ToString();
            string colorConfigurationName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + colorPicker.Tag.ToString();
            
            bool colorAddedSuccessfully = BusinessController.Instance.AddUpdateConfiguration(colorConfigurationName, selectedColor);

            if (colorAddedSuccessfully == false)
            {
                MessageBox.Show("El color seleccionado no pudo ser guardado en la configuración", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            scheduler.RepaintEvents();
        }
        #endregion

        #region Window's logic
        private void InitializeEventStatusFilters()
        {
            scheduler.CanceledEventsVisible = chkCanceledEvents.IsChecked.Value;
            scheduler.ExceptionEventsVisible = chkExceptionEvents.IsChecked.Value;
            scheduler.PatientSkipsEventsVisible = chkPatientSkipsEvents.IsChecked.Value;
            scheduler.PendingEventsVisible = chkPendingEvents.IsChecked.Value;
            scheduler.CompletedEventsVisible = chkCompletedEvents.IsChecked.Value;
            scheduler.ConfirmedEventsVisible = chkConfirmedEvents.IsChecked.Value;
        }

        private void SetSchedulerColors()
        {
            List<Model.Configuration> scheduleColors = BusinessController.Instance.FindBy<Model.Configuration>(c => c.Name.Contains(Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX)).ToList();
            Model.Configuration canceledEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.CANCELED.ToString()).FirstOrDefault();
            Model.Configuration exceptionEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.EXCEPTION.ToString()).FirstOrDefault();
            Model.Configuration patientSkipsEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.PATIENT_SKIPS.ToString()).FirstOrDefault();
            Model.Configuration completedEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.COMPLETED.ToString()).FirstOrDefault();
            Model.Configuration pendingEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.PENDING.ToString()).FirstOrDefault();
            Model.Configuration confirmedEventColor = scheduleColors.Where(c => c.Name == Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.CONFIRMED.ToString()).FirstOrDefault();

            FillRectangleColor(cpCanceledEvents, canceledEventColor);
            FillRectangleColor(cpExceptionEvents, exceptionEventColor);
            FillRectangleColor(cpPatientSkipsEvents, patientSkipsEventColor);
            FillRectangleColor(cpCompletedEvents, completedEventColor);
            FillRectangleColor(cpPendingEvents, pendingEventColor);
            FillRectangleColor(cpConfirmedEvents, confirmedEventColor);
        }

        private void FillRectangleColor(Xceed.Wpf.Toolkit.ColorPicker cpEvents, Model.Configuration eventColor)
        {
            if (eventColor != null)
            {
                cpEvents.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(eventColor.Value));
            }
        }

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
        #endregion

        #region Logic used in another window
        public void RepaintSchedulerIfDateModifiedIsSelected(List<DateTime> datesUpdates)
        {
            if (datesUpdates.Count(du => du.Date == scheduler.SelectedDate.Date) > 0)
            {
                LoadScheduler(scheduler.SelectedDate.Date);
            }
        }

        public static bool? ModifyEventStatus(WpfScheduler.Event e, EventStatus es, int userLoggedInId)
        {
            string oldEventStatus = e.EventStatus.ToString();

            switch (es)
            {
                case EventStatus.CANCELED:
                    if (e.EventInfo.IsCanceled && e.EventInfo.IsCompleted == false 
                        && e.EventInfo.PatientSkips == false && e.EventInfo.IsConfirmed == false)
                    {
                        return null;
                    }

                    e.EventInfo.IsCanceled = true;
                    e.EventInfo.IsCompleted = false;
                    e.EventInfo.PatientSkips = false;
                    e.EventInfo.IsConfirmed = false;
                    break;
                case EventStatus.COMPLETED:
                    if (e.EventInfo.IsCanceled == false && e.EventInfo.IsCompleted
                        && e.EventInfo.PatientSkips == false && e.EventInfo.IsConfirmed == false)
                    {
                        return null;
                    }

                    e.EventInfo.IsCanceled = false;
                    e.EventInfo.IsCompleted = true;
                    e.EventInfo.PatientSkips = false;
                    e.EventInfo.IsConfirmed = false;
                    break;
                case EventStatus.PATIENT_SKIPS:
                    if (e.EventInfo.IsCanceled == false && e.EventInfo.IsCompleted
                        && e.EventInfo.PatientSkips && e.EventInfo.IsConfirmed == false)
                    {
                        return null;
                    }

                    e.EventInfo.IsCanceled = false;
                    e.EventInfo.IsCompleted = true;
                    e.EventInfo.PatientSkips = true;
                    e.EventInfo.IsConfirmed = false;
                    break;

                case EventStatus.CONFIRMED:
                    if (e.EventInfo.IsCanceled == false && e.EventInfo.PatientSkips == false
                        && e.EventInfo.IsCompleted == false && e.EventInfo.IsConfirmed)
                    {
                        return null;
                    }

                    e.EventInfo.IsCanceled = false;
                    e.EventInfo.IsCompleted = false;
                    e.EventInfo.PatientSkips = false;
                    e.EventInfo.IsConfirmed = true;
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
                    e.EventInfo.IsConfirmed = false;

                    return false;
                }
            }

            if (BusinessController.Instance.Update<Model.Event>(e.EventInfo))
            {
                bool eventStatusChangeRegistered = Utils.AddEventStatusChanges(oldEventStatus, e.EventStatus.ToString(), e.EventInfo.EventId, userLoggedInId);
                if (eventStatusChangeRegistered == false)
                {
                    MessageBox.Show("No se pudo guardar el cambio registrado en el estado de la cita", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                CreateFunctionalityBasedOnEventStatus(e, es);

                return true;
            }

            MessageBox.Show("No pudo ser modificado el estado de la cita", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        private static void CreateFunctionalityBasedOnEventStatus(WpfScheduler.Event e, EventStatus es)
        {   
            //Creating reminder for recurrent treatments
            if (es == EventStatus.COMPLETED && e.EventInfo.Treatment.Recurrent != null)
            {
                string reminderMessage = "El paciente '" + e.EventInfo.Patient.FirstName + " " + e.EventInfo.Patient.LastName + "'"
                                        + " con Exp. No. " + e.EventInfo.Patient.AssignedId + " tomó el tratamiento de '" + e.EventInfo.Treatment.Name + "' el día "
                                        + e.EventInfo.StartEvent.ToString("D") + " a las " + e.EventInfo.StartEvent.ToString("HH:mm") + " hrs. "
                                        + "\nDado que este tratamiento es recurrente es necesario que llame al paciente para agendar de nuevo una cita.";

                Model.Reminder reminderToAdd = new Model.Reminder()
                {
                    Message = reminderMessage,
                    AppearDate = e.EventInfo.StartEvent.AddDays(e.EventInfo.Treatment.Recurrent.Value),
                    CreatedDate = DateTime.Now,
                    RequireAdmin = true,
                    Seen = false,
                    SeenBy = null,
                    AutoGenerated = true
                };

                if (Controllers.BusinessController.Instance.Add<Model.Reminder>(reminderToAdd) == false)
                {
                    MessageBox.Show("No se pudo generar un recordatorio para esta cita que es de tratammiento recurrente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


            if (es == EventStatus.CANCELED)
            {
                //If the event is canceled, then increment UsesLeft to the selected instrument
                //TODO: Now the user is able to make an event regardless if there is instruments available.
                //      So, we can't increment the UsesLeft followint the old logic.
                //if (e.EventInfo.Instrument != null)
                //{
                //    e.EventInfo.Instrument.UsesLeft = e.EventInfo.Instrument.UsesLeft.Value + 1;
                //    if (BusinessController.Instance.Update<Model.Instrument>(e.EventInfo.Instrument) == false)
                //    {
                //        MessageBox.Show("No se pudo incrementar la cantidad de usos al instrumento que iba a ser utilizado en esta cita", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //    }   
                //}

                //Send email to patient for recurrent canceled events
                List<Model.Event> canceledEventsInARow = GetPatientCanceledEventsInARow(e.EventInfo.Patient.PatientId, 3);
                List<Model.Event> canceledEventsOfSameTreatment = GetPatientCanceledEventsOfSameTreatment(e.EventInfo.Patient.PatientId, e.EventInfo.TreatmentId, 3);

                if (canceledEventsInARow != null || canceledEventsOfSameTreatment != null)
                {
                    new CanceledEventsSendEmailModal(canceledEventsInARow, canceledEventsOfSameTreatment, e.EventInfo.Patient, e.EventInfo.Treatment).ShowDialog();
                }
            }   
        }

        public static List<Model.Event> GetPatientCanceledEventsInARow(int patientId, int consecutiveEvents)
        {
            List<Model.Event> canceledEventsInARow = BusinessController.Instance.FindBy<Model.Event>(e => e.PatientId == patientId && !(!e.IsCanceled && !e.IsCompleted && !e.PatientSkips))
                                                                                .OrderByDescending(e => e.StartEvent)
                                                                                .Take(consecutiveEvents)
                                                                                .ToList();

            if (canceledEventsInARow.Count(e => e.IsCanceled) == consecutiveEvents)
                return canceledEventsInARow;
            else
                return null;
        }

        private static List<Model.Event> GetPatientCanceledEventsOfSameTreatment(int patientId, int treatmentId, int consecutiveEvents)
        {
            List<Model.Event> canceledEventsOfSameTreatment = BusinessController.Instance.FindBy<Model.Event>(e => e.PatientId == patientId && e.TreatmentId == treatmentId && e.IsCanceled)
                                                                                            .ToList();

            if (canceledEventsOfSameTreatment.Count % consecutiveEvents == 0)
                return canceledEventsOfSameTreatment.OrderByDescending(e => e.StartEvent)
                                                    .Take(consecutiveEvents)
                                                    .ToList();
            else
                return null;
        }

        public static Model.Event OverlappedWithExistingEvent(Model.Event eventToAdd, List<WpfScheduler.Event> events)
        {
            events = events.Where(e => e.EventInfo.IsCanceled == false && e.EventInfo.PatientSkips == false)
                            .OrderBy(e => e.EventInfo.StartEvent)
                            .ToList();

            foreach (WpfScheduler.Event e in events)
            {
                if (Utils.IsOverlappedTime(eventToAdd.StartEvent, eventToAdd.EndEvent, e.EventInfo.StartEvent, e.EventInfo.EndEvent))
                {
                    return e.EventInfo;
                }
            }

            return null;
        }

        private string GetNextPendingEvent(WpfScheduler.Event e)
        {
            string nextEventDate = string.Empty;

            if (e.EventStatus == EventStatus.CANCELED)
            {
                Model.Event nextPendingEvent = BusinessController.Instance.FindBy<Model.Event>(
                                                            ev =>
                                                                ev.PatientId == e.EventInfo.PatientId
                                                                && !ev.IsCanceled
                                                                && !ev.PatientSkips
                                                                && !ev.IsCompleted
                                                                && e.EventInfo.StartEvent < ev.StartEvent
                                                            )
                                                            .OrderBy(ev => ev.StartEvent)
                                                            .FirstOrDefault();

                nextEventDate = nextPendingEvent == null
                                ? nextEventDate
                                : nextPendingEvent.StartEvent.ToString("dd/MMMM/yyyy");
            }

            return nextEventDate;
        }
        #endregion
    }
}
