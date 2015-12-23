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
        private Model.TreatmentPrice _selectedTreatmentPrice;
        private TreatmentPriceControl _treatmentControl;
        private decimal _total;
        private decimal _price;
        private int _quantity;
        private int _discount;
        #endregion

        #region Constructors
        public AddEditTreatmentPaymentModal(Model.TreatmentPayment treatment, Model.Patient selectedPatient, TreatmentPriceControl treatmentControl)
		{
			this.InitializeComponent();

            _treatmentControl = treatmentControl;
            _treatment = treatment;
            _selectedPatient = selectedPatient;
            _isUpdateTreatment = _treatment != null;

            FillTreatments();

            if (_isUpdateTreatment)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_selectedTreatmentPrice == null)
            {
                MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (UpdateTotalAmount() == false)
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

        private void cbTreatments_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedTreatmentPrice = (cbTreatments.SelectedItem as Controllers.ComboBoxItem).Value as Model.TreatmentPrice;

            if (_selectedTreatmentPrice != null)
            {
                UpdateCostAndDiscount();
            }
        }

        private void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateTotalAmount();
        }
        #endregion

        #region Window's logic
        private void UpdateCostAndDiscount()
        {
            txtTreatmentCost.Text = _selectedTreatmentPrice.Price.ToString();
            cbDiscount.SelectedValue = _selectedTreatmentPrice.Discount;
        }

        private bool UpdateTotalAmount()
        {
            _discount = Convert.ToInt32(cbDiscount.SelectedItem);

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

            _total = _quantity * _price;
            _total = (_total - (_discount * _total / 100m));
            txtTotalAmount.Text = _total.ToString();

            return true;            
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar tratamiento de caja";
            btnAddTreatment.Content = "Actualizar";

            for (int i = 0; i < cbTreatments.Items.Count; i++)
            {
                Model.TreatmentPrice treatmentItem = ((cbTreatments.Items[i] as Controllers.ComboBoxItem).Value as Model.TreatmentPrice);
                if (treatmentItem != null && treatmentItem.TreatmentPriceId == _treatment.TreatmentPriceId)
                {
                    cbTreatments.SelectedIndex = i;
                    break;
                }
            }

            txtTreatmentCost.Text = _treatment.Price.ToString();
            txtQuantity.Text = _treatment.Quantity.ToString();
            txtTotalAmount.Text = _treatment.Total.ToString();
            cbDiscount.SelectedValue = _treatment.Discount;
        }

        private void FillTreatments()
        {
            List<Model.TreatmentPrice> treatments = BusinessController.Instance.FindBy<Model.TreatmentPrice>(tp => tp.CreatedDate.Year == DateTime.Now.Year && tp.IsDeleted == false)
                                                        .OrderBy(tp => tp.TreatmentKey)
                                                        .ThenBy(tp => tp.Name)
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