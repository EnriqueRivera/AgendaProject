using Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for EventStatusChangesWindow.xaml
	/// </summary>
	public partial class EventStatusChangesWindow : Window
	{
        private Controllers.CustomViewModel<Model.EventStatusChanx> _eventStatusChangesViewModel;
        private WpfScheduler.Event _eventToCheck;

		public EventStatusChangesWindow(WpfScheduler.Event eventToCheck)
		{
			this.InitializeComponent();

            _eventToCheck = eventToCheck;
            LoadEventInfo();
            UpdateGrid();
		}

        private void LoadEventInfo()
        {
            lblEventId.ToolTip = lblEventId.Text =_eventToCheck.EventInfo.EventId.ToString();
            lblEventStartTime.ToolTip = lblEventStartTime.Text = _eventToCheck.EventInfo.StartEvent.ToString("D") + _eventToCheck.EventInfo.StartEvent.ToString(", HH:mm") + " hrs";
            lblEventEndTime.ToolTip = lblEventEndTime.Text = _eventToCheck.EventInfo.EndEvent.ToString("D") + _eventToCheck.EventInfo.EndEvent.ToString(", HH:mm") + " hrs";
            lblEventStatus.ToolTip = lblEventStatus.Text = _eventToCheck.EventStatusString;
            lblEventCapturer.ToolTip = lblEventCapturer.Text = _eventToCheck.EventInfo.User.FirstName + " " + _eventToCheck.EventInfo.User.LastName;
        }

        private void UpdateGrid()
        {
            _eventStatusChangesViewModel = new Controllers.CustomViewModel<Model.EventStatusChanx>(ec => ec.EventId == _eventToCheck.EventInfo.EventId, "ChangeDate", "asc");
            this.DataContext = _eventStatusChangesViewModel;
        }
	}

    public class EventSatusAndDateValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1 && values[0] != null)
            {
                if (values[0] is string)
                {
                    return Utils.EventStatusString((EventStatus)Enum.Parse(typeof(EventStatus), (values[0] as string), true));    
                }

                if (values[0] is DateTime)
                {
                    DateTime dateToShow = (DateTime)values[0];
                    return dateToShow.ToString("D") + dateToShow.ToString(", HH:mm:ss") + " hrs";
                }                
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}