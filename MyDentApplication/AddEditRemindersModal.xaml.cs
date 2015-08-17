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
	/// Interaction logic for AddEditRemindersModal.xaml
	/// </summary>
	public partial class AddEditRemindersModal : Window
	{
        private Model.Reminder _reminderToUpdate;
        private bool _isUpdateReminderInfo;

		public AddEditRemindersModal(Model.Reminder reminderToUpdate)
		{
			this.InitializeComponent();

            _reminderToUpdate = reminderToUpdate;

            _isUpdateReminderInfo = reminderToUpdate != null;
            dtpReminderAppearDate.Value = DateTime.Now;

            if (_isUpdateReminderInfo)
            {
                PrepareWindowForUpdates();
            }
		}

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del recordatorio";
            btnAddUpdateReminder.Content = "Actualizar";
            txtReminderSubject.Text = _reminderToUpdate.Subject;
            txtReminderMessage.Text = _reminderToUpdate.Message;
            dtpReminderAppearDate.Value = _reminderToUpdate.AppearDate;
            chkRequireAdmin.IsChecked = _reminderToUpdate.RequireAdmin;
        }

        private void btnAddUpdateReminder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show(dtpReminderAppearDate.Value.ToString());
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
	}
}