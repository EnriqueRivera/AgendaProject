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

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ManageBudgetsWindow.xaml
	/// </summary>
	public partial class ManageBudgetsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Budget> _budgetsViewModel;
        #endregion

        #region Constructors
        public ManageBudgetsWindow()
		{
			this.InitializeComponent();

            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnViewBudget_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Budget budgetSelected = dgBudgets.SelectedItem == null ? null : dgBudgets.SelectedItem as Model.Budget;

            if (budgetSelected == null)
            {
                MessageBox.Show("Seleccione un presupuesto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //new ViewBudgetModal(null).ShowDialog();
            }
		}

		private void btnAddBudget_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditBudgetsModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditBudget_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Budget budgetSelected = dgBudgets.SelectedItem == null ? null : dgBudgets.SelectedItem as Model.Budget;

            if (budgetSelected == null)
            {
                MessageBox.Show("Seleccione un presupuesto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditBudgetsModal(budgetSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteBudget_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Budget budgetSelected = dgBudgets.SelectedItem == null ? null : dgBudgets.SelectedItem as Model.Budget;

            if (budgetSelected == null)
            {
                MessageBox.Show("Seleccione un presupuesto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el presupuesto con el nombre '{0}'?",
                                        budgetSelected.Name),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                budgetSelected.IsDeleted = true;

                if (Controllers.BusinessController.Instance.Update<Model.Budget>(budgetSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el presupuesto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            _budgetsViewModel = new Controllers.CustomViewModel<Model.Budget>(t => t.IsDeleted == false, "BudgetId", "desc");
            this.DataContext = _budgetsViewModel;
        }
        #endregion
    }
}