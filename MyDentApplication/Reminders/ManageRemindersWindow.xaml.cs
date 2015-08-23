using System;
using System.Collections.Generic;
using System.Data.Objects;
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
	/// Interaction logic for ManageRemindersWindow.xaml
	/// </summary>
	public partial class ManageRemindersWindow : Window
	{
        private Controllers.CustomViewModel<Model.Reminder> _remindersViewModel;

		public ManageRemindersWindow()
		{
			this.InitializeComponent();

            dpReminders.SelectedDate = DateTime.Now;       
		}

        private void UpdateGrid()
        {
            _remindersViewModel = new Controllers.CustomViewModel<Model.Reminder>(u => EntityFunctions.TruncateTime(u.AppearDate) == EntityFunctions.TruncateTime(dpReminders.SelectedDate.Value), "AppearDate", "asc");
            this.DataContext = _remindersViewModel;
        }

		private void btnViewReminder_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Reminder reminderSelected = dgReminders.SelectedItem == null ? null : dgReminders.SelectedItem as Model.Reminder;

            if (reminderSelected == null)
            {
                MessageBox.Show("Seleccione un recordatorio", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new ViewReminderModal(reminderSelected).ShowDialog();
            }
		}

		private void btnAddReminder_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditRemindersModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditReminder_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Reminder reminderSelected = dgReminders.SelectedItem == null ? null : dgReminders.SelectedItem as Model.Reminder;

            if (reminderSelected == null)
            {
                MessageBox.Show("Seleccione un recordatorio", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (reminderSelected.Seen)
            {
                MessageBox.Show("No puede editar un recordatorio que ya ha sido visto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditRemindersModal(reminderSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteReminder_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Reminder reminderSelected = dgReminders.SelectedItem == null ? null : dgReminders.SelectedItem as Model.Reminder;

            if (reminderSelected == null)
            {
                MessageBox.Show("Seleccione un recordatorio", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (reminderSelected.Seen)
            {
                MessageBox.Show("No puede eliminar un recordatorio que ya ha sido visto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                ("¿Está seguro(a) que desea eliminar el recordatorio seleccionado?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                if (Controllers.BusinessController.Instance.Delete<Model.Reminder>(reminderSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el recordatorio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

		private void dpReminders_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
            UpdateGrid();
		}
	}
}