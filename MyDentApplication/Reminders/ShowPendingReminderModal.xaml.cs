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

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ShowPendingReminderModal.xaml
	/// </summary>
	public partial class ShowPendingReminderModal : Window
	{
        private Model.Reminder _reminderToShow;
        private Model.User _userLoggedIn;

		public ShowPendingReminderModal(Model.Reminder reminderToShow, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _reminderToShow = reminderToShow;
            _userLoggedIn = userLoggedIn;
            UpdateReminderFields();
		}

        private void UpdateReminderFields()
        {
            lblReminderTime.ToolTip = lblReminderTime.Content = _reminderToShow.AppearDate.ToLongDateString() + " a las " + _reminderToShow.AppearDate.ToString("HH:mm") + " hrs.";
            txtReminderMessage.ToolTip = txtReminderMessage.Text = _reminderToShow.Message;
            lblRequireAdmin.Visibility = _reminderToShow.RequireAdmin ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

		private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}

        private bool MarkReminderAsSeen()
        {
            int seenByUserId;

            if (_reminderToShow.RequireAdmin)
            {
                if (MainWindow.IsValidAdminPassword(_userLoggedIn, out seenByUserId) == false)
                {
                    return false;
                }
            }
            else
            {
                seenByUserId = _userLoggedIn.UserId;
            }

            _reminderToShow.Seen = true;
            _reminderToShow.SeenBy = seenByUserId;

            if (Controllers.BusinessController.Instance.Update<Model.Reminder>(_reminderToShow))
            {
                return true;
            }
            else
            {
                MessageBox.Show("No pudo ser marcado como visto el recordatorio.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !MarkReminderAsSeen();
        }
	}
}