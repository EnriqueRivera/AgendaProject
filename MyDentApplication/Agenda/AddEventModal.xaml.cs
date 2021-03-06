﻿using System;
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
using Controllers;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for AddEventModal.xaml
	/// </summary>
	public partial class AddEventModal : Window
    {
        #region Instance variables
        private WpfScheduler.Scheduler _scheduler;
        private DateTime _eventStart;
        private Model.Patient _selectedPatient;
        private Model.Treatment _selectedTreatment;
        private Model.User _userLoggedIn;
        private const int maxSkippedEvents = 3;
        #endregion

        #region Constructors
        public AddEventModal(WpfScheduler.Scheduler scheduler, DateTime eventStart, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _scheduler = scheduler;
            _eventStart = eventStart;
            _userLoggedIn = userLoggedIn;

            this.Title = "Agendar cita (" + eventStart.ToString("D") + ")";
            lblEventStartTime.ToolTip = lblEventStartTime.Text = eventStart.ToString("HH:mm") + " hrs";

            FillPatients();
            FillTreatments();
		}
        #endregion

        #region Window event handlers
        private void cbPatientName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedPatient = (cbPatientName.SelectedValue as Controllers.ComboBoxItem).Value as Model.Patient;

            FillPatientFields(_selectedPatient);
        }

        private void cbTratmentName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedTreatment = (cbTratmentName.SelectedValue as Controllers.ComboBoxItem).Value as Model.Treatment;

            FillTreatmentFields(_selectedTreatment);
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

            List<Model.Event> skippedEvents = _selectedPatient.Events
                                                .Where(ev => ev.IsCompleted && ev.PatientSkips)
                                                .OrderBy(ev => ev.StartEvent)
                                                .ToList();

            
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
                                                "Advertencia",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Warning);
                return;
            }

            //Health Insurance
            if (_selectedPatient.HasHealthInsurance)
            {
                if (_selectedPatient.IsDentegra)
                {
                    List<Model.DentegraAuthorization> elegibilities = _selectedPatient.DentegraAuthorizations
                                                                       .OrderByDescending(a => a.AuthorizationDate)
                                                                       .Take(1)
                                                                       .ToList();

                    if (elegibilities.Count == 0 || IsValidElegibility(elegibilities[0]) == false)
                    {
                        if (MessageBox.Show("El paciente seleccionado tiene seguro médico pero no cuenta con un número de " +
                                        "elegibilidad vigente.\n¿Desea agendar la cita?",
                                        "Advertencia",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Warning
                                    ) == MessageBoxResult.No || MainWindow.IsValidAdminPassword(_userLoggedIn) == false)
                        {
                            return;
                        }
                    }
                }
                else if (_selectedPatient.IsDiverse == false)
	            {
                    List<Model.Authorization> authorizations = _selectedPatient.Authorizations
                                                                       .OrderByDescending(a => a.AuthorizationDate)
                                                                       .Take(1)
                                                                       .ToList();

                    if (authorizations.Count == 0 || IsValidAuthorization(authorizations[0]) == false)
                    {
                        if (MessageBox.Show("El paciente seleccionado tiene seguro médico pero no cuenta con un número de " +
                                        "autorización vigente.\n¿Desea agendar la cita?",
                                        "Advertencia",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Warning
                                    ) == MessageBoxResult.No || MainWindow.IsValidAdminPassword(_userLoggedIn) == false)
                        {
                            return;
                        }
                    }
	            }
            }

            //Statements
            Model.Statement currentStatement = _selectedPatient.Statements
                                                    .Where(s => s.IsPaid == false)
                                                    .FirstOrDefault();

            if (currentStatement != null && currentStatement.ExpirationDate < DateTime.Now.Date)
            {
                MessageBox.Show("Este paciente posee un estado de cuenta que ha expirado (Estado de cuenta número: " + currentStatement.StatementId + ")" +
                                "\nEs necesario que el paciente liquide el estado de cuenta para poder agendarle una cita.",
                                "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            //Check for restricted hours if user has recurrent canceled events
            List<Model.Event> canceledEventsInARow = AgendaWindow.GetPatientCanceledEventsInARow(_selectedPatient.PatientId, 3);
            if (canceledEventsInARow != null && IsRestrictedHour(_eventStart))
            {
                string canceledEventsMessage = string.Empty;
                for (int i = 0; i < canceledEventsInARow.Count; i++)
                {
                    canceledEventsMessage += string.Format(
                                                "\nCita cancelada #{0}:" +
                                                "\n -Tratamiento: {1}" +
                                                "\n -Día de la cita: {2}" +
                                                "\n -Hora de inicio de la cita: {3}" +
                                                "\n -Hora de fin de la cita: {4}",
                                                i + 1,
                                                canceledEventsInARow[i].Treatment.Name,
                                                canceledEventsInARow[i].StartEvent.ToString("D"),
                                                canceledEventsInARow[i].StartEvent.ToString("HH:mm") + " hrs",
                                                canceledEventsInARow[i].EndEvent.ToString("HH:mm") + " hrs" + (i == canceledEventsInARow.Count - 1 ? "" : "\n"));
                }

                MessageBox.Show(string.Format("El paciente seleccionado no puede agendar cita en un horario fuera de " + 
                                                "13 hrs. a 15 hrs. y 19 hrs. en delante, dado que se le ha penalizado " +
                                                "por 3 citas canceladas consecutivamente.\n\nUltimas 3 citas canceladas:\n{0}",
                                                    canceledEventsMessage
                                                ),
                                                "Advertencia",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Warning);

                return;
            }

            Model.Event eventToAdd = new Model.Event();
            if (IsValidEvent(eventToAdd))
            {
                if (IsValidTreatmentInstrumentRelation(eventToAdd) == false)
                {
                    return;
                }

                if (BusinessController.Instance.Add<Model.Event>(eventToAdd))
                {
                    WpfScheduler.Event eventToAddScheduler = new WpfScheduler.Event() { EventInfo = eventToAdd };
                    _scheduler.AddEvent(eventToAddScheduler);

                    //Change status
                    bool eventStatusChangeRegistered = Utils.AddEventStatusChanges(null, eventToAddScheduler.EventStatus.ToString(), eventToAdd.EventId, _userLoggedIn.UserId);
                    if (eventStatusChangeRegistered == false)
                    {
                        MessageBox.Show("No se pudo guardar el cambio registrado en el estado de la cita", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    //Discount UsesLeft to the selected instrument
                    List<Model.Instrument> missingInstruments = new List<Model.Instrument>();
                    if (eventToAdd.Instruments.Count > 0)
                    {
                        bool discountError = false;
                        foreach (var instrument in eventToAdd.Instruments)
                        {
                            if (instrument.UsesLeft > 0)
                            {
                                instrument.UsesLeft--;
                                discountError &= !BusinessController.Instance.Update<Model.Instrument>(instrument);
                            }
                            else
                            {
                                missingInstruments.Add(instrument);
                            }
                        }
                        
                        if (discountError)
                        {
                            MessageBox.Show("No se pudo descontar la cantidad de usos a algún instrumento seleccionado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    if (missingInstruments.Count > 0)
                    {
                        MessageBox.Show("La cita se ha agendado, pero debe tomar en cuenta que faltan los siguientes instrumentos:\n-" + string.Join("\n-", missingInstruments.Select(i => i.Name)), "Información", MessageBoxButton.OK, MessageBoxImage.Information);   
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("No pudo ser agendada la cita", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region Window's logic
        private bool IsRestrictedHour(DateTime eventStart)
        {
            DateTime range1 = eventStart.Date + new TimeSpan(13, 0, 0); //1pm
            DateTime range2 = eventStart.Date + new TimeSpan(15, 0, 0); //3pm
            DateTime range3 = eventStart.Date + new TimeSpan(19, 0, 0); //7pm

            return !((eventStart >= range1 && eventStart < range2) || eventStart >= range3);
        }

        private bool IsValidTreatmentInstrumentRelation(Model.Event eventToAdd)
        {
            List<Model.Instrument> instrumentsWithTreatment = BusinessController.Instance.FindBy<Model.Instrument>(i => i.Drawer.IsDeleted == false && i.IsDeleted == false)
                                                                                            .Where(i => i.Treatments.Any(j => j.TreatmentId == eventToAdd.TreatmentId))
                                                                                            .ToList();

            if (instrumentsWithTreatment.Count == 0)
            {
                return true;
            }
            else
            {
                new InstrumentTreatmentRelationModal(instrumentsWithTreatment, eventToAdd).ShowDialog();

                return eventToAdd.Instruments.Count > 0;
            }
        }

        private bool IsValidAuthorization(Model.Authorization authorization)
        {
            if (authorization.AuthorizationNumber != null)
            {
                DateTime today = DateTime.Now.Date;
                DateTime authorizationDate = authorization.AuthorizationDate.AddMonths(1);

                return authorizationDate >= today;
            }

            return true;
        }

        private bool IsValidElegibility(Model.DentegraAuthorization elegibility)
        {
            DateTime today = DateTime.Now.Date;
            DateTime elegibilityDate = elegibility.AuthorizationDate.AddDays(3.0);

            return elegibilityDate >= today;
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
                lblExpNo.ToolTip = lblExpNo.Text = selectedPatient.AssignedId.ToString();
                lblCellphone.ToolTip = lblCellphone.Text = selectedPatient.CellPhone;
                lblHomePhone.ToolTip = lblHomePhone.Text = selectedPatient.HomePhone;
                lblEmail.ToolTip = lblEmail.Text = selectedPatient.Email;

                lblUpdateClinicHistoryMessage.Visibility = MainWindow.HasPatientToUpdateClinicHistory(selectedPatient)
                                                                ? System.Windows.Visibility.Visible
                                                                : System.Windows.Visibility.Hidden;
			}
		}

        private void FillTreatmentFields(Model.Treatment selectedTreatment)
        {
            lblDuration.ToolTip = lblDuration.Text = selectedTreatment == null ? string.Empty : selectedTreatment.Duration.ToString() + " minutos";

            DateTime secondExtraHourMaxRange = new DateTime(_eventStart.Year, _eventStart.Month, _eventStart.Day, _scheduler.MaxHour, 0, 0);
            DateTime eventEnd = _eventStart.AddMinutes(_selectedTreatment.Duration);
            eventEnd = eventEnd > secondExtraHourMaxRange ? secondExtraHourMaxRange : eventEnd;

            lblEventEndTime.ToolTip = lblEventEndTime.Text = eventEnd.ToString("HH:mm") + " hrs";

        }

        /// <summary>
        /// Check for overlapping events
        /// </summary>
        /// <param name="eventToAdd"></param>
        /// <returns></returns>
        private bool IsValidEvent(Model.Event eventToAdd)
        {
            eventToAdd.StartEvent = _eventStart;
            eventToAdd.EndEvent = _eventStart.AddMinutes(_selectedTreatment.Duration);
            eventToAdd.IsException = false;
            eventToAdd.IsCanceled = false;
            eventToAdd.IsCompleted = false;
            eventToAdd.PatientSkips = false;
            eventToAdd.IsConfirmed = false;
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
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
                {
                    if (MainWindow.IsValidAdminPassword(_userLoggedIn))
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
                if (MainWindow.IsValidAdminPassword(_userLoggedIn))
                {
                    eventToAdd.IsException = true;

                    DateTime secondExtraHourMaxRange = new DateTime(eventToAdd.StartEvent.Year, eventToAdd.StartEvent.Month, eventToAdd.StartEvent.Day, _scheduler.MaxHour, 0, 0);
                    eventToAdd.EndEvent = eventToAdd.EndEvent > secondExtraHourMaxRange ? secondExtraHourMaxRange : eventToAdd.EndEvent;

                    return true;   
                }

                return false;
            }

            return true;
        }

        private bool IsTimeRangeException(DateTime eventToAddStart, DateTime eventToAddEnd)
        {
            DateTime firstExtraHourMinRange = new DateTime(eventToAddStart.Year, eventToAddStart.Month, eventToAddStart.Day, _scheduler.MinHour, 0, 0);
            DateTime firstExtraHourMaxRange = new DateTime(eventToAddStart.Year, eventToAddStart.Month, eventToAddStart.Day, _scheduler.FinishFirstExtraHour, 0, 0);
            DateTime secondExtraHourMinRange = new DateTime(eventToAddStart.Year, eventToAddStart.Month, eventToAddStart.Day, _scheduler.StartSecondExtraHour, 0, 0);
            DateTime secondExtraHourMaxRange = new DateTime(eventToAddStart.Year, eventToAddStart.Month, eventToAddStart.Day, _scheduler.MaxHour, 0, 0);


            return Utils.IsOverlappedTime(eventToAddStart, eventToAddEnd, firstExtraHourMinRange, firstExtraHourMaxRange)
                    || Utils.IsOverlappedTime(eventToAddStart, eventToAddEnd, secondExtraHourMinRange, secondExtraHourMaxRange);
        }

        private void FillTreatments()
        {
            List<Model.Treatment> treatments = BusinessController.Instance.FindBy<Model.Treatment>(t => t.IsDeleted == false)
                                                .OrderBy(t => t.Name)
                                                .ThenBy(t => t.Duration)
                                                .ToList();

            foreach (Model.Treatment treatment in treatments)
            {
                cbTratmentName.Items.Add(new Controllers.ComboBoxItem() { Text = treatment.Name, Value = treatment });
            }
        }

        private void FillPatients()
        {
            List<Model.Patient> patients = BusinessController.Instance.FindBy<Model.Patient>(p => p.IsDeleted == false)
                                            .OrderBy(p => p.FirstName)
                                            .ThenBy(p => p.LastName)
                                            .ThenBy(p => p.AssignedId)
                                            .ToList();

            foreach (Model.Patient patient in patients)
            {
                cbPatientName.Items.Add(new Controllers.ComboBoxItem() { Text = string.Format("{1} {2} (Exp. No. {0})", patient.AssignedId, patient.FirstName, patient.LastName), Value = patient });
            }
        }
        #endregion
    }
}