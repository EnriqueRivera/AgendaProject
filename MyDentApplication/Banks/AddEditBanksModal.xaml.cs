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
	/// Interaction logic for AddEditBanksModal.xaml
	/// </summary>
	public partial class AddEditBanksModal : Window
	{
        #region Instance variables
        private Model.Bank _bankToUpdate;
        private bool _isUpdateBank;
        #endregion

        #region Constructors
        public AddEditBanksModal(Model.Bank bankToUpdate)
		{
			this.InitializeComponent();

            _bankToUpdate = bankToUpdate;
            _isUpdateBank = _bankToUpdate != null;

            if (_isUpdateBank)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddUpdateBanks_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string bankName = txtBankName.Text.Trim();
            if (string.IsNullOrEmpty(bankName))
            {
                MessageBox.Show("Ingrese un nombre para el banco", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_isUpdateBank)
            {
                _bankToUpdate.Name = bankName;

                UpdateBank(_bankToUpdate);
            }
            else
            {
                Model.Bank bankToAdd = new Model.Bank()
                {
                    Name = bankName,
                    IsDeleted = false
                };

                AddBank(bankToAdd);
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
        }
        #endregion

        #region Window's logic
        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del banco";
            btnAddUpdateBanks.Content = "Actualizar";
            txtBankId.Text = _bankToUpdate.BankId.ToString();
            txtBankName.Text = _bankToUpdate.Name;
        }

        private void AddBank(Model.Bank bankToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Bank>(bankToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el banco", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateBank(Model.Bank bankToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Bank>(bankToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el banco", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}