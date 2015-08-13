using Controllers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for FinishedEventsControl.xaml
	/// </summary>
	public partial class FinishedEventsControl : UserControl
	{
        private Model.Event _eventToRender;
        private WpfScheduler.Event _schedulerEvent;
        private Model.User _userLoggedIn;

        public FinishedEventsControl(Model.Event eventToRender, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            _eventToRender = eventToRender;
            _schedulerEvent = new WpfScheduler.Event() { EventInfo = _eventToRender };

            LoadEventInfo();
		}

        private void LoadEventInfo()
        {
            if (_eventToRender != null)
            {

                //Event Info
                lblEventId.ToolTip = lblEventId.Text = _eventToRender.EventId.ToString();
                lblEventDate.ToolTip = lblEventDate.Text = _eventToRender.StartEvent.ToString("D");
                lblEventStartTime.ToolTip = lblEventStartTime.Text = _eventToRender.StartEvent.ToString("HH:mm") + " hrs";
                lblEventEndTime.ToolTip = lblEventEndTime.Text = _eventToRender.EndEvent.ToString("HH:mm") + " hrs";
                lblEventStatus.ToolTip = lblEventStatus.Text = _schedulerEvent.EventStatusString;
                lblEventCapturer.ToolTip = lblEventCapturer.Text = _eventToRender.User.FirstName + " " + _eventToRender.User.LastName;

                //Pacient Info
                lblExpNo.ToolTip = lblExpNo.Text = _eventToRender.Patient.PatientId.ToString();
                lblPacientName.ToolTip = lblPacientName.Text = _eventToRender.Patient.FirstName + " " + _eventToRender.Patient.LastName;
                lblCellPhone.ToolTip = lblCellPhone.Text = _eventToRender.Patient.CellPhone;
                lblHomePhone.ToolTip = lblHomePhone.Text = _eventToRender.Patient.HomePhone;
                lblEmail.ToolTip = lblEmail.Text = _eventToRender.Patient.Email;

                gridBackground.Background = _schedulerEvent.Color;
            }
        }

        private void changeStatusEvent_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EventStatus statusToChange = (EventStatus)Enum.Parse(typeof(EventStatus), (sender as Button).Tag.ToString(), true);

            bool? eventModified = AgendaWindow.ModifyEventStatus(_schedulerEvent, statusToChange, _userLoggedIn.UserId);
            if (eventModified == null || eventModified == true)
            {
                gridBackground.Background = _schedulerEvent.Color;
                DisableButtons();
            }
        }

        private void DisableButtons()
        {
            btnPatientNotSkips.IsEnabled = false;
            btnPatientSkips.IsEnabled = false;
            btnPatientCancel.IsEnabled = false;
        }
	}
}