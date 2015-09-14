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
	/// Interaction logic for AddEditGeneralPaidsModal.xaml
	/// </summary>
	public partial class AddEditGeneralPaidsModal : Window
	{
		#region Instance variables
        private Model.GeneralPaid _generalPaidToUpdate;
        private bool _isUpdateGeneralPaid;
        #endregion

        #region Constructors
        public AddEditGeneralPaidsModal(Model.GeneralPaid generalPaidToUpdate)
		{
			this.InitializeComponent();

            _generalPaidToUpdate = generalPaidToUpdate;
            _isUpdateGeneralPaid = generalPaidToUpdate != null;
            dtpPurchaseDate.SelectedDate = DateTime.Now;

            if (_isUpdateGeneralPaid)
            {
                PrepareWindowForUpdates();
            }
        }
        #endregion

        #region Window event handlers
        private void btnAddUpdateGeneralPaid_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            decimal totalAmount;
            string providerName = txtProviderName.Text.Trim();
            string ticketNumber = txtTicketNumber.Text.Trim();
            string totalAmountText = txtTotalAmount.Text.Trim();

            if (AreValidFields(providerName, totalAmountText, ticketNumber, out totalAmount) == false)
            {
                return;
            }

            if (_isUpdateGeneralPaid)
            {
                _generalPaidToUpdate.ProviderName = providerName;
                _generalPaidToUpdate.PurchaseDate = dtpPurchaseDate.SelectedDate.Value;
                _generalPaidToUpdate.TicketNumber = ticketNumber;
                _generalPaidToUpdate.PaidMethod = cbPaidMethod.SelectedValue.ToString();
                _generalPaidToUpdate.TotalAmount = totalAmount;
                
                UpdateGeneralPaid(_generalPaidToUpdate);
            }
            else
            {
                Model.GeneralPaid generalPaidToAdd = new Model.GeneralPaid()
                {
                    ProviderName = providerName,
                    PurchaseDate = dtpPurchaseDate.SelectedDate.Value,
                    TicketNumber = ticketNumber,
                    PaidMethod = cbPaidMethod.SelectedValue.ToString(),
                    TotalAmount = totalAmount
                };

                AddGeneralPaid(generalPaidToAdd);
            }
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Window's logic
        private void UpdateGeneralPaid(Model.GeneralPaid generalPaidToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.GeneralPaid>(generalPaidToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el pago", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddGeneralPaid(Model.GeneralPaid generalPaidToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.GeneralPaid>(generalPaidToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el pago", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del pago";
            btnAddUpdateGeneralPaid.Content = "Actualizar";

            txtProviderName.Text = _generalPaidToUpdate.ProviderName;
            dtpPurchaseDate.SelectedDate = _generalPaidToUpdate.PurchaseDate;
            txtTicketNumber.Text = _generalPaidToUpdate.TicketNumber;
            txtTotalAmount.Text = _generalPaidToUpdate.TotalAmount.ToString();
            cbPaidMethod.SelectedValue = _generalPaidToUpdate.PaidMethod;
        }

        private bool AreValidFields(string providerName, string totalAmountText, string ticketNumber, out decimal totalAmount)
        {
            totalAmount = 0;

            if (string.IsNullOrEmpty(providerName))
            {
                MessageBox.Show("Indique un nombre para el proveedor", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (dtpPurchaseDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha de compra válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(ticketNumber))
            {
                MessageBox.Show("Indique un número de ticket", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (cbPaidMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un método de pago", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (decimal.TryParse(totalAmountText, out totalAmount) == false)
            {
                MessageBox.Show("Cantidad total inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
        #endregion
	}
}