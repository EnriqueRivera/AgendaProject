using Controllers;
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
	/// Interaction logic for AddEditTreatmentPaymentModal.xaml
	/// </summary>
	public partial class AddEditTreatmentPaymentModal : Window
	{
        #region Instance variables
        private Model.TreatmentPayment _treatment;
        private Model.Patient _selectedPatient;
        private bool _isUpdateTreatment;
        #endregion

        #region Constructors
        public AddEditTreatmentPaymentModal(Model.TreatmentPayment treatment, Model.Patient selectedPatient)
		{
			this.InitializeComponent();

            FillTreatments();

            _treatment = treatment;
            _selectedPatient = selectedPatient;
            _isUpdateTreatment = _treatment != null;

            if (_isUpdateTreatment)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbTreatments_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }
        #endregion

        #region Window's logic
        private void PrepareWindowForUpdates()
        {
            //
        }

        private void FillTreatments()
        {
            List<Model.TreatmentPrice> treatments = BusinessController.Instance.GetAll<Model.TreatmentPrice>()
                                                    .Where(tp => tp.CreatedDate.Year == DateTime.Now.Year 
                                                            && tp.IsDeleted == false 
                                                            && tp.Type.Contains(Utils.TREATMENT_HEALTH_INSURANCE) == _selectedPatient.HasHealthInsurance)
                                                    .OrderBy(tp => tp.TreatmentKey)
                                                    .OrderBy(tp => tp.Name)
                                                    .ToList();

            cbTreatments.Items.Add(new Controllers.ComboBoxItem() { Text = string.Empty, Value = null });

            foreach (Model.TreatmentPrice treatment in treatments)
            {
                cbTreatments.Items.Add(new Controllers.ComboBoxItem() { Text = string.Format("{0} - {1} ({2})", treatment.TreatmentKey, treatment.Name, treatment.Type), Value = treatment });
            }

            cbTreatments.SelectedIndex = 0;
        }
        #endregion
    }
}