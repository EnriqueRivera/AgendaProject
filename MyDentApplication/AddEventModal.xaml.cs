using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for AddEventModal.xaml
	/// </summary>
	public partial class AddEventModal : Window
	{
        private WpfScheduler.Scheduler _scheduler;
        private List<Model.Patient> _patients;
        private List<Model.Treatment> _treatments;
        private DateTime _eventStart;
        private Model.Patient _selectedPatient;
        private Model.Treatment _selectedTreatment;
        private Model.User _userLoggedIn;

        public AddEventModal(WpfScheduler.Scheduler scheduler, DateTime eventStart, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _scheduler = scheduler;
            _eventStart = eventStart;
            _userLoggedIn = userLoggedIn;

            this.Title = "Agendar cita (" + eventStart.ToString("D") + ")";
            lblEventStartTime.ToolTip = lblEventStartTime.Text = eventStart.ToString("HH:mm") + " hrs";

            GetPatients();
            GetTreatments();
		}

        private void GetTreatments()
        {
            var allTreatments = Controllers.BusinessController.Instance.GetAll<Model.Treatment>();

            if (allTreatments != null)
	        {
                _treatments = allTreatments
                            .OrderBy(t => t.Name)
                            .ThenBy(t => t.Duration)
                            .ToList();

                foreach (Model.Treatment treatment in _treatments)
                {
                    cbTratmentName.Items.Add(new ComboBoxItem() { Text = treatment.Name, Value = treatment });
                }
	        }
        }

        private void GetPatients()
        {
            var allPatients = Controllers.BusinessController.Instance.GetAll<Model.Patient>();

            if (allPatients != null)
            {
                _patients = allPatients
                            .OrderBy(p => p.FirstName)
                            .ThenBy(p => p.LastName)
                            .ToList();

                foreach (Model.Patient patient in _patients)
                {
                    cbPatientName.Items.Add(new ComboBoxItem() { Text = patient.FirstName + " " + patient.LastName, Value = patient });
                }
            }            
        }

        private void cbPatientName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedPatient = (cbPatientName.SelectedValue as ComboBoxItem).Value as Model.Patient;

            FillPatientFields(_selectedPatient);
        }

        private void cbTratmentName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedTreatment = (cbTratmentName.SelectedValue as ComboBoxItem).Value as Model.Treatment;

            FillTreatmentFields(_selectedTreatment);

            lblEventEndTime.ToolTip = lblEventEndTime.Text = _eventStart.AddMinutes(_selectedTreatment.Duration).ToString("HH:mm") + " hrs";
        }
		
		private void FillPatientFields(Model.Patient selectedPatient) 
		{
			if (selectedPatient == null) 
			{
                lblExpNo.ToolTip = lblExpNo.Text = string.Empty;
                lblCellphone.ToolTip = lblCellphone.Text = string.Empty;
                lblHomePhone.ToolTip = lblHomePhone.Text = string.Empty;
                lblEmail.ToolTip = lblEmail.Text = string.Empty;
			}
			else
			{
                lblExpNo.ToolTip = lblExpNo.Text = selectedPatient.PatientId.ToString();
                lblCellphone.ToolTip = lblCellphone.Text = selectedPatient.CellPhone;
                lblHomePhone.ToolTip = lblHomePhone.Text = selectedPatient.HomePhone;
                lblEmail.ToolTip = lblEmail.Text = selectedPatient.Email;
			}
		}

        private void FillTreatmentFields(Model.Treatment selectedTreatment)
        {
            lblDuration.ToolTip = lblDuration.Text = selectedTreatment == null ? string.Empty : selectedTreatment.Duration.ToString() + " minutos";
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cbPatientName.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un paciente", "Informaicón", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (cbTratmentName.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tratamiento", "Informaicón", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            List<Model.Event> skippedEvents = ((cbPatientName.SelectedValue as ComboBoxItem).Value as Model.Patient).Events
                                                .Where(ev => ev.IsCompleted && ev.PatientSkips)
                                                .OrderBy(ev => ev.StartEvent)
                                                .ToList();

            Model.Configuration skippedEventsConfiguration = Controllers.BusinessController.Instance.FindBy<Model.Configuration>(c => c.Name == Controllers.Utils.PATIENT_MAX_SKIPPED_EVENTS_CONFIGURATION).FirstOrDefault();

            int maxSkippedEvents;
            if (int.TryParse(skippedEventsConfiguration == null ? "3" : skippedEventsConfiguration.Value, out maxSkippedEvents) == false)
            {
                maxSkippedEvents = 3;
            }

            if (skippedEvents.Count >= maxSkippedEvents)
            {
                string skippedEventsMessage = string.Empty;
                for (int i = 0; i < skippedEvents.Count; i++)
                {
                    skippedEventsMessage += string.Format(
                                                "\nFalta #{0}:" + 
                                                "\n -Tratamiento: {1}" + 
                                                "\n -Día de la cita: {2}" + 
                                                "\n -Hora de inicio de la cita: {3}" + 
                                                "\n -Hora de fin de la cita: {4}",
                                                i + 1,
                                                skippedEvents[i].Treatment.Name,
                                                skippedEvents[i].StartEvent.ToString("D"),
                                                skippedEvents[i].StartEvent.ToString("HH:mm") + " hrs",
                                                skippedEvents[i].EndEvent.ToString("HH:mm") + " hrs" + (i == skippedEvents.Count - 1 ? "" : "\n"));
                }

                MessageBox.Show(string.Format("El paciente seleccionado no puede agendar cita, ya que cuenta con {0} {1} y el máximo permitido es de {2} {3}\n{4}",
                                                skippedEvents.Count,
                                                skippedEvents.Count == 1 ? "falta" : "faltas",
                                                maxSkippedEvents,
                                                maxSkippedEvents == 1 ? "falta" : "faltas",
                                                skippedEventsMessage
                                                ),
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            Model.Event eventToAdd = new Model.Event();
            if (IsValidEvent(eventToAdd))
            {
                if (Controllers.BusinessController.Instance.Add<Model.Event>(eventToAdd))
                {
                    _scheduler.AddEvent(new WpfScheduler.Event() { EventInfo = eventToAdd });
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No pudo ser agendada la cita", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool IsValidEvent(Model.Event eventToAdd)
        {
            eventToAdd.StartEvent = _eventStart;
            eventToAdd.EndEvent = _eventStart.AddMinutes(_selectedTreatment.Duration);
            eventToAdd.IsException = false;
            eventToAdd.IsCanceled = false;
            eventToAdd.IsCompleted = false;
            eventToAdd.PatientSkips = false;
            eventToAdd.PatientId = _selectedPatient.PatientId;
            eventToAdd.TreatmentId = _selectedTreatment.TreatmentId;
            eventToAdd.EventCapturerId = _userLoggedIn.UserId;


            Model.Event overlappedEvent = AgendaWindow.OverlappedWithExistingEvent(eventToAdd, _scheduler.Events.ToList());
            if (overlappedEvent != null)
            {
                if (MessageBox.Show
                                (string.Format("La cita que intenta agendar se traslapa con la cita para el tratamiento de '{0}' "
                                    + "que inicia a las {1} hrs y termina a las {2} hrs."
                                    + "\n\n¿Desea que sea ajustado el tiempo de la cita que desea agreagar?", 
                                        overlappedEvent.Treatment.Name, 
                                        overlappedEvent.StartEvent.ToString("HH:mm"), 
                                        overlappedEvent.EndEvent.ToString("HH:mm")),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Information
                                ) == MessageBoxResult.Yes)
                {
                    if (IsValidAdminPassword())
                    {
                        eventToAdd.EndEvent = overlappedEvent.StartEvent;
                        eventToAdd.IsException = true;
                        return true;   
                    }
                }

                return false;
            }

            if (IsTimeRangeException(eventToAdd.StartEvent, eventToAdd.EndEvent))
            {
                if (IsValidAdminPassword())
                {
                    eventToAdd.IsException = true;
                    return true;   
                }

                return false;
            }

            return true;
        }

        private bool IsValidAdminPassword()
        {
            if (_userLoggedIn.IsAdmin)
            {
                return true;
            }

            Model.User userResult = new Model.User();
            new RequestAdminCredentialsModal(userResult).ShowDialog();

            return userResult.IsAdmin;
        }

        private bool IsTimeRangeException(DateTime eventToAddStart, DateTime eventToAddEnd)
        {
            DateTime firstExtraHourMinRange = new DateTime(eventToAddStart.Year, eventToAddStart.Month, eventToAddStart.Day, _scheduler.MinHour, 0, 0);
            DateTime firstExtraHourMaxRange = new DateTime(eventToAddStart.Year, eventToAddStart.Month, eventToAddStart.Day, _scheduler.FinishFirstExtraHour, 0, 0);
            DateTime secondExtraHourMinRange = new DateTime(eventToAddStart.Year, eventToAddStart.Month, eventToAddStart.Day, _scheduler.StartSecondExtraHour, 0, 0);
            DateTime secondExtraHourMaxRange = new DateTime(eventToAddStart.Year, eventToAddStart.Month, eventToAddStart.Day, _scheduler.MaxHour, 0, 0);


            return Controllers.Utils.IsOverlappedTime(eventToAddStart, eventToAddEnd, firstExtraHourMinRange, firstExtraHourMaxRange)
                    || Controllers.Utils.IsOverlappedTime(eventToAddStart, eventToAddEnd, secondExtraHourMinRange, secondExtraHourMaxRange);
        }
        

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}