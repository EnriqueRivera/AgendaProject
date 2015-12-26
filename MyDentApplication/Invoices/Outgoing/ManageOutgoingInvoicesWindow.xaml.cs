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
	/// Interaction logic for ManageOutgoingInvoicesWindow.xaml
	/// </summary>
	public partial class ManageOutgoingInvoicesWindow : Window
	{
		#region Instance variables
        private Controllers.CustomViewModel<Model.OutgoingInvoice> _invoicesViewModel;
        #endregion

        #region Constructors
        public ManageOutgoingInvoicesWindow()
		{
			this.InitializeComponent();

            dtudSelectedMonth.Value = DateTime.Now;
            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnAddInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditOutgoingInvoicesModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.OutgoingInvoice invoiceSelected = dgInvoices.SelectedItem == null ? null : dgInvoices.SelectedItem as Model.OutgoingInvoice;

            if (invoiceSelected == null)
            {
                MessageBox.Show("Seleccione una factura", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditOutgoingInvoicesModal(invoiceSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.OutgoingInvoice invoiceSelected = dgInvoices.SelectedItem == null ? null : dgInvoices.SelectedItem as Model.OutgoingInvoice;

            if (invoiceSelected == null)
            {
                MessageBox.Show("Seleccione una factura", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar la factura seleccionada con el folio '{0}'?",
                                        invoiceSelected.Folio),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                invoiceSelected.IsDeleted = true;

                if (Controllers.BusinessController.Instance.Update<Model.OutgoingInvoice>(invoiceSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la factura", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

		private void btnViewInvoices_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            UpdateGrid();
		}
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            DateTime selectedDate = dtudSelectedMonth.Value.Value;

            _invoicesViewModel = cbFilter.SelectedIndex == 0
                                ? new Controllers.CustomViewModel<Model.OutgoingInvoice>(i => i.IsDeleted == false && i.InvoiceDate.Value.Month == selectedDate.Month && i.InvoiceDate.Value.Year == selectedDate.Year, "InvoiceDate", "asc")
                                : new Controllers.CustomViewModel<Model.OutgoingInvoice>(i => i.IsDeleted == false && i.PaidDate.Month == selectedDate.Month && i.PaidDate.Year == selectedDate.Year, "PaidDate", "asc");   
            
            
            this.DataContext = _invoicesViewModel;

            decimal totalMonth = _invoicesViewModel.ObservableData.Sum(i => i.TotalAmount);
            lblTotalMonth.ToolTip = lblTotalMonth.Content = "Total del mes: $" + totalMonth.ToString("0.00");
        }
        #endregion
	}
}