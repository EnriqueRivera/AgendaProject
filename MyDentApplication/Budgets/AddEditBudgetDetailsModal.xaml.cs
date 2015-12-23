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
	/// Interaction logic for AddEditBudgetDetailsModal.xaml
	/// </summary>
	public partial class AddEditBudgetDetailsModal : Window
	{
        #region Instance variables
        private Model.BudgetDetail _budgetDetailToUpdate;
        private bool _isUpdateBudgetDetial;
        #endregion

        #region Constructors
        public AddEditBudgetDetailsModal(Model.BudgetDetail budgetDetailToUpdate)
		{
			this.InitializeComponent();

            _budgetDetailToUpdate = budgetDetailToUpdate;
            _isUpdateBudgetDetial = _budgetDetailToUpdate.BudgetDetailId != -1;
            FillTreatments();

            if (_isUpdateBudgetDetial)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddUpdateBudgetDetail_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            decimal unitCost;
            int quantity;
            int numberOfEvents;
            int discount = (int)cbDiscount.SelectedItem;

            if (AreValidFields(out unitCost, out quantity, out numberOfEvents) == false)
            {
                return;
            }

            _budgetDetailToUpdate.Quantity = quantity;
            _budgetDetailToUpdate.Concept = cbConcepts.SelectedValue.ToString();
            _budgetDetailToUpdate.NumberOfEvents = numberOfEvents;
            _budgetDetailToUpdate.UnitCost = unitCost;
            _budgetDetailToUpdate.Discount = discount;
            _budgetDetailToUpdate.UnitCostDiscount = ((100 - discount) / 100m) * unitCost;
            _budgetDetailToUpdate.NetTotal = unitCost * quantity;
            _budgetDetailToUpdate.TotalDiscount = ((100 - discount) / 100m) * _budgetDetailToUpdate.NetTotal;
            _budgetDetailToUpdate.TotalPerEvent = _budgetDetailToUpdate.TotalDiscount / ((decimal)numberOfEvents);

            _budgetDetailToUpdate.BudgetDetailId = _isUpdateBudgetDetial ? _budgetDetailToUpdate.BudgetDetailId : 0;

            this.Close();
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}
        #endregion

        #region Window's logic
        private bool AreValidFields(out decimal unitCost, out int quantity, out int numberOfEvents)
        {
            unitCost = quantity = numberOfEvents = 0;

            if (cbConcepts.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un concepto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (int.TryParse(txtQuantity.Text.Trim(), out quantity) == false)
            {
                MessageBox.Show("Ingrese una cantidad válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (quantity < 1)
            {
                MessageBox.Show("La cantidad no puede ser menor a 1", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (int.TryParse(txtNumberOfEvents.Text.Trim(), out numberOfEvents) == false)
            {
                MessageBox.Show("Ingrese un número de citas válido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (numberOfEvents < 1)
            {
                MessageBox.Show("El número de citas no puede ser menor a 1", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (decimal.TryParse(txtCost.Text.ToString().Trim(), out unitCost) == false)
            {
                MessageBox.Show("Ingrese un Costo u. normal válido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (unitCost < 0)
            {
                MessageBox.Show("El costo no puede ser menor a 0", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar concepto del presupuesto";
            btnAddUpdateBudgetDetail.Content = "Actualizar";
            txtQuantity.ToolTip = txtQuantity.Text = _budgetDetailToUpdate.Quantity.ToString();
            txtNumberOfEvents.ToolTip = txtNumberOfEvents.Text = _budgetDetailToUpdate.NumberOfEvents.ToString();
            txtCost.ToolTip = txtCost.Text = _budgetDetailToUpdate.UnitCost.ToString();
            cbDiscount.SelectedValue = _budgetDetailToUpdate.Discount;
            cbConcepts.SelectedValue = _budgetDetailToUpdate.Concept;
        }

        private void FillTreatments()
        {
            List<Model.BudgetTreatment> treatments = BusinessController.Instance.FindBy<Model.BudgetTreatment>(t => t.IsDeleted == false)
                                                        .OrderBy(t => t.Name)
                                                        .ToList();

            foreach (Model.BudgetTreatment treatment in treatments)
            {
                cbConcepts.Items.Add(treatment.Name);
            }
        }
        #endregion
    }
}