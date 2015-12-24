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
	/// Interaction logic for ViewStatementWindow.xaml
	/// </summary>
	public partial class ViewStatementWindow : Window
    {
        #region Instance variables
        private Model.Statement _statement;
        private Controllers.CustomViewModel<Model.TreatmentPayment> _treatmentsViewModel;
        private Controllers.CustomViewModel<Model.Payment> _paymentsViewModel;
        private List<Model.TreatmentPayment> _treatments;
        private List<Model.Payment> _payments;
        private decimal _totalAmountOfTreatments;
        private decimal _totalAmountOfPayments;
        private decimal _grandTotal;
        private decimal _positiveBalance;
        private int _numberOfTreatments;
        private int _numberOfPayments;
        #endregion

        #region Constructors
        public ViewStatementWindow(Model.Statement statement)
        {
            this.InitializeComponent();

            _statement = statement;

            lblPatientName.ToolTip = lblPatientName.Content = string.Format("(Exp. No. {0}) {1} {2}", _statement.Patient.PatientId, _statement.Patient.FirstName, _statement.Patient.LastName);
            lblAccountStatusNumber.ToolTip = lblAccountStatusNumber.Content = _statement.StatementId.ToString();

            UpdateStatementInfo();
        }
        #endregion

        #region Window event handlers
        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}

		private void btnGeneratePdf_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO: Add event handler implementation here.
		}

		private void btnSendMail_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// TODO: Add event handler implementation here.
        }
        #endregion

        #region Window's logic
        private void UpdateStatementInfo()
        {
            _treatments = _statement.TreatmentPayments
                            .OrderBy(t => t.TreatmentDate)
                            .ToList();

            _payments = _statement.Payments
                            .OrderBy(p => p.PaymentDate)
                            .ToList();

            _treatmentsViewModel = new Controllers.CustomViewModel<Model.TreatmentPayment>(_treatments);
            _paymentsViewModel = new Controllers.CustomViewModel<Model.Payment>(_payments);

            dgTreatments.DataContext = _treatmentsViewModel;
            dgPayments.DataContext = _paymentsViewModel;

            UpdateTotals();
        }

        private void UpdateTotals()
        {
            _totalAmountOfTreatments = 0m;
            _totalAmountOfPayments = 0m;
            _grandTotal = 0m;
            _numberOfTreatments = 0;
            _numberOfPayments = 0;
            _positiveBalance = 0m;

            foreach (var item in _treatments)
            {
                _totalAmountOfTreatments += item.Total;
                _numberOfTreatments += item.Quantity;
            }

            foreach (var item in _payments)
            {
                _totalAmountOfPayments += item.Amount;
                _numberOfPayments++;
            }

            _grandTotal = _totalAmountOfTreatments - _totalAmountOfPayments;
            
            if (_grandTotal < 0m)
            {
                _positiveBalance = Math.Abs(_grandTotal);
                _grandTotal = 0m;
            }

            lblTreatmentsCount.ToolTip = lblTreatmentsCount.Content = "No. de tratamientos: " + _numberOfTreatments;
            lblPaymentsCount.ToolTip = lblPaymentsCount.Content = "No. de pagos: " + _numberOfPayments;
            lblTotalAmountTreatments.ToolTip = lblTotalAmountTreatments.Text = "$" + _totalAmountOfTreatments.ToString("0.00");
            lblTotalAmountPayments.ToolTip = lblTotalAmountPayments.Text = "$" + _totalAmountOfPayments.ToString("0.00");
            lblGrandTotal.ToolTip = lblGrandTotal.Text = "$" + _grandTotal.ToString("0.00");
            lblPositiveBalance.ToolTip = lblPositiveBalance.Text = "$" + _positiveBalance.ToString("0.00");
        }
        #endregion
    }
}