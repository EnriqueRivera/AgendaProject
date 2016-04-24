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
	/// Interaction logic for AddEditAuthorizationsModal.xaml
	/// </summary>
	public partial class AddEditAuthorizationsModal : Window
	{
        #region Instance variables
        private Model.Patient _selectedPatient;
        private Model.Authorization _selectedAuthorization;
        private bool _isUpdateAuthorization;
        #endregion

        #region Constructors
        public AddEditAuthorizationsModal(Model.Patient selectedPatient, Model.Authorization selectedAuthorization)
		{
			this.InitializeComponent();

            _selectedPatient = selectedPatient;
            _selectedAuthorization = selectedAuthorization;
            _isUpdateAuthorization = selectedAuthorization != null;

            lblExpNumber.ToolTip = lblExpNumber.Content = selectedPatient.AssignedId;
            lblPatientName.ToolTip = lblPatientName.Content = selectedPatient.FirstName + " " + selectedPatient.LastName;

            if (_isUpdateAuthorization)
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

		private void btnAddUpdateAuthorization_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string authorizationNumber = txtAuthorization.Text.Trim();
            string preAuthorizationNumber = txtPreAuthorization.Text.Trim();

            if (AreValidFields(authorizationNumber, preAuthorizationNumber) == false)
            {
                return;
            }

            if (_isUpdateAuthorization)
            {
                _selectedAuthorization.AuthorizationDate = DateTime.Now;
                _selectedAuthorization.AuthorizationNumber = chkAuthorization.IsChecked.Value ? authorizationNumber : null;
                _selectedAuthorization.PreAuthorizationNumber = chkPreAuthorization.IsChecked.Value ? preAuthorizationNumber : null;

                UpdateAuthorization(_selectedAuthorization);
            }
            else
            {
                Model.Authorization authorizationToAdd = new Model.Authorization()
                {
                    AuthorizationDate = DateTime.Now,
                    AuthorizationNumber = chkAuthorization.IsChecked.Value ? authorizationNumber : null,
                    PreAuthorizationNumber = chkPreAuthorization.IsChecked.Value ? preAuthorizationNumber : null,
                    PatientId = _selectedPatient.PatientId
                };

                AddAuthorization(authorizationToAdd);
            }
		}

        private void chkPreAuthorization_CheckedUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            txtPreAuthorization.IsEnabled = chkPreAuthorization.IsChecked.Value;
        }

        private void chkAuthorization_CheckedUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            txtAuthorization.IsEnabled = chkAuthorization.IsChecked.Value;
        }
        #endregion

        #region Window's logic
        private void AddAuthorization(Model.Authorization authorizationToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Authorization>(authorizationToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar la autorización", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateAuthorization(Model.Authorization authorizationToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Authorization>(authorizationToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la autorización", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AreValidFields(string authorizationNumber, string preAuthorizationNumber)
        {
            if (chkAuthorization.IsChecked.Value == false && chkPreAuthorization.IsChecked.Value == false)
            {
                MessageBox.Show("Debe ingresar al menos un número de autorización", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (chkPreAuthorization.IsChecked.Value && string.IsNullOrEmpty(preAuthorizationNumber))
            {
                MessageBox.Show("Ingrese un número de Pre Autorización", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (chkAuthorization.IsChecked.Value && string.IsNullOrEmpty(authorizationNumber))
            {
                MessageBox.Show("Ingrese un número de Autorización", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private void PrepareWindowForUpdates()
        {
            chkAuthorization.IsChecked = _selectedAuthorization.AuthorizationNumber != null;
            chkPreAuthorization.IsChecked = _selectedAuthorization.PreAuthorizationNumber != null;
            txtAuthorization.Text = _selectedAuthorization.AuthorizationNumber;
            txtPreAuthorization.Text = _selectedAuthorization.PreAuthorizationNumber;

            btnAddUpdateAuthorization.Content = "Actualizar";
            this.Title = "Actualizar autorización";
        }
        #endregion
    }
}