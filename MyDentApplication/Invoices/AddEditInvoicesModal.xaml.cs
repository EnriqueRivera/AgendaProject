﻿using Controllers;
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
	public partial class AddEditInvoicesModal : Window
	{
        #region Instance variables
        private Model.Invoice _invoiceToUpdate;
        private bool _isUpdateInvoice;
        #endregion

        #region Constructors
        public AddEditInvoicesModal(Model.Invoice invoiceToUpdate)
		{
			this.InitializeComponent();

            _invoiceToUpdate = invoiceToUpdate;
            _isUpdateInvoice = invoiceToUpdate != null;
            dtpPurchaseDate.SelectedDate = dtpInvoiceDate.SelectedDate = DateTime.Now;
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
                _invoiceToUpdate.InvoiceDate = dtpInvoiceDate.SelectedDate.Value;
                _invoiceToUpdate.PurchaseDate = dtpPurchaseDate.SelectedDate.Value;
                _invoiceToUpdate.Folio = folio;
                _invoiceToUpdate.PaidMethod = cbPaidMethod.SelectedValue.ToString();
                _invoiceToUpdate.TotalAmount = Convert.ToDecimal(totalAmount);

                UpdateInvoice(_invoiceToUpdate);
            }
            else
            {
                Model.Invoice invoiceToAdd = new Model.Invoice()
                {
                    ProviderId = providerId,
                    InvoiceDate = dtpInvoiceDate.SelectedDate.Value,
                    PurchaseDate = dtpPurchaseDate.SelectedDate.Value,
                    Folio = folio,
                    PaidMethod = cbPaidMethod.SelectedValue.ToString(),
                    TotalAmount = Convert.ToDecimal(totalAmount),
                    IsDeleted = false
                };

                AddInvoice(invoiceToAdd);
            }
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Window's logic
        private void UpdateInvoice(Model.Invoice invoiceToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Invoice>(invoiceToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la factura", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddInvoice(Model.Invoice invoiceToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Invoice>(invoiceToAdd))
            {
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
            dtpInvoiceDate.SelectedDate = _invoiceToUpdate.InvoiceDate;
            dtpPurchaseDate.SelectedDate = _invoiceToUpdate.PurchaseDate;
            txtFolio.Text = _invoiceToUpdate.Folio;
            txtTotalAmount.Text = _invoiceToUpdate.TotalAmount.ToString();
            cbPaidMethod.SelectedValue = _invoiceToUpdate.PaidMethod;

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
            List<Model.ResourceProvider> providers = BusinessController.Instance.GetAll<Model.ResourceProvider>()
                                                        .Where(p => p.IsDeleted == false)
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

            if (dtpInvoiceDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha de facturación válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (dtpPurchaseDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha de compra válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(folio))
            {
                MessageBox.Show("Ingrese un folio", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
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

            providerId = ((cbProviders.SelectedItem as Controllers.ComboBoxItem).Value as Model.ResourceProvider).ProviderId;

            return true;
        }
        #endregion
    }
}