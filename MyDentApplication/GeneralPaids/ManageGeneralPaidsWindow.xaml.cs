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
	/// Interaction logic for ManageGeneralPaidsWindow.xaml
	/// </summary>
	public partial class ManageGeneralPaidsWindow : Window
	{
		#region Instance variables
        private Controllers.CustomViewModel<Model.GeneralPaid> _generalPaidsViewModel;
        #endregion

        #region Constructors
        public ManageGeneralPaidsWindow()
		{
			this.InitializeComponent();

            dtudSelectedMonth.Value = DateTime.Now;
            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnAddGeneralPaid_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditGeneralPaidsModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditGeneralPaid_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.GeneralPaid paidSelected = dgGeneralPaids.SelectedItem == null ? null : dgGeneralPaids.SelectedItem as Model.GeneralPaid;

            if (paidSelected == null)
            {
                MessageBox.Show("Seleccione un pago", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditGeneralPaidsModal(paidSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteGeneralPaid_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.GeneralPaid paidSelected = dgGeneralPaids.SelectedItem == null ? null : dgGeneralPaids.SelectedItem as Model.GeneralPaid;

            if (paidSelected == null)
            {
                MessageBox.Show("Seleccione un pago", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el pago seleccionado con el No. de ticket '{0}'?",
                                        paidSelected.TicketNumber),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                if (Controllers.BusinessController.Instance.Delete<Model.GeneralPaid>(paidSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el pago", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

        private void btnRefreshGeneralPaids_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateGrid();
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            DateTime selectedDate = dtudSelectedMonth.Value.Value;

            _generalPaidsViewModel = new Controllers.CustomViewModel<Model.GeneralPaid>(i => i.PurchaseDate.Month == selectedDate.Month && i.PurchaseDate.Year == selectedDate.Year, "PurchaseDate", "asc");   
            
            this.DataContext = _generalPaidsViewModel;

            decimal totalMonth = _generalPaidsViewModel.ObservableData.Sum(i => i.TotalAmount);
            lblTotalMonth.ToolTip = lblTotalMonth.Content = "Total del mes: $" + string.Format("{0:n}", totalMonth);
        }
        #endregion
	}
}