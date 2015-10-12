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
	/// Interaction logic for AddEditDotationsModal.xaml
	/// </summary>
	public partial class AddEditDotationsModal : Window
	{
        #region Instance variables
        private Model.Dotation _dotationToUpdate;
        private bool _isUpdateDotation;
        #endregion

        #region Constructors
        public AddEditDotationsModal(Model.Dotation dotationToUpdate)
		{
			this.InitializeComponent();

            _dotationToUpdate = dotationToUpdate;
            _isUpdateDotation = dotationToUpdate != null;

            if (_isUpdateDotation)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddUpdateDotation_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string dotationAmountText = txtDotationAmount.Text.Trim();
            decimal dotationAmount;

            if (AreValidFields(dotationAmountText, out dotationAmount) == false)
            {
                return;
            }

            if (_isUpdateDotation)
            {
                _dotationToUpdate.DotationDate = DateTime.Now;
                _dotationToUpdate.Amount = dotationAmount;

                UpdateDotation(_dotationToUpdate);
            }
            else
            {
                Model.Dotation dotationToAdd = new Model.Dotation()
                {
                    DotationDate = DateTime.Now,
                    Amount = dotationAmount,
                    UserId = null,
                    SignedDate = null
                };

                AddDotation(dotationToAdd);
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
            this.Title = "Actualizar información de la dotación";
            lblDotationDate.ToolTip = lblDotationDate.Content = _dotationToUpdate.DotationDate.ToString("D") + _dotationToUpdate.DotationDate.ToString(", HH:mm:ss") + " hrs";
            txtDotationAmount.Text = _dotationToUpdate.Amount.ToString();
            btnAddUpdateDotation.Content = "Actualizar";
        }

        private void AddDotation(Model.Dotation dotationToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Dotation>(dotationToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar la dotación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateDotation(Model.Dotation dotationToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Dotation>(dotationToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la dotación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AreValidFields(string dotationAmountText, out decimal dotationAmount)
        {
            if (decimal.TryParse(dotationAmountText, out dotationAmount) == false)
            {
                MessageBox.Show("Cantidad inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}