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
	/// Interaction logic for AddEditPatientsModal.xaml
	/// </summary>
	public partial class AddEditPatientsModal : Window
	{
        #region Instance variables
        private Model.Patient _patientToUpdate;
        private bool _isUpdatePatientInfo;
        private Model.User _userLoggedIn;
        private bool _hasHealthInsurance;
        #endregion

        #region Constructors
        public AddEditPatientsModal(Model.Patient patientToUpdate, bool hasHealthInsurance, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            _hasHealthInsurance = hasHealthInsurance;
            _patientToUpdate = patientToUpdate;
            _isUpdatePatientInfo = patientToUpdate != null;

            if (_isUpdatePatientInfo)
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

        private void btnAddUpdatePatient_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string patientFirstName = txtPatientFirstName.Text.Trim();
            string patientLastName = txtPatientLastName.Text.Trim();
            string homePhone = txtHomePhone.Text.Trim();
            string cellPhone = txtCellPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (AreValidFields(patientFirstName, patientLastName, homePhone, cellPhone, email) == false)
            {
                return;
            }

            if (_isUpdatePatientInfo)
            {
                _patientToUpdate.FirstName = patientFirstName;
                _patientToUpdate.LastName = patientLastName;
                _patientToUpdate.HomePhone = homePhone;
                _patientToUpdate.CellPhone = cellPhone;
                _patientToUpdate.Email = email;

                UpdateUser(_patientToUpdate);
            }
            else
            {
                Model.Patient patientToAdd = new Model.Patient()
                {
                    FirstName = patientFirstName,
                    LastName = patientLastName,
                    HomePhone = homePhone,
                    CellPhone = cellPhone,
                    Email = txtEmail.Text.Trim(),
                    HasHealthInsurance = _hasHealthInsurance,
                    CaptureDate = DateTime.Now,
                    IsDeleted = false,
                    DataCapturerId = _userLoggedIn.UserId
                };

                AddUser(patientToAdd);
            }
        }
        #endregion

        #region Window's logic
        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del paciente";
            btnAddUpdatePatient.Content = "Actualizar";

            txtPatientFirstName.Text = _patientToUpdate.FirstName;
            txtPatientLastName.Text = _patientToUpdate.LastName;
            txtHomePhone.Text = _patientToUpdate.HomePhone;
            txtCellPhone.Text = _patientToUpdate.CellPhone;
            txtEmail.Text = _patientToUpdate.Email;
        }

        private bool AreValidFields(string patientFirstName, string patientLastName, string homePhone, string cellPhone, string email)
        {
            if (string.IsNullOrEmpty(patientFirstName))
            {
                MessageBox.Show("Ingrese un nombre", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(patientLastName))
            {
                MessageBox.Show("Ingrese un apellido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(homePhone) && string.IsNullOrEmpty(cellPhone))
            {
                MessageBox.Show("Ingrese al menos un número de teléfono", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(email) == false && Controllers.Utils.IsValidEmail(email) == false)
            {
                MessageBox.Show("Correo inválido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private void AddUser(Model.Patient patientToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Patient>(patientToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateUser(Model.Patient patientToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Patient>(patientToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}