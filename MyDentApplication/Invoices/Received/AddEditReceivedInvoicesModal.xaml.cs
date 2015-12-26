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
	/// Interaction logic for AddEditInvoicesModal.xaml
	/// </summary>
	public partial class AddEditReceivedInvoicesModal : Window
	{
        #region Instance variables
        private Model.ReceivedInvoice _invoiceToUpdate;
        private bool _isUpdateInvoice;
        #endregion

        #region Constructors
        public AddEditReceivedInvoicesModal(Model.ReceivedInvoice invoiceToUpdate)
		{
			this.InitializeComponent();

            _invoiceToUpdate = invoiceToUpdate;
            _isUpdateInvoice = invoiceToUpdate != null;
            dtpPurchaseDate.SelectedDate = dtpInvoiceDate.SelectedDate = DateTime.Now;
            dtpInvoiceDate.SelectedDate = null;
            FillProviderComboBox();

            if (_isUpdateInvoice)
            {
                PrepareWindowForUpdates();
            }
        }
        #endregion

        #region Window event handlers
        private void btnAddUpdateInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int providerId;
            decimal totalAmount;
            string folio = txtFolio.Text.Trim();
            string totalAmountText = txtTotalAmount.Text.Trim();

            if (AreValidFields(folio, totalAmountText, out providerId, out totalAmount) == false)
            {
                return;
            }

            if (_isUpdateInvoice)
            {
                _invoiceToUpdate.ProviderId = providerId;
                _invoiceToUpdate.InvoiceDate = dtpInvoiceDate.SelectedDate;
                _invoiceToUpdate.PurchaseDate = dtpPurchaseDate.SelectedDate.Value;
                _invoiceToUpdate.Folio = folio;
                _invoiceToUpdate.PaidMethod = cbPaidMethod.SelectedValue.ToString();
                _invoiceToUpdate.TotalAmount = totalAmount;
                _invoiceToUpdate.IsPaid = chkIsPaid.IsChecked.Value;
                
                UpdateInvoice(_invoiceToUpdate);
            }
            else
            {
                Model.ReceivedInvoice invoiceToAdd = new Model.ReceivedInvoice()
                {
                    ProviderId = providerId,
                    InvoiceDate = dtpInvoiceDate.SelectedDate,
                    PurchaseDate = dtpPurchaseDate.SelectedDate.Value,
                    Folio = folio,
                    PaidMethod = cbPaidMethod.SelectedValue.ToString(),
                    TotalAmount = Convert.ToDecimal(totalAmount),
                    IsDeleted = false,
                    IsPaid = chkIsPaid.IsChecked.Value                    
                };

                AddInvoice(invoiceToAdd);
            }
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void chkIsInvoiced_CheckedUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (chkIsInvoiced.IsChecked.Value)
            {
                dtpInvoiceDate.IsEnabled = true;
                dtpInvoiceDate.SelectedDate = _isUpdateInvoice && _invoiceToUpdate.InvoiceDate != null
                                                ? _invoiceToUpdate.InvoiceDate
                                                : DateTime.Now;
            }
            else
            {
                dtpInvoiceDate.IsEnabled = false;
                dtpInvoiceDate.SelectedDate = null;
            }
        }
        #endregion

        #region Window's logic
        private void UpdateInvoice(Model.ReceivedInvoice invoiceToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.ReceivedInvoice>(invoiceToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la factura", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddInvoice(Model.ReceivedInvoice invoiceToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.ReceivedInvoice>(invoiceToAdd))
            {
                if (invoiceToAdd.IsPaid == false)
                {
                    string reminderMessage = string.Format(
                                          "Revise que la compra realizada a '{0}' el día {1} haya sido pagada."+
                                          "\nFactura con número de folio '{2}' por la cantidad de '{3}'.",
                                            invoiceToAdd.ResourceProvider.Name,
                                            invoiceToAdd.PurchaseDate.ToString("D"),
                                            invoiceToAdd.Folio,
                                            invoiceToAdd.TotalAmount);

                    Model.Reminder reminderToAdd = new Model.Reminder()
                    {
                        Message = reminderMessage,
                        AppearDate = DateTime.Now.AddDays(15.0),
                        CreatedDate = DateTime.Now,
                        RequireAdmin = true,
                        Seen = false,
                        SeenBy = null,
                        AutoGenerated = true
                    };

                    if (Controllers.BusinessController.Instance.Add<Model.Reminder>(reminderToAdd) == false)
                    {
                        MessageBox.Show("No se pudo generar un recordatorio para esta factura", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }   
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar la factura", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información de la factura";
            btnAddUpdateInvoice.Content = "Actualizar";
            
            dtpPurchaseDate.SelectedDate = _invoiceToUpdate.PurchaseDate;
            txtFolio.Text = _invoiceToUpdate.Folio;
            txtTotalAmount.Text = _invoiceToUpdate.TotalAmount.ToString();
            cbPaidMethod.SelectedValue = _invoiceToUpdate.PaidMethod;
            chkIsPaid.IsChecked = _invoiceToUpdate.IsPaid;
            chkIsInvoiced.IsChecked = _invoiceToUpdate.InvoiceDate != null;
            dtpInvoiceDate.SelectedDate = _invoiceToUpdate.InvoiceDate;

            //Select Paid method
            for (int i = 0; i < cbPaidMethod.Items.Count; i++)
            {
                if ((cbPaidMethod.Items[i] as string) == _invoiceToUpdate.PaidMethod)
                {
                    cbPaidMethod.SelectedIndex = i;
                    break;
                }
            }

            //Select provider
            for (int i = 0; i < cbProviders.Items.Count; i++)
            {
                if ((cbProviders.Items[i] as Controllers.ComboBoxItem).Text == _invoiceToUpdate.ResourceProvider.Name)
                {
                    cbProviders.SelectedIndex = i;
                    break;
                }
            }
        }

        private void FillProviderComboBox()
        {
            List<Model.ResourceProvider> providers = BusinessController.Instance.FindBy<Model.ResourceProvider>(p => p.IsDeleted == false)
                                                        .OrderBy(p => p.Name)
                                                        .ToList();

            foreach (Model.ResourceProvider provider in providers)
            {
                cbProviders.Items.Add(new Controllers.ComboBoxItem() { Text = provider.Name, Value = provider });
            }
        }

        private bool AreValidFields(string folio, string totalAmountText, out int providerId, out decimal totalAmount)
        {
            providerId = -1;
            totalAmount = 0;

            if (cbProviders.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un proveedor", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (chkIsInvoiced.IsChecked.Value && dtpInvoiceDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha de facturación válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (dtpPurchaseDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha de compra válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (cbPaidMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un método de pago", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }


            if (decimal.TryParse(totalAmountText, out totalAmount) == false || totalAmount < 0m)
            {
                MessageBox.Show("Cantidad total inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            providerId = ((cbProviders.SelectedItem as Controllers.ComboBoxItem).Value as Model.ResourceProvider).ProviderId;

            return true;
        }
        #endregion
    }
}