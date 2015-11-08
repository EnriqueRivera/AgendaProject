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
        private Controllers.CustomViewModel<Model.Patient> _patientsNoHIViewModel;
        private Controllers.CustomViewModel<Model.Patient> _patientsWithHIViewModel;
        private bool _lastSearchAllPatients = true;
        private Model.User _userLoggedIn;
        #endregion    

        #region Constructors
        public ManagePatientsWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;

            UpdateGrid();
            tcPatients.SelectedIndex = 1;
            UpdateGrid();
            tcPatients.SelectedIndex = 0;
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
            new AddEditPatientsModal(null, tcPatients.SelectedIndex != 0, _userLoggedIn).ShowDialog();
            UpdateGrid();
        }

        private void btnEditPatient_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGrid dgAux = tcPatients.SelectedIndex == 0 ? dgPatientsNoHI : dgPatientsWithHI;

            Model.Patient patientSelected = dgAux.SelectedItem == null ? null : dgAux.SelectedItem as Model.Patient;

            if (patientSelected == null)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditPatientsModal(patientSelected, false, _userLoggedIn).ShowDialog();
                UpdateGrid();
            }
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGrid dgAux = tcPatients.SelectedIndex == 0 ? dgPatientsNoHI : dgPatientsWithHI;

            Model.Patient patientSelected = dgAux.SelectedItem == null ? null : dgAux.SelectedItem as Model.Patient;

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
            DataGrid dgAux = tcPatients.SelectedIndex == 0 ? dgPatientsNoHI : dgPatientsWithHI;

            Model.Patient patientSelected = dgAux.SelectedItem == null ? null : dgAux.SelectedItem as Model.Patient;

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

            if (tcPatients.SelectedIndex == 0)
            {
                switch (cbFilter.SelectedIndex)
                {
                    case 0:
                        _patientsNoHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance == false && u.IsDeleted == false && u.FirstName.Contains(searchTerm), "PatientId", "asc");
                        break;
                    case 1:
                        _patientsNoHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance == false && u.IsDeleted == false && u.LastName.Contains(searchTerm), "PatientId", "asc");
                        break;
                    case 2:
                        int patientId;
                        int.TryParse(searchTerm, out patientId);

                        _patientsNoHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance == false && u.IsDeleted == false && u.PatientId == patientId, "PatientId", "asc");
                        break;
                    default:
                        break;
                }

                dgPatientsNoHI.DataContext = _patientsNoHIViewModel;
            }
            else
            {
                switch (cbFilter.SelectedIndex)
                {
                    case 0:
                        _patientsWithHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDeleted == false && u.FirstName.Contains(searchTerm), "PatientId", "asc");
                        break;
                    case 1:
                        _patientsWithHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDeleted == false && u.LastName.Contains(searchTerm), "PatientId", "asc");
                        break;
                    case 2:
                        int patientId;
                        int.TryParse(searchTerm, out patientId);

                        _patientsWithHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDeleted == false && u.PatientId == patientId, "PatientId", "asc");
                        break;
                    default:
                        break;
                }

                dgPatientsWithHI.DataContext = _patientsWithHIViewModel;
            }
        }

        private void UpdateGridAllPatients()
        {
            if (tcPatients.SelectedIndex == 0)
            {
                _patientsNoHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance == false && u.IsDeleted == false, "PatientId", "asc");
                dgPatientsNoHI.DataContext = _patientsNoHIViewModel;
            }
            else
            {
                _patientsWithHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDeleted == false, "PatientId", "asc");
                dgPatientsWithHI.DataContext = _patientsWithHIViewModel;
            }
        }
        #endregion
    }
}