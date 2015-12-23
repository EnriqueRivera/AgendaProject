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
        private Model.TreatmentPayment _treatmentPayment;
        private Model.Patient _selectedPatient;
        internal event EventHandler<bool> OnTreatmentDeleted;
        internal event EventHandler<bool> OnTreatmentEdited;
        private Model.TreatmentPrice _treatmentPrice;
        #endregion

        #region Getters and setters
        public Model.TreatmentPayment TreatmentPayment
        {
            get { return _treatmentPayment; }

            set { _treatmentPayment = value; }
        }

        public Model.TreatmentPrice TreatmentPrice
        {
            get { return _treatmentPrice; }

            set { _treatmentPrice = value; }

        }
        #endregion

        #region Constructors
        public TreatmentPriceControl(Model.TreatmentPayment treatment, Model.Patient selectedPatient)
		{
			this.InitializeComponent();

            _treatmentPayment = treatment;
            _selectedPatient = selectedPatient;

            UpdateTreatmentInfo();
		}

        public TreatmentPriceControl(Model.Patient patient)
        {
            this.InitializeComponent();

            _selectedPatient = patient;
        }
        #endregion

        #region Window event handlers
        private void btnRemoveTreatmentPrice_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (IsNewTreatment()
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
            if (_treatmentPayment != null)
            {
                new AddEditTreatmentPaymentModal(_treatmentPayment, _selectedPatient, this).ShowDialog();
                OnTreatmentEdited(this, true);
            }
        }
        #endregion

        #region Window's logic
        private void UpdateTreatmentInfo()
        {
            lblTreatmentName.ToolTip = lblTreatmentName.Text = _treatmentPrice == null ? string.Empty : string.Format("{0} - {1} ({2})", _treatmentPrice.TreatmentKey, _treatmentPrice.Name, _treatmentPrice.Type);
            
            if (_treatmentPayment != null)
            {
                lblQuantity.ToolTip = lblQuantity.Text = _treatmentPayment.Quantity.ToString();
                lblDiscount.ToolTip = lblDiscount.Text = _treatmentPayment.Discount.ToString() + "%";
                lblUnitPrice.ToolTip = lblUnitPrice.Text = "$" + _treatmentPayment.Price.ToString("0.00");
                lblTotal.ToolTip = lblTotal.Text = "$" + _treatmentPayment.Total.ToString("0.00");
                lblTreatmentDate.ToolTip = lblTreatmentDate.Text = _treatmentPayment.TreatmentDate.ToString("dd/MMMM/yyyy");

                if (_treatmentPayment.TreatmentPaymentId != 0)
	            {
		            btnEditTreatmentPrice.Visibility = System.Windows.Visibility.Hidden;
                    btnRemoveTreatmentPrice.Visibility = System.Windows.Visibility.Hidden;
	            }
            }
        }

        public void UpdateData()
        {
            UpdateTreatmentInfo();
        }

        public bool IsNewTreatment()
        {
            return _treatmentPayment != null && _treatmentPayment.TreatmentPaymentId == 0;
        }
        #endregion
    }
}