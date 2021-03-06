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
	/// Interaction logic for AddEditPaymentModal.xaml
	/// </summary>
	public partial class AddEditPaymentModal : Window
	{
        #region Instance variables
        private Model.Payment _paymentToUpdate;
        private Model.Bank _bank;
        private bool _isUpdatePayment;
        private PaymentType _paymentType;
        private PaymentControl _paymentControl;
        #endregion

        #region Constructors
        public AddEditPaymentModal(Model.Payment paymentToUpdate, PaymentType paymentType, PaymentControl paymentControl, Model.Bank bank)
		{
			this.InitializeComponent();

            _bank = bank;
            _paymentControl = paymentControl;
            _paymentType = paymentType;
            _paymentToUpdate = paymentToUpdate;
            _isUpdatePayment = _paymentToUpdate != null;

            FillBanks();
            AdjustWindowForPaymentType();

            if (_isUpdatePayment)
            {
                PrepareWindowForUpdates();
            }

            this.Title += " (" + paymentType.ToString() + ")";
        }
        #endregion

        #region Window event handlers
        private void btnAddPayment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string amountText = txtAmount.Text.Trim();
            Model.Bank bank = (cbBanks.SelectedItem as Controllers.ComboBoxItem).Value as Model.Bank;
            string voucherCheckNumber = txtVoucherCheckNumber.Text.Trim();
            decimal amount;

            if (AreValidFields(amountText, bank, voucherCheckNumber, out amount) == false)
            {
                return;
            }

            if (_isUpdatePayment)
            {
                _paymentToUpdate.PaymentDate = DateTime.Now;
                _paymentToUpdate.Amount = amount;
                _paymentToUpdate.Type = _paymentType.ToString();
                _paymentToUpdate.Observation = txtObservations.Text.Trim();

                if (_paymentType != PaymentType.Efectivo)
                {
                    _paymentToUpdate.BankId = bank.BankId;
                    _paymentToUpdate.VoucherCheckNumber = voucherCheckNumber;
                }

                _paymentControl.Payment = _paymentToUpdate;
            }
            else
            {
                Model.Payment paymentToAdd = new Model.Payment()
                {
                    PaymentDate = DateTime.Now,
                    Amount = amount,
                    Type = _paymentType.ToString(),
                    Observation = txtObservations.Text.Trim()
                };

                if (_paymentType != PaymentType.Efectivo)
                {
                    paymentToAdd.BankId = bank.BankId;
                    paymentToAdd.VoucherCheckNumber = voucherCheckNumber;
                }

                _paymentControl.Payment = paymentToAdd;
            }

            _paymentControl.Width = Double.NaN;
            _paymentControl.Bank = bank;
            _paymentControl.UpdateData();

            this.Close();
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Window's logic
        private bool AreValidFields(string amountText, Model.Bank bank, string voucherCheckNumber, out decimal amount)
        {
            amount = 0;

            if (decimal.TryParse(amountText, out amount) == false)
	        {
		        MessageBox.Show("Cantidad inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
	        }
            else if (amount <= 0m)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (_paymentType != PaymentType.Efectivo)
            {
                if (bank == null)
                {
                    MessageBox.Show("Seleccione un banco", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }

                if (string.IsNullOrEmpty(voucherCheckNumber))
                {
                    MessageBox.Show("Ingrese un " + lblVoucherCheckNumber.Content.ToString().Replace(":", ""), "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            return true;
        }

        private void FillBanks()
        {
            List<Model.Bank> banks = BusinessController.Instance.FindBy<Model.Bank>(p => p.IsDeleted == false)
                                            .OrderBy(p => p.Name)
                                            .ToList();

            cbBanks.Items.Add(new Controllers.ComboBoxItem() { Text = string.Empty, Value = null });

            foreach (Model.Bank bank in banks)
            {
                cbBanks.Items.Add(new Controllers.ComboBoxItem() { Text = bank.Name, Value = bank });
            }

            cbBanks.SelectedIndex = 0;
        }

        private void AdjustWindowForPaymentType()
        {
            switch (_paymentType)
            {
                case PaymentType.Efectivo:
                    lblBank.Visibility = System.Windows.Visibility.Hidden;
                    cbBanks.Visibility = System.Windows.Visibility.Hidden;
                    lblVoucherCheckNumber.Visibility = System.Windows.Visibility.Hidden;
                    txtVoucherCheckNumber.Visibility = System.Windows.Visibility.Hidden;
                    this.Height = 245;
                    break;
                case PaymentType.Cheque:
                    lblVoucherCheckNumber.Content = "No. de cheque:";
                    break;
                case Controllers.PaymentType.Tarjeta:
                default:
                    break;
            }
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar pago";
            btnAddPayment.Content = "Actualizar";
            txtAmount.Text = _paymentToUpdate.Amount.ToString();
            txtVoucherCheckNumber.Text = _paymentToUpdate.VoucherCheckNumber;
            txtObservations.Text = _paymentToUpdate.Observation;

            if (_bank != null)
            {
                for (int i = 0; i < cbBanks.Items.Count; i++)
                {
                    if ((cbBanks.Items[i] as Controllers.ComboBoxItem).Text == _bank.Name)
                    {
                        cbBanks.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        #endregion
    }
}