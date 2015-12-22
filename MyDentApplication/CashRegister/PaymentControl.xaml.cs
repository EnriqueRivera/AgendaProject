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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for PaymentControl.xaml
	/// </summary>
	public partial class PaymentControl : UserControl
	{
        #region Instance variables
        private Model.Payment _payment;
        internal event EventHandler<bool> OnPaymentDeleted;
        internal event EventHandler<bool> OnPaymentEdited;
        private Model.PositiveBalance _positiveBalance = null;
        #endregion

        #region Getters and setters
        public Model.Payment Payment
        {
            get { return _payment; }

            set { _payment = value; }

        }

        public Model.PositiveBalance PositiveBalance
        {
            get { return _positiveBalance; }
        }
        #endregion

        #region Constructors
        public PaymentControl(Model.Payment payment, Model.PositiveBalance positiveBalance)
		{
			this.InitializeComponent();

            _positiveBalance = positiveBalance;
            Payment = payment;

            UpdatePaymentInfo();
		}

        public PaymentControl()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Window event handlers
        private void btnRemovePayment_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (_payment != null 
                && _payment.PaymentId == 0
                && MessageBox.Show("¿Está seguro(a) que desea eliminar el pago?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                OnPaymentDeleted(this, true);
            }
		}

		private void btnEditPayment_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (_payment != null)
            {
                new AddEditPaymentModal(_payment, (Controllers.PaymentType)Enum.Parse(typeof(Controllers.PaymentType), _payment.Type, true), this).ShowDialog();
                OnPaymentEdited(this, true);
            }
        }
        #endregion

        #region Window's logic
        private void UpdatePaymentInfo()
        {
            if (_payment != null)
            {
                lblPaymentType.ToolTip = lblPaymentType.Text = _payment.Type;
                lblBankName.ToolTip = lblBankName.Text = _payment.Bank == null ? "N/A" : _payment.Bank.Name;
                lblObservations.ToolTip = lblObservations.Text = _payment.Observation;
                lblAmount.ToolTip = lblAmount.Text = "$" + _payment.Amount.ToString("0.00");
                lblVoucherCheckNumber.ToolTip = lblVoucherCheckNumber.Text = string.IsNullOrEmpty(_payment.VoucherCheckNumber) ? "N/A" : _payment.VoucherCheckNumber;
                lblPaymentDate.ToolTip = lblPaymentDate.Text = _payment.PaymentDate.ToString("dd/MMMM/yyyy");

                btnEditPayment.IsEnabled = btnRemovePayment.IsEnabled = _payment.PaymentId == 0 && _positiveBalance == null;
            }
        }

        public void UpdateData()
        {
            UpdatePaymentInfo();
        }
        #endregion
    }
}