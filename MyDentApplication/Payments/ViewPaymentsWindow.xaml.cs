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
using System.Data.Objects;

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

            if (selectedPatient == null)
                _paymentsViewModel = new Controllers.CustomViewModel<Model.Payment>(p => EntityFunctions.TruncateTime(p.PaymentDate) >= EntityFunctions.TruncateTime(startDate) && EntityFunctions.TruncateTime(p.PaymentDate) <= EntityFunctions.TruncateTime(endDate), "PaymentDate", "asc");
            else
                _paymentsViewModel = new Controllers.CustomViewModel<Model.Payment>(p => EntityFunctions.TruncateTime(p.PaymentDate) >= EntityFunctions.TruncateTime(startDate) && EntityFunctions.TruncateTime(p.PaymentDate) <= EntityFunctions.TruncateTime(endDate) && p.PaymentFolio.PatientId == selectedPatient.PatientId, "PaymentDate", "asc");
            

            this.DataContext = _paymentsViewModel;

            decimal total = _paymentsViewModel.ObservableData.Sum(p => p.Amount);
            lblTotal.ToolTip = lblTotal.Content = "Total: $" + total.ToString("0.00");
        }

        private void FillPatients()
        {
            List<Model.Patient> patients = BusinessController.Instance.FindBy<Model.Patient>(p => p.IsDeleted == false)
                                            .OrderBy(p => p.PatientId)
                                            .ThenBy(p => p.FirstName)
                                            .ThenBy(p => p.LastName)
                                            .ToList();

            cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = "", Value = null });

            foreach (Model.Patient patient in patients)
            {
                cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = string.Format("(Exp. No. {0}) {1} {2}", patient.PatientId, patient.FirstName, patient.LastName), Value = patient });
            }

            cbPatients.SelectedIndex = 0;
        }
        #endregion
    }
}