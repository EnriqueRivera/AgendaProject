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
	/// Interaction logic for ViewReminderControl.xaml
	/// </summary>
	public partial class ViewReminderControl : UserControl
	{
        private Model.Reminder _reminder;

        public Model.Reminder Reminder
        {
            get { return _reminder; }

            set
            {
                _reminder = value;
                UpdateReminderFields();
            }

        }

		public ViewReminderControl()
		{
			this.InitializeComponent();
		}

        public ViewReminderControl(Model.Reminder reminder)
        {
            this.InitializeComponent();

            Reminder = reminder;
        }

        private void UpdateReminderFields()
        {
            if (_reminder != null)
	        {
                lrReminder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(_reminder.Seen ? "#FFB2EEB2" : "#FFB2C7EE"));
                lblReminderTime.ToolTip = lblReminderTime.Content = _reminder.AppearDate.ToString("HH:mm:ss") + " hrs.";
                lblReminderMessage.ToolTip = _reminder.Message;
                lblReminderMessage.Content = (_reminder.Message.Length > 10) ? _reminder.Message.Substring(0, 10) + "..." : _reminder.Message;
	        }
        }

		private void btnViewReminder_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (_reminder != null)
            {
                new ViewReminderModal(_reminder).ShowDialog();    
            }
		}   
	}
}