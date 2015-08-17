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
            txtReminderMessage.Text = _reminderToUpdate.Message;
            dtpReminderAppearDate.Value = _reminderToUpdate.AppearDate;
            chkRequireAdmin.IsChecked = _reminderToUpdate.RequireAdmin;
        }

        private void btnAddUpdateReminder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string reminderMessage = txtReminderMessage.Text.Trim();
            DateTime reminderAppearDate = dtpReminderAppearDate.Value.Value;

            if (AreValidFields(reminderMessage, reminderAppearDate) == false)
            {
                return;
            }

            if (_isUpdateReminderInfo)
            {
                _reminderToUpdate.Message = reminderMessage;
                _reminderToUpdate.AppearDate = reminderAppearDate;
                _reminderToUpdate.RequireAdmin = chkRequireAdmin.IsChecked.Value;

                UpdateReminder(_reminderToUpdate);
            }
            else
            {
                Model.Reminder reminderToAdd = new Model.Reminder()
                {
                    Message = reminderMessage,
                    AppearDate = reminderAppearDate,
                    CreatedDate = DateTime.Now,
                    RequireAdmin = chkRequireAdmin.IsChecked.Value,
                    Seen = false,
                    SeenBy = null
                };

                AddReminder(reminderToAdd);
            }
        }

        private void AddReminder(Model.Reminder reminderToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Reminder>(reminderToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el recordatorio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateReminder(Model.Reminder reminderToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Reminder>(reminderToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el recordatorio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AreValidFields(string reminderMessage, DateTime reminderAppearDate)
        {
            if (string.IsNullOrEmpty(reminderMessage))
            {
                MessageBox.Show("Ingrese un mensaje", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (reminderAppearDate < DateTime.Now)
            {
                MessageBox.Show("No puede crear un recordatorio para una fecha y hora pasada", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
	}
}