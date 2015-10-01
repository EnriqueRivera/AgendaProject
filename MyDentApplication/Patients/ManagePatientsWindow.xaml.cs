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
        #endregion    

        #region Constructors
        public ManagePatientsWindow()
		{
			this.InitializeComponent();
            UpdateGridAllPatients();
		}
        #endregion

        #region Window event handlers
        private void btnRefreshPatients_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            UpdateGridFilteredPatients();
		}

		private void btnViewAllPatients_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            UpdateGridAllPatients();
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}
        #endregion

        #region Window's logic
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