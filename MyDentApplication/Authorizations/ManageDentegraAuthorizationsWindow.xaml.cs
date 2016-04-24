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
	/// Interaction logic for ManageDentegraAuthorizationsWindow.xaml
	/// </summary>
	public partial class ManageDentegraAuthorizationsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.DentegraAuthorization> _elegibilitiesViewModel;
        #endregion

        #region Constructors
        public ManageDentegraAuthorizationsWindow()
		{
			this.InitializeComponent();

            FillAllPatients();
		}
        #endregion

        #region Window event handlers
        private void cbPatients_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
            UpdateGrid();
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            int selectedPatientId = Convert.ToInt32((cbPatients.SelectedItem as Controllers.ComboBoxItem).Value);

            _elegibilitiesViewModel = selectedPatientId == -1
                                ? new Controllers.CustomViewModel<Model.DentegraAuthorization>(a => true, "AuthorizationDate", "desc")
                                : new Controllers.CustomViewModel<Model.DentegraAuthorization>(a => a.PatientId == selectedPatientId, "AuthorizationDate", "desc");


            this.DataContext = _elegibilitiesViewModel;
        }

        private void FillAllPatients()
        {
            List<Model.Patient> patients = Controllers.BusinessController.Instance.FindBy<Model.Patient>(p => p.HasHealthInsurance && p.IsDiverse == false)
                                            .OrderBy(p => p.FirstName)
                                            .ThenBy(p => p.LastName)
                                            .ToList();

            cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = "Todos...", Value = -1 });

            foreach (Model.Patient patient in patients)
            {
                cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = string.Format("{1} {2} (Exp. No. {0})", patient.AssignedId, patient.FirstName, patient.LastName), Value = patient.PatientId });
            }
        }
        #endregion
    }
}