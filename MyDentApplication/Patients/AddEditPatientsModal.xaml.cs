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
using System.Linq;

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
        private bool _isDiverse;
        #endregion

        #region Constructors
        public AddEditPatientsModal(Model.Patient patientToUpdate, bool hasHealthInsurance, bool isDiverse, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            _hasHealthInsurance = hasHealthInsurance;
            _isDiverse = isDiverse;
            _patientToUpdate = patientToUpdate;
            _isUpdatePatientInfo = patientToUpdate != null;

            if (_isUpdatePatientInfo)
            {
                PrepareWindowForUpdates();
            }
            else
            {
                txtExpNumber.Text = GetNextExpNumber().ToString();
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
            int expNumber;
            string expNumberText = txtExpNumber.Text.Trim();
            string patientFirstName = txtPatientFirstName.Text.Trim();
            string patientLastName = txtPatientLastName.Text.Trim();
            string homePhone = txtHomePhone.Text.Trim();
            string cellPhone = txtCellPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (AreValidFields(patientFirstName, patientLastName, homePhone, cellPhone, email, expNumberText, out expNumber) == false)
            {
                return;
            }

            if (_isUpdatePatientInfo)
            {
                _patientToUpdate.AssignedId = expNumber;
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
                    AssignedId = expNumber,
                    FirstName = patientFirstName,
                    LastName = patientLastName,
                    HomePhone = homePhone,
                    CellPhone = cellPhone,
                    Email = txtEmail.Text.Trim(),
                    HasHealthInsurance = _hasHealthInsurance,
                    IsDiverse = _isDiverse,
                    CaptureDate = DateTime.Now,
                    IsDeleted = false,
                    DataCapturerId = _userLoggedIn.UserId
                };

                AddUser(patientToAdd);
            }
        }
        #endregion

        #region Window's logic
        private int GetNextExpNumber()
        {
            int x = 0;
            List<Model.Patient> patients = Controllers.BusinessController.Instance.FindBy<Model.Patient>(p => p.IsDeleted == false)
                                                    .OrderBy(p => p.AssignedId)
                                                    .ToList();

            for (int i = 0; i < patients.Count; i++)
            {
                if (i == 0)
                {
                    if (patients[i].AssignedId > 1)
                        return x;
                    else
                        x = patients[i].AssignedId;
                }
                else if (patients[i].AssignedId > (x + 1))
                    break;
                else
                    x = patients[i].AssignedId;
            }

            return x + 1;
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del paciente";
            btnAddUpdatePatient.Content = "Actualizar";

            txtExpNumber.Text = _patientToUpdate.AssignedId.ToString();
            txtPatientFirstName.Text = _patientToUpdate.FirstName;
            txtPatientLastName.Text = _patientToUpdate.LastName;
            txtHomePhone.Text = _patientToUpdate.HomePhone;
            txtCellPhone.Text = _patientToUpdate.CellPhone;
            txtEmail.Text = _patientToUpdate.Email;
        }

        private bool AreValidFields(string patientFirstName, string patientLastName, string homePhone, string cellPhone, string email, string expNumberText, out int expNumber)
        {
            if (int.TryParse(expNumberText, out expNumber) == false || expNumber < 1)
            {
                MessageBox.Show("No. de expediente inválido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

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

            if ((_isUpdatePatientInfo && _patientToUpdate.AssignedId != expNumber && PatientExpNumberExists(expNumber))
                || (_isUpdatePatientInfo == false && PatientExpNumberExists(expNumber)))
            {
                MessageBox.Show("No se puede " + (_isUpdatePatientInfo ? "actualizar" : "agregar") + " el paciente porque ya existe otro paciente con el mismo No. de expediente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private bool PatientExpNumberExists(int assignedId)
        {
            return Controllers.BusinessController.Instance.FindBy<Model.Patient>(p => p.AssignedId == assignedId).Count() != 0;
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