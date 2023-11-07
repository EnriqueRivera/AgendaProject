using Controllers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Data.Objects;
using System.Collections.ObjectModel;

namespace MyDentApplication
{
    /// <summary>
    /// Interaction logic for ViewPaymentsWindow.xaml
    /// </summary>
    public partial class ViewPaymentsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Payment> _paymentsViewModel;
        #endregion

        #region Constructors
        public ViewPaymentsWindow()
		{
			this.InitializeComponent();

            dpStartDate.SelectedDate = DateTime.Now;
            dpEndDate.SelectedDate = DateTime.Now;
            lblTotal.ToolTip = lblTotal.Content = "Total: $0.00";

            FillPatients();
            FillPaymentTypes();
        }
        #endregion

        #region Window event handlers
        private void btnFilter_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            UpdateGrid();
		}
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            DateTime startDate = dpStartDate.SelectedDate.Value;
            DateTime endDate = dpEndDate.SelectedDate.Value;
            Model.Patient selectedPatient = (cbPatients.SelectedValue as Controllers.ComboBoxItem).Value as Model.Patient;
            string selectedPaymentType = (cbPaymentTypes.SelectedValue as Controllers.ComboBoxItem).Value as string;

            _paymentsViewModel = new Controllers.CustomViewModel<Model.Payment>(
                p => EntityFunctions.TruncateTime(p.PaymentDate) >= EntityFunctions.TruncateTime(startDate) 
                    && EntityFunctions.TruncateTime(p.PaymentDate) <= EntityFunctions.TruncateTime(endDate)
                    , "PaymentDate", "asc");

            if (selectedPatient != null)
            {
                List<Model.Payment> paymentsFilteredByPatient = _paymentsViewModel.ObservableData.Where(p => p.PaymentFolio.PatientId == selectedPatient.PatientId).ToList();
                _paymentsViewModel.ObservableData = new ObservableCollection<Model.Payment>(paymentsFilteredByPatient);
            }

            if (selectedPaymentType != null)
            {
                List<Model.Payment> paymentsFilteredBypaymentType = _paymentsViewModel.ObservableData.Where(p => p.Type == selectedPaymentType).ToList();
                _paymentsViewModel.ObservableData = new ObservableCollection<Model.Payment>(paymentsFilteredBypaymentType);
            }

            this.DataContext = _paymentsViewModel;

            decimal total = _paymentsViewModel.ObservableData.Sum(p => p.Amount);
            lblTotal.ToolTip = lblTotal.Content = "Total: $" + total.ToString("n");
        }

        private void FillPatients()
        {
            List<Model.Patient> patients = BusinessController.Instance.FindBy<Model.Patient>(p => p.IsDeleted == false)
                                                .OrderBy(p => p.FirstName)
                                                .ThenBy(p => p.LastName)
                                                .ThenBy(p => p.AssignedId)
                                                .ToList();

            cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = "", Value = null });

            foreach (Model.Patient patient in patients)
            {
                cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = string.Format("{1} {2} (Exp. No. {0})", patient.AssignedId, patient.FirstName, patient.LastName), Value = patient });
            }

            cbPatients.SelectedIndex = 0;
        }

        private void FillPaymentTypes()
        {
            cbPaymentTypes.Items.Add(new Controllers.ComboBoxItem() { Text = "", Value = null });

            foreach (PaymentType type in Enum.GetValues(typeof(PaymentType)))
            {
                cbPaymentTypes.Items.Add(new Controllers.ComboBoxItem() { Text = type.ToString(), Value = type.ToString() });
            }

            cbPaymentTypes.SelectedIndex = 0;
        }
        #endregion
    }
}