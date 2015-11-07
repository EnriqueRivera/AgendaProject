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
	/// Interaction logic for AddEditAmericanExpressPaidsModal.xaml
	/// </summary>
	public partial class AddEditAmericanExpressPaidsModal : Window
	{
        #region Instance variables
        private Model.AmericanExpressPaid _paidToUpdate;
        private bool _isUpdatePaid;
        #endregion

        #region Constructors
        public AddEditAmericanExpressPaidsModal(Model.AmericanExpressPaid paidToUpdate)
		{
			this.InitializeComponent();

            _paidToUpdate = paidToUpdate;
            _isUpdatePaid = _paidToUpdate != null;
            dtpPaidDate.SelectedDate = DateTime.Now;

            if (_isUpdatePaid)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddUpdatePaid_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string establishment = txtEstablishment.Text.Trim();
            string concept = txtConcept.Text.Trim();
            string totalAmountText = txtTotalAmount.Text.Trim();
            decimal totalAmount;

            if (AreValidFields(totalAmountText, establishment, concept, out totalAmount) == false)
            {
                return;
            }

            if (_isUpdatePaid)
            {
                _paidToUpdate.Total = totalAmount;
                _paidToUpdate.PaidDate = dtpPaidDate.SelectedDate.Value;
                _paidToUpdate.Establishment = establishment;
                _paidToUpdate.Concept = concept;

                UpdatePaid(_paidToUpdate);
            }
            else
            {
                Model.AmericanExpressPaid paidToAdd = new Model.AmericanExpressPaid()
                {
                    Total = totalAmount,
                    PaidDate = dtpPaidDate.SelectedDate.Value,
                    Establishment = establishment,
                    Concept = concept
                };

                AddPaid(paidToAdd);
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}
        #endregion

        #region Window's logic
        private void AddPaid(Model.AmericanExpressPaid paidToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.AmericanExpressPaid>(paidToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el pago", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdatePaid(Model.AmericanExpressPaid paidToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.AmericanExpressPaid>(paidToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el pago", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AreValidFields(string totalAmountText, string establishment, string concept, out decimal totalAmount)
        {
            totalAmount = 0;
            
            if (dtpPaidDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha de pago válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(establishment))
            {
                MessageBox.Show("Ingrese un establecimiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(concept))
            {
                MessageBox.Show("Ingrese un concepto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (decimal.TryParse(totalAmountText, out totalAmount) == false)
            {
                MessageBox.Show("Cantidad total inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar pago American Express";
            btnAddUpdatePaid.Content = "Actualizar";

            dtpPaidDate.SelectedDate = _paidToUpdate.PaidDate;
            txtEstablishment.Text = _paidToUpdate.Establishment;
            txtConcept.Text = _paidToUpdate.Concept;
            txtTotalAmount.Text = _paidToUpdate.Total.ToString();
        }
        #endregion
    }
}