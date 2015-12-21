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
	/// Interaction logic for TreatmentPriceControl.xaml
	/// </summary>
	public partial class TreatmentPriceControl : UserControl
    {
        #region Instance variables
        private Model.TreatmentPayment _treatment;
        private Model.Patient _patient;
        internal event EventHandler<bool> OnTreatmentDeleted;
        internal event EventHandler<bool> OnTreatmentEdited;
        #endregion

        #region Getters and setters
        public Model.TreatmentPayment Treatment
        {
            get { return _treatment; }

            set
            {
                _treatment = value;
                UpdateTreatmentInfo();
            }

        }
        #endregion

        #region Constructors
        public TreatmentPriceControl(Model.TreatmentPayment treatment, Model.Patient patient)
		{
			this.InitializeComponent();

            Treatment = treatment;
            _patient = patient;
		}

        public TreatmentPriceControl(Model.Patient patient)
        {
            this.InitializeComponent();

            _patient = patient;
        }
        #endregion

        #region Window event handlers
        private void btnRemoveTreatmentPrice_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (_treatment != null 
                && _treatment.TreatmentPaymentId == 0
                && MessageBox.Show("¿Está seguro(a) que desea eliminar el tratamiento?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                OnTreatmentDeleted(this, true);
            }
		}

		private void btnEditTreatmentPrice_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (_treatment != null)
            {
                new AddEditTreatmentPaymentModal(_treatment, _userLoggedIn).ShowDialog();
                UpdateTreatmentInfo();
                OnTreatmentEdited(this, true);
            }
        }
        #endregion

        #region Window's logic
        private void UpdateTreatmentInfo()
        {
            if (_treatment != null)
            {
                lblTreatmentName.ToolTip = lblTreatmentName.Text = string.Format("{0} ({1})", _treatment.TreatmentPrice.Name, _treatment.TreatmentPrice.Type);
                lblQuantity.ToolTip = lblQuantity.Text = _treatment.Quantity.ToString();
                lblDiscount.ToolTip = lblDiscount.Text = _treatment.Discount.ToString() + "%";
                lblUnitPrice.ToolTip = lblUnitPrice.Text = "$" + _treatment.Price.ToString("0.00");
                lblTotal.ToolTip = lblTotal.Text = "$" + _treatment.Total.ToString("0.00");
                lblTreatmentDate.ToolTip = lblTreatmentDate.Text = _treatment.TreatmentDate.ToString("dd/MMMM/yyyy");

                btnEditTreatmentPrice.IsEnabled = btnRemoveTreatmentPrice.IsEnabled = _treatment.TreatmentPaymentId == 0;
            }
        }
        #endregion
    }
}