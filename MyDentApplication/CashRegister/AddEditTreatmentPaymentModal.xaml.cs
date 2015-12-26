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
        private bool _isUpdateTreatment;
        private Model.TreatmentPrice _selectedTreatmentPrice;
        private TreatmentPriceControl _treatmentControl;
        private decimal _total;
        private decimal _price;
        private int _quantity;
        private int _discount;
        #endregion

        #region Constructors
        public AddEditTreatmentPaymentModal(Model.TreatmentPayment treatment, TreatmentPriceControl treatmentControl)
		{
			this.InitializeComponent();

            cbDiscount.SelectedIndex = 0;
            _treatmentControl = treatmentControl;
            _treatment = treatment;
            _isUpdateTreatment = _treatment != null;
            
            if (_isUpdateTreatment)
            {
                PrepareWindowForUpdates();
                UpdateTotalFieldChanged();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AreValidFields() == false)
	        {
		        return;
	        }

            if (_isUpdateTreatment)
            {
                _treatment.TreatmentPriceId = _selectedTreatmentPrice.TreatmentPriceId;
                _treatment.TreatmentDate = DateTime.Now;
                _treatment.Price = _price;
                _treatment.Discount = _discount;
                _treatment.Quantity = _quantity;
                _treatment.Total = _total;

                _treatmentControl.TreatmentPayment = _treatment;
            }
            else
            {
                Model.TreatmentPayment treatmentToAdd = new Model.TreatmentPayment()
                {
                    TreatmentPriceId = _selectedTreatmentPrice.TreatmentPriceId,
                    TreatmentDate = DateTime.Now,
                    Price = _price,
                    Discount = _discount,
                    Quantity = _quantity,
                    Total = _total
                };

                _treatmentControl.TreatmentPayment = treatmentToAdd;                
            }

            _treatmentControl.TreatmentPrice = _selectedTreatmentPrice;
            _treatmentControl.Width = Double.NaN;
            _treatmentControl.UpdateData();

            this.Close();
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AreValidFields();
        }

        private void btnFindTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<Model.TreatmentPrice> selectedTreatments = new List<Model.TreatmentPrice>();
            new FindTreatmentPriceModal(selectedTreatments, txtTreatment.Text.Trim()).ShowDialog();

            _selectedTreatmentPrice = selectedTreatments.Count == 0 ? null : selectedTreatments[0];

            UpdateTreatment();
            UpdateCostAndDiscount();
        }

        private void btnRemoveTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _selectedTreatmentPrice = null;
            txtTreatment.Text = string.Empty;
            UpdateTreatment();
        }

        private void txtTreatmentCost_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateTotalFieldChanged();
        }

        private void cbDiscount_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateTotalFieldChanged();
        }

        private void txtQuantity_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateTotalFieldChanged();
        }
        #endregion

        #region Window's logic
        private void UpdateTotalFieldChanged()
        {
            if (decimal.TryParse(txtTreatmentCost.Text, out _price) == false
                || int.TryParse(cbDiscount.SelectedValue.ToString(), out _discount) == false
                || int.TryParse(txtQuantity.Text, out _quantity) == false)
            {
                txtTotalAmount.Text = string.Empty;
                return;
            }

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            _total = _quantity * _price;
            _total = (_total - (_discount * _total / 100m));
            txtTotalAmount.Text = _total.ToString();
        }

        private void UpdateCostAndDiscount()
        {
            if (_selectedTreatmentPrice != null)
            {
                txtTreatmentCost.Text = _selectedTreatmentPrice.Price.ToString();
                cbDiscount.SelectedValue = _selectedTreatmentPrice.Discount;   
            }
        }

        private bool AreValidFields()
        {
            if (_selectedTreatmentPrice == null)
            {
                MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (decimal.TryParse(txtTreatmentCost.Text, out _price) == false)
            {
                MessageBox.Show("Costo inválido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (_price <= 0m)
            {
                MessageBox.Show("El costo debe ser mayor a 0", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (int.TryParse(txtQuantity.Text, out _quantity) == false)
            {
                MessageBox.Show("Cantidad inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (_quantity <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            _discount = Convert.ToInt32(cbDiscount.SelectedItem);

            UpdateTotal();

            return true;            
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar tratamiento de caja";
            btnAddTreatment.Content = "Actualizar";
            txtTreatmentCost.Text = _treatment.Price.ToString();
            txtQuantity.Text = _treatment.Quantity.ToString();
            txtTotalAmount.Text = _treatment.Total.ToString();
            cbDiscount.SelectedValue = _treatment.Discount;
            UpdateTotalFieldChanged();

            _selectedTreatmentPrice = _treatmentControl.TreatmentPrice;
            UpdateTreatment();
        }

        private void UpdateTreatment()
        {
            if (_selectedTreatmentPrice == null)
            {
                txtTreatment.IsEnabled = true;
                btnFindTreatment.IsEnabled = true;
                btnRemoveTreatment.IsEnabled = false;
            }
            else
            {
                txtTreatment.ToolTip = txtTreatment.Text = string.Format("{0} - {1} ({2})", _selectedTreatmentPrice.TreatmentKey, _selectedTreatmentPrice.Name, _selectedTreatmentPrice.Type);
                txtTreatment.IsEnabled = false;
                btnFindTreatment.IsEnabled = false;
                btnRemoveTreatment.IsEnabled = true;
            }
        }
        #endregion
    }
}