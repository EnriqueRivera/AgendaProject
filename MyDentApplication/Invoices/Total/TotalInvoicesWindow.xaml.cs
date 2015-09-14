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
	/// Interaction logic for TotalInvoicesWindow.xaml
	/// </summary>
	public partial class TotalInvoicesWindow : Window
	{
		#region Instance variables
        private Controllers.CustomViewModel<Model.ReceivedInvoice> _receivedInvoicesViewModel;
        private Controllers.CustomViewModel<Model.OutgoingInvoice> _outgoingInvoicesViewModel;
        #endregion

        #region Constructors
        public TotalInvoicesWindow()
		{
			this.InitializeComponent();

            dtudSelectedMonth.Value = DateTime.Now;
            UpdateGrids();
		}
        #endregion

        #region Window event handlers
        private void btnRefreshInvoices_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateGrids();
        }
        #endregion

        #region Window's logic
        private void UpdateGrids()
        {
            DateTime selectedDate = dtudSelectedMonth.Value.Value;

            _receivedInvoicesViewModel = new Controllers.CustomViewModel<Model.ReceivedInvoice>(i => i.IsDeleted == false && i.InvoiceDate.Value.Month == selectedDate.Month && i.InvoiceDate.Value.Year == selectedDate.Year, "InvoiceDate", "asc");
            _outgoingInvoicesViewModel = new Controllers.CustomViewModel<Model.OutgoingInvoice>(i => i.IsDeleted == false && i.InvoiceDate.Value.Month == selectedDate.Month && i.InvoiceDate.Value.Year == selectedDate.Year, "InvoiceDate", "asc");
                        
            dgReceivedInvoices.DataContext = _receivedInvoicesViewModel;
            dgOutgoingInvoices.DataContext = _outgoingInvoicesViewModel;

            decimal receivedInvoicesTotal = _receivedInvoicesViewModel.ObservableData.Sum(i => i.TotalAmount);
            decimal outgoingInvoicesTotal = _outgoingInvoicesViewModel.ObservableData.Sum(i => i.TotalAmount);
            lblTotalMonth.ToolTip = lblTotalMonth.Content = "Diferencia: $" + (outgoingInvoicesTotal - receivedInvoicesTotal).ToString("0.00");
        }
        #endregion
	}
}