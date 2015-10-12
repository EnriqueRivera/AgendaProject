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
	/// Interaction logic for ManagePatientsWindow.xaml
	/// </summary>
	public partial class ManagePatientsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Patient> _patientsViewModel;
        private bool _lastSearchAllPatients = true;
        private Model.User _userLoggedIn;
        #endregion    

        #region Constructors
        public ManagePatientsWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;

            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnRefreshPatients_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            _lastSearchAllPatients = false;
            UpdateGrid();
		}

		private void btnViewAllPatients_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            _lastSearchAllPatients = true;
            UpdateGrid();
		}

        private void btnAddPatient_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new AddEditPatientsModal(null, _userLoggedIn).ShowDialog();
            UpdateGrid();
        }

        private void btnEditPatient_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.Patient patientSelected = dgPatients.SelectedItem == null ? null : dgPatients.SelectedItem as Model.Patient;

            if (patientSelected == null)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditPatientsModal(patientSelected, _userLoggedIn).ShowDialog();
                UpdateGrid();
            }
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.Patient patientSelected = dgPatients.SelectedItem == null ? null : dgPatients.SelectedItem as Model.Patient;

            if (patientSelected == null)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el paciente con el Exp. No. {0}?",
                                        patientSelected.PatientId),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                patientSelected.IsDeleted = true;

                if (Controllers.BusinessController.Instance.Update<Model.Patient>(patientSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
		
		private void btnUpdateHc_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.Patient patientSelected = dgPatients.SelectedItem == null ? null : dgPatients.SelectedItem as Model.Patient;

            if (patientSelected == null)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new UpdateClinicHistoryWindow(patientSelected).ShowDialog();
                UpdateGrid();
            }
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            if (_lastSearchAllPatients)
            {
                UpdateGridAllPatients();
            }
            else
            {
                UpdateGridFilteredPatients();
            }
        }

        private void UpdateGridFilteredPatients()
        {
            string searchTerm = txtSearchTerm.Text;

            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    _patientsViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.IsDeleted == false && u.FirstName.Contains(searchTerm), "PatientId", "asc");
                    break;
                case 1:
                    _patientsViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.IsDeleted == false && u.LastName.Contains(searchTerm), "PatientId", "asc");
                    break;
                case 2:
                    int patientId;
                    int.TryParse(searchTerm, out patientId);

                    _patientsViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.IsDeleted == false && u.PatientId == patientId, "PatientId", "asc");
                    break;
                default:
                    break;
            }

            this.DataContext = _patientsViewModel;
        }

        private void UpdateGridAllPatients()
        {
            _patientsViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.IsDeleted == false, "PatientId", "asc");
            this.DataContext = _patientsViewModel;
        }
        #endregion
    }
}