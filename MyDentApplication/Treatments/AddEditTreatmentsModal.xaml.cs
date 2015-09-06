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
	/// Interaction logic for AddEditTreatmentsModal.xaml
	/// </summary>
	public partial class AddEditTreatmentsModal : Window
	{
        #region Instance variables
        private Model.Treatment _treatmentToUpdate;
        private bool _isUpdateTreatmentInfo;
        private const int _maxMinutesDuration = 180;
        private const int _treatmentIntervalDuration = 15;
        #endregion

        #region Constructors
        public AddEditTreatmentsModal(Model.Treatment treatmentToUpdate)
		{
			this.InitializeComponent();

            _treatmentToUpdate = treatmentToUpdate;
            _isUpdateTreatmentInfo = treatmentToUpdate != null;
            FillComboBoxDuration();

            if (_isUpdateTreatmentInfo)
            {
                PrepareWindowForUpdates();
            }
        }
        #endregion

        #region Window event handlers
        private void btnAddUpdateTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string treatmentName = txtTreatmentName.Text.Trim();
            int? recurrentDays;

            if (AreValidFields(treatmentName, out recurrentDays) == false)
            {
                return;
            }

            int duration = (int)(cbTreatmentDuration.SelectedValue as Controllers.ComboBoxItem).Value;

            if (_isUpdateTreatmentInfo)
            {
                _treatmentToUpdate.Name = treatmentName;
                _treatmentToUpdate.Duration = duration;
                _treatmentToUpdate.Recurrent = recurrentDays;

                UpdateTreatment(_treatmentToUpdate);
            }
            else
            {
                Model.Treatment treatmentToAdd = new Model.Treatment()
                {
                    Name = treatmentName,
                    Duration = duration,
                    IsDeleted = false,
                    Recurrent = recurrentDays
                };

                AddTreatment(treatmentToAdd);
            }
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void chkIsRecurrent_CheckedUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (chkIsRecurrent.IsChecked.Value)
            {
                txtRecurrentDays.IsEnabled = true;
                txtRecurrentDays.ToolTip = txtRecurrentDays.Text = _isUpdateTreatmentInfo && _treatmentToUpdate.Recurrent != null
                                                                    ? _treatmentToUpdate.Recurrent.ToString()
                                                                    : string.Empty;
            }
            else
            {
                txtRecurrentDays.IsEnabled = false;
                txtRecurrentDays.ToolTip = txtRecurrentDays.Text = string.Empty;
            }
        }
        #endregion

        #region Window's logic
        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del tratamiento";
            btnAddUpdateTreatment.Content = "Actualizar";
            txtTreatmentName.ToolTip = txtTreatmentName.Text = _treatmentToUpdate.Name;
            cbTreatmentDuration.SelectedIndex = (_treatmentToUpdate.Duration / _treatmentIntervalDuration) - 1;
            chkIsRecurrent.IsChecked = txtRecurrentDays.IsEnabled = _treatmentToUpdate.Recurrent != null;
            txtRecurrentDays.ToolTip = txtRecurrentDays.Text = _treatmentToUpdate.Recurrent == null ? string.Empty : _treatmentToUpdate.Recurrent.Value.ToString();
        }

        private void FillComboBoxDuration()
        {
            for (int i = _treatmentIntervalDuration; i <= _maxMinutesDuration; i = i + _treatmentIntervalDuration)
            {
                int hours = i / 60;
                int minutes = (i % 60) == 0 ? 0 : (i - (hours * 60));

                string comboBoxTextDuration = string.Format("{0} ({1}:{2})", i, hours.ToString("00"), minutes.ToString("00"));
                cbTreatmentDuration.Items.Add(new Controllers.ComboBoxItem() { Text = comboBoxTextDuration, Value = i });
            }
        }

        private bool AreValidFields(string treatmentName, out int? recurrentDays)
        {
            int auxRecurrentDays;
            int.TryParse(txtRecurrentDays.Text.Trim(), out auxRecurrentDays);
            recurrentDays = auxRecurrentDays;

            if (string.IsNullOrEmpty(treatmentName))
            {
                MessageBox.Show("Ingrese el nombre del tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (cbTreatmentDuration.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione la duración del tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (chkIsRecurrent.IsChecked.Value)
            {
                if (recurrentDays <= 0)
	            {
		            MessageBox.Show("Indique un número válido para los días de recurrencia", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
	            }
            }
            else
            {
                recurrentDays = null;
            }

            return true;
        }

        private void AddTreatment(Model.Treatment treatmentToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Treatment>(treatmentToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el tratamiento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTreatment(Model.Treatment treatmentToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Treatment>(treatmentToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el tratamiento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}