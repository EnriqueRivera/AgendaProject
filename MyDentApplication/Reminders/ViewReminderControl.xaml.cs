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
        #region Instance variables
        private Model.Reminder _reminder;
        private GradientBrush _reminderSeenColor;
        private GradientBrush _reminderPendingColor;
        #endregion

        #region Getters and setters
        public Model.Reminder Reminder
        {
            get { return _reminder; }

            set
            {
                _reminder = value;
                UpdateReminderFields();
            }

        }
        #endregion

        #region Constructors
        public ViewReminderControl()
		{
			this.InitializeComponent();

            _reminderSeenColor = (GradientBrush)rcReminderSeenColor.Fill;
            _reminderPendingColor = (GradientBrush)rcPendingReminderColor.Fill;
		}
        #endregion

        #region Window event handlers
        private void btnViewReminder_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (_reminder != null)
            {
                new ViewReminderModal(_reminder).ShowDialog();    
            }
		}
        #endregion

        #region Window's logic
        private void UpdateReminderFields()
        {
            if (_reminder != null)
            {
                lrReminder.Background = _reminder.Seen ? _reminderSeenColor : _reminderPendingColor;
                lblReminderTime.ToolTip = lblReminderTime.Content = _reminder.AppearDate.ToString("HH:mm") + " hrs.";
                lblReminderMessage.ToolTip = _reminder.Message;
                lblReminderMessage.Content = (_reminder.Message.Length > 10) ? _reminder.Message.Substring(0, 10) + "..." : _reminder.Message;
            }
        }
        #endregion
    }
}