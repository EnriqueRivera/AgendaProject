using System;
using System.Collections.Generic;
using System.Data.Objects;
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
	/// Interaction logic for ViewPaymentFoliosWindow.xaml
	/// </summary>
	public partial class ViewPaymentFoliosWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.PaymentFolio> _paymentFoliosViewModel;
        private Model.User _userLoggedIn;
        #endregion

        #region Constructors
        public ViewPaymentFoliosWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;

            dpStartDate.SelectedDate = DateTime.Now;
            dpEndDate.SelectedDate = DateTime.Now;
		}
        #endregion

        #region Window event handlers
        private void btnSearch_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            int folioNumber;
            if (int.TryParse(txtFolioNumber.Text, out folioNumber) == false)
            {
                MessageBox.Show("Ingrese un número de folio válido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _paymentFoliosViewModel = new Controllers.CustomViewModel<Model.PaymentFolio>(pf => pf.FolioNumber == folioNumber, "FolioNumber", "asc");
                this.DataContext = _paymentFoliosViewModel;
            }
		}

        private void btnFilter_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            DateTime startDate = dpStartDate.SelectedDate.Value;
            DateTime endDate = dpEndDate.SelectedDate.Value;

            _paymentFoliosViewModel = new Controllers.CustomViewModel<Model.PaymentFolio>(pf => EntityFunctions.TruncateTime(pf.FolioDate) >= EntityFunctions.TruncateTime(startDate) && EntityFunctions.TruncateTime(pf.FolioDate) <= EntityFunctions.TruncateTime(endDate), "FolioDate", "asc");
            this.DataContext = _paymentFoliosViewModel;
        }

        private void btnViewFolioDetails_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.PaymentFolio selectedFolio = dgFolioNumbers.SelectedItem == null ? null : dgFolioNumbers.SelectedItem as Model.PaymentFolio;

            if (selectedFolio == null)
            {
                MessageBox.Show("Seleccione un folio", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new ViewPaymentFolioDetailWindow(selectedFolio, _userLoggedIn).ShowDialog();
            }
        }
        #endregion
    }
}