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
	/// Interaction logic for ManagePatientsWindow.xaml
	/// </summary>
	public partial class ManagePatientsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Patient> _patientsNoHIViewModel;
        private Controllers.CustomViewModel<Model.Patient> _patientsWithHIViewModel;
        private Controllers.CustomViewModel<Model.Patient> _patientsDiverseViewModel;
        private bool _lastSearchAllPatients = true;
        private Model.User _userLoggedIn;
        #endregion    

        #region Constructors
        public ManagePatientsWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;

            tcPatients.SelectedIndex = 1;
            UpdateGrid();
            tcPatients.SelectedIndex = 2;
            UpdateGrid();
            tcPatients.SelectedIndex = 0;
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
            new AddEditPatientsModal(null, tcPatients.SelectedIndex != 0, tcPatients.SelectedIndex == 2, _userLoggedIn).ShowDialog();
            UpdateGrid();
        }

        private void btnEditPatient_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGrid dgAux = GetCurrentDataGrid();

            Model.Patient selectedPatient = dgAux.SelectedItem == null ? null : dgAux.SelectedItem as Model.Patient;

            if (selectedPatient == null)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditPatientsModal(selectedPatient, false, false, _userLoggedIn).ShowDialog();
                UpdateGrid();
            }
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGrid dgAux = GetCurrentDataGrid();

            Model.Patient selectedPatient = dgAux.SelectedItem == null ? null : dgAux.SelectedItem as Model.Patient;

            if (selectedPatient == null)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el paciente con el Exp. No. {0}?",
                                        selectedPatient.AssignedId),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                selectedPatient.IsDeleted = true;
                selectedPatient.AssignedId = -1;

                if (Controllers.BusinessController.Instance.Update<Model.Patient>(selectedPatient))
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
            DataGrid dgAux = GetCurrentDataGrid();

            Model.Patient selectedPatient = dgAux.SelectedItem == null ? null : dgAux.SelectedItem as Model.Patient;

            if (selectedPatient == null)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new UpdateClinicHistoryWindow(selectedPatient).ShowDialog();
                UpdateGrid();
            }
        }

        private void tcPatients_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (btnAuthorization != null && e.Source is TabControl)
            {
                btnAuthorization.IsEnabled = tcPatients.SelectedIndex == 1;
            }
        }

        private void btnAuthorization_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.Patient selectedPatient = dgPatientsWithHI.SelectedItem == null ? null : dgPatientsWithHI.SelectedItem as Model.Patient;

            if (selectedPatient == null)
            {
                MessageBox.Show("Seleccione un paciente asegurado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new ManageUserAuthorizationsWindow(selectedPatient).ShowDialog();
            }
        }

        private void btnViewStatements_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGrid dgAux = GetCurrentDataGrid();

            Model.Patient selectedPatient = dgAux.SelectedItem == null ? null : dgAux.SelectedItem as Model.Patient;

            if (selectedPatient == null)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                List<Model.Statement> patientStatements = Controllers.BusinessController.Instance.FindBy<Model.Statement>(s => s.PatientId == selectedPatient.PatientId)
                                                                .OrderByDescending(s => s.StatementId)
                                                                .ToList();

                if (patientStatements.Count == 0)
                {
                    MessageBox.Show("El paciente seleccionado no posee estados de cuenta", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    new ManagePatientStatementsWindow(_userLoggedIn, selectedPatient, patientStatements).ShowDialog();
                    UpdateGrid();
                }
            }
        }
        #endregion

        #region Window's logic
        private DataGrid GetCurrentDataGrid()
        {
            switch (tcPatients.SelectedIndex)
            {
                case 0: return dgPatientsNoHI;
                case 1: return dgPatientsWithHI;
                case 2: return dgPatientsDiverse;
                default: return dgPatientsNoHI;
            }
        }

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
                        _patientsNoHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance == false && u.IsDeleted == false && u.FirstName.Contains(searchTerm), "AssignedId", "asc");
                        break;
                    case 1:
                        _patientsNoHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance == false && u.IsDeleted == false && u.LastName.Contains(searchTerm), "AssignedId", "asc");
                        break;
                    case 2:
                        int patientId;
                        int.TryParse(searchTerm, out patientId);

                        _patientsNoHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance == false && u.IsDeleted == false && u.AssignedId == patientId, "AssignedId", "asc");
                        break;
                    default:
                        break;
                }

                dgPatientsNoHI.DataContext = _patientsNoHIViewModel;
            }
            else if (tcPatients.SelectedIndex == 1)
            {
                switch (cbFilter.SelectedIndex)
                {
                    case 0:
                        _patientsWithHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDiverse == false && u.IsDeleted == false && u.FirstName.Contains(searchTerm), "AssignedId", "asc");
                        break;
                    case 1:
                        _patientsWithHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDiverse == false && u.IsDeleted == false && u.LastName.Contains(searchTerm), "AssignedId", "asc");
                        break;
                    case 2:
                        int patientId;
                        int.TryParse(searchTerm, out patientId);

                        _patientsWithHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDiverse == false && u.IsDeleted == false && u.AssignedId == patientId, "AssignedId", "asc");
                        break;
                    default:
                        break;
                }

                dgPatientsWithHI.DataContext = _patientsWithHIViewModel;
            }
            else
            {
                switch (cbFilter.SelectedIndex)
                {
                    case 0:
                        _patientsDiverseViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDiverse && u.IsDeleted == false && u.FirstName.Contains(searchTerm), "AssignedId", "asc");
                        break;
                    case 1:
                        _patientsDiverseViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDiverse && u.IsDeleted == false && u.LastName.Contains(searchTerm), "AssignedId", "asc");
                        break;
                    case 2:
                        int patientId;
                        int.TryParse(searchTerm, out patientId);

                        _patientsDiverseViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDiverse && u.IsDeleted == false && u.AssignedId == patientId, "AssignedId", "asc");
                        break;
                    default:
                        break;
                }

                dgPatientsDiverse.DataContext = _patientsDiverseViewModel;
            }
        }

        private void UpdateGridAllPatients()
        {
            switch (tcPatients.SelectedIndex)
            {
                case 0:
                    _patientsNoHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance == false && u.IsDeleted == false, "AssignedId", "asc");
                    dgPatientsNoHI.DataContext = _patientsNoHIViewModel;
                    break;
                case 1:
                    _patientsWithHIViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDiverse == false && u.IsDeleted == false, "AssignedId", "asc");
                    dgPatientsWithHI.DataContext = _patientsWithHIViewModel;
                    break;
                case 2:
                    _patientsDiverseViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.HasHealthInsurance && u.IsDiverse && u.IsDeleted == false, "AssignedId", "asc");
                    dgPatientsDiverse.DataContext = _patientsDiverseViewModel;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}