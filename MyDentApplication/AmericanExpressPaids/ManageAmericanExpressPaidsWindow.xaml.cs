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
	/// Interaction logic for ManageAmericanExpressPaidsWindow.xaml
	/// </summary>
	public partial class ManageAmericanExpressPaidsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.AmericanExpressPaid> _paidsViewModel;
        #endregion

        #region Constructors
        public ManageAmericanExpressPaidsWindow()
		{
			this.InitializeComponent();

            dpStartDate.SelectedDate = DateTime.Now;
            dpEndDate.SelectedDate = DateTime.Now;
            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnDeletePaid_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.AmericanExpressPaid paidSelected = dgAmericanExpressPaids.SelectedItem == null ? null : dgAmericanExpressPaids.SelectedItem as Model.AmericanExpressPaid;

            if (paidSelected == null)
            {
                MessageBox.Show("Seleccione un pago", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el pago con el concepto de: '{0}'?",
                                        paidSelected.Concept),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                if (Controllers.BusinessController.Instance.Delete<Model.AmericanExpressPaid>(paidSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el pago", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

		private void btnEditPaid_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.AmericanExpressPaid paidSelected = dgAmericanExpressPaids.SelectedItem == null ? null : dgAmericanExpressPaids.SelectedItem as Model.AmericanExpressPaid;

            if (paidSelected == null)
            {
                MessageBox.Show("Seleccione un pago", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditAmericanExpressPaidsModal(paidSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnAddPaid_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditAmericanExpressPaidsModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            UpdateGrid();
		}
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            DateTime startDate = dpStartDate.SelectedDate.Value;
            DateTime endDate = dpEndDate.SelectedDate.Value;

            _paidsViewModel = new Controllers.CustomViewModel<Model.AmericanExpressPaid>(i => EntityFunctions.TruncateTime(i.PaidDate) >= EntityFunctions.TruncateTime(startDate) && EntityFunctions.TruncateTime(i.PaidDate) <= EntityFunctions.TruncateTime(endDate), "PaidDate", "asc");

            this.DataContext = _paidsViewModel;

            decimal total = _paidsViewModel.ObservableData.Sum(i => i.Total);
            lblTotal.ToolTip = lblTotal.Content = "Total del periodo: $" + total.ToString("0.00");
        }
        #endregion
	}
}