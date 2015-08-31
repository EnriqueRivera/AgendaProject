using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
using System.Text;
using System.Threading;
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
	/// Interaction logic for ManageInvoicesWindow.xaml
	/// </summary>
	public partial class ManageInvoicesWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Invoice> _invoicesViewModel;
        #endregion

        #region Constructors
        public ManageInvoicesWindow()
		{
			this.InitializeComponent();

            dtudSelectedMonth.Value = DateTime.Now;
            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnAddInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditInvoicesModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Invoice invoiceSelected = dgInvoices.SelectedItem == null ? null : dgInvoices.SelectedItem as Model.Invoice;

            if (invoiceSelected == null)
            {
                MessageBox.Show("Seleccione una factura", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditInvoicesModal(invoiceSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Invoice invoiceSelected = dgInvoices.SelectedItem == null ? null : dgInvoices.SelectedItem as Model.Invoice;

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

                if (Controllers.BusinessController.Instance.Update<Model.Invoice>(invoiceSelected))
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
                                ? new Controllers.CustomViewModel<Model.Invoice>(i => i.IsDeleted == false && i.InvoiceDate.Month == selectedDate.Month && i.InvoiceDate.Year == selectedDate.Year, "InvoiceDate", "asc")
                                : new Controllers.CustomViewModel<Model.Invoice>(i => i.IsDeleted == false && i.PurchaseDate.Month == selectedDate.Month && i.PurchaseDate.Year == selectedDate.Year, "PurchaseDate", "asc");   
            
            
            this.DataContext = _invoicesViewModel;

            decimal totalMonth = _invoicesViewModel.ObservableData.Sum(i => i.TotalAmount);
            lblTotalMonth.ToolTip = lblTotalMonth.Content = "Total del mes: $" + totalMonth.ToString("0.00");
        }
        #endregion
	}

    public class DateValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
            {
                if (values[0] is DateTime)
                {
                    return ((DateTime)values[0]).ToString("dd/MMMM/yyyy");
                }
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}