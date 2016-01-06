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
	/// Interaction logic for FindPatientEmailModal.xaml
	/// </summary>
	public partial class FindPatientEmailModal : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Patient> _patientsViewModel;
        private List<Model.Patient> _selectedPatients;
        #endregion        

        #region Constructors
        public FindPatientEmailModal(List<Model.Patient> selectedPatients)
		{
			this.InitializeComponent();

            _selectedPatients = selectedPatients;
            UpdateGridAllPatients();
		}
        #endregion

        #region Window event handlers
        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectedPatients = dgPatients.SelectedItems;
            foreach (var item in selectedPatients)
            {
                Model.Patient patient = item as Model.Patient;
                _selectedPatients.Add(patient);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnViewAllPatients_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateGridAllPatients();
        }

        private void btnRefreshPatients_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateGridFilteredPatients();
        }
        #endregion

        #region Window's logic
        private void UpdateGridFilteredPatients()
        {
            string searchTerm = txtSearchTerm.Text;

            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    _patientsViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.IsDeleted == false && u.FirstName.Contains(searchTerm), "AssignedId", "asc");
                    break;
                case 1:
                    _patientsViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.IsDeleted == false && u.LastName.Contains(searchTerm), "AssignedId", "asc");
                    break;
                case 2:
                    _patientsViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.IsDeleted == false && u.Email.Contains(searchTerm), "AssignedId", "asc");
                    break;
                default:
                    break;
            }

            this.DataContext = _patientsViewModel;
        }

        private void UpdateGridAllPatients()
        {
            _patientsViewModel = new Controllers.CustomViewModel<Model.Patient>(u => u.IsDeleted == false, "AssignedId", "asc");
            this.DataContext = _patientsViewModel;
        }
        #endregion
    }
}