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

            if (AreValidFields(treatmentName) == false)
            {
                return;
            }

            int duration = (int)(cbTreatmentDuration.SelectedValue as Controllers.ComboBoxItem).Value;

            if (_isUpdateTreatmentInfo)
            {
                _treatmentToUpdate.Name = treatmentName;
                _treatmentToUpdate.Duration = duration;

                UpdateTreatment(_treatmentToUpdate);
            }
            else
            {
                Model.Treatment treatmentToAdd = new Model.Treatment()
                {
                    Name = treatmentName,
                    Duration = duration,
                    IsDeleted = false
                };

                AddTreatment(treatmentToAdd);
            }
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Window's logic
        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del tratamiento";
            btnAddUpdateTreatment.Content = "Actualizar";
            txtTreatmentName.Text = _treatmentToUpdate.Name;
            cbTreatmentDuration.SelectedIndex = (_treatmentToUpdate.Duration / _treatmentIntervalDuration) - 1;
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

        private bool AreValidFields(string treatmentName)
        {
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