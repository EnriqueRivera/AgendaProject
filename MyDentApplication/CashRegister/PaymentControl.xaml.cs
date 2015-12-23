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
        private Model.PositiveBalance _positiveBalance;
        private Model.Bank _bank;
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

        public Model.Bank Bank
        {
            get { return _bank; }
            set { _bank = value; }
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
            if (IsNewPayment()
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
                new AddEditPaymentModal(_payment, (Controllers.PaymentType)Enum.Parse(typeof(Controllers.PaymentType), _payment.Type, true), this, _bank).ShowDialog();
                OnPaymentEdited(this, true);
            }
        }

        private void btnPositiveBalanceMessage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Saldo a favor agregado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region Window's logic
        private void UpdatePaymentInfo()
        {
            if (_payment != null)
            {
                lblPaymentType.ToolTip = lblPaymentType.Text = _payment.Type;
                lblObservations.ToolTip = lblObservations.Text = _payment.Observation;
                lblAmount.ToolTip = lblAmount.Text = "$" + _payment.Amount.ToString("0.00");
                lblVoucherCheckNumber.ToolTip = lblVoucherCheckNumber.Text = string.IsNullOrEmpty(_payment.VoucherCheckNumber) ? "N/A" : _payment.VoucherCheckNumber;
                lblPaymentDate.ToolTip = lblPaymentDate.Text = _payment.PaymentDate.ToString("dd/MMMM/yyyy");
                lblBankName.ToolTip = lblBankName.Text = _payment.Bank == null
                                                            ? (_bank == null ? "N/A" : _bank.Name)
                                                            : _payment.Bank.Name;

                if (_payment.PaymentId != 0 || _positiveBalance != null)
	            {
                    btnEditPayment.Visibility = System.Windows.Visibility.Hidden;
                    btnRemovePayment.Visibility = System.Windows.Visibility.Hidden;
                    btnPositiveBalanceMessage.Visibility = _positiveBalance == null
                                                            ? btnPositiveBalanceMessage.Visibility
                                                            : System.Windows.Visibility.Visible;
	            }
            }
        }

        public void UpdateData()
        {
            UpdatePaymentInfo();
        }

        public bool IsNewPayment()
        {
            return _payment != null && _payment.PaymentId == 0;
        }
        #endregion
    }
}