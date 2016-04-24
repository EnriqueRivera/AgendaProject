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
	/// Interaction logic for AddEditDentegraAuthorizationsModal.xaml
	/// </summary>
	public partial class AddEditDentegraAuthorizationsModal : Window
	{
        #region Instance variables
        private Model.Patient _selectedPatient;
        private Model.DentegraAuthorization _selectedElegibility;
        private bool _isUpdateElegibility;
        #endregion

        #region Constructors
        public AddEditDentegraAuthorizationsModal(Model.Patient selectedPatient, Model.DentegraAuthorization selectedElegibility)
		{
			this.InitializeComponent();

            _selectedPatient = selectedPatient;
            _selectedElegibility = selectedElegibility;
            _isUpdateElegibility = selectedElegibility != null;

            lblExpNumber.ToolTip = lblExpNumber.Content = selectedPatient.AssignedId;
            lblPatientName.ToolTip = lblPatientName.Content = selectedPatient.FirstName + " " + selectedPatient.LastName;

            if (_isUpdateElegibility)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}

		private void btnAddUpdateElegibility_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string elegibilityNumber = txtElegibilityNumber.Text.Trim();

            if (AreValidFields(elegibilityNumber) == false)
            {
                return;
            }

            if (_isUpdateElegibility)
            {
                _selectedElegibility.AuthorizationDate = DateTime.Now;
                _selectedElegibility.ElegibilityNumber = elegibilityNumber;

                UpdateElegibility(_selectedElegibility);
            }
            else
            {
                Model.DentegraAuthorization elegibilityToAdd = new Model.DentegraAuthorization()
                {
                    AuthorizationDate = DateTime.Now,
                    ElegibilityNumber = elegibilityNumber,
                    PatientId = _selectedPatient.PatientId
                };

                AddElegibility(elegibilityToAdd);
            }
        }
        #endregion

        #region Window's logic
        private void AddElegibility(Model.DentegraAuthorization elegibilityToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.DentegraAuthorization>(elegibilityToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar la elegibilidad", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateElegibility(Model.DentegraAuthorization elegibilityToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.DentegraAuthorization>(elegibilityToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la elegibilidad", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AreValidFields(string elegibilityNumber)
        {
            if (string.IsNullOrEmpty(elegibilityNumber))
            {
                MessageBox.Show("Ingrese un número de elegibilidad", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private void PrepareWindowForUpdates()
        {
            txtElegibilityNumber.Text = _selectedElegibility.ElegibilityNumber;

            btnAddUpdateElegibility.Content = "Actualizar";
            this.Title = "Actualizar elegibilidad";
        }
        #endregion
    }
}