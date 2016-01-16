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
	/// Interaction logic for AddEditOutgoingInvoicesModal.xaml
	/// </summary>
	public partial class AddEditOutgoingInvoicesModal : Window
	{
		#region Instance variables
        private Model.OutgoingInvoice _invoiceToUpdate;
        private bool _isUpdateInvoice;
        #endregion

        #region Constructors
        public AddEditOutgoingInvoicesModal(Model.OutgoingInvoice invoiceToUpdate)
		{
			this.InitializeComponent();

            _invoiceToUpdate = invoiceToUpdate;
            _isUpdateInvoice = invoiceToUpdate != null;
            dtpPaidDate.SelectedDate = dtpInvoiceDate.SelectedDate = DateTime.Now;
            FillPatients();

            if (_isUpdateInvoice)
            {
                PrepareWindowForUpdates();
            }
        }
        #endregion

        #region Window event handlers
        private void btnAddUpdateInvoice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int patientId;
            decimal totalAmount;
            string folio = txtFolio.Text.Trim();
            string totalAmountText = txtTotalAmount.Text.Trim();

            if (AreValidFields(totalAmountText, out patientId, out totalAmount) == false)
            {
                return;
            }

            if (_isUpdateInvoice)
            {
                _invoiceToUpdate.PatientId = patientId;
                _invoiceToUpdate.InvoiceDate = dtpInvoiceDate.SelectedDate;
                _invoiceToUpdate.PaidDate = dtpPaidDate.SelectedDate.Value;
                _invoiceToUpdate.Folio = folio;
                _invoiceToUpdate.PaidMethod = cbPaidMethod.SelectedValue.ToString();
                _invoiceToUpdate.TotalAmount = totalAmount;
                
                UpdateInvoice(_invoiceToUpdate);
            }
            else
            {
                Model.OutgoingInvoice invoiceToAdd = new Model.OutgoingInvoice()
                {
                    PatientId = patientId,
                    InvoiceDate = dtpInvoiceDate.SelectedDate,
                    PaidDate = dtpPaidDate.SelectedDate.Value,
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
        private void UpdateInvoice(Model.OutgoingInvoice invoiceToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.OutgoingInvoice>(invoiceToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la factura", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddInvoice(Model.OutgoingInvoice invoiceToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.OutgoingInvoice>(invoiceToAdd))
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
            
            dtpPaidDate.SelectedDate = _invoiceToUpdate.PaidDate;
            txtFolio.Text = _invoiceToUpdate.Folio;
            txtTotalAmount.Text = _invoiceToUpdate.TotalAmount.ToString();
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
            for (int i = 0; i < cbPatients.Items.Count; i++)
            {
                if ((cbPatients.Items[i] as Controllers.ComboBoxItem).Text == (string.Format("(Exp. No. {0}) {1} {2}", _invoiceToUpdate.Patient.AssignedId, _invoiceToUpdate.Patient.FirstName, _invoiceToUpdate.Patient.LastName)))
                {
                    cbPatients.SelectedIndex = i;
                    break;
                }
            }
        }

        private void FillPatients()
        {
            List<Model.Patient> patients = BusinessController.Instance.FindBy<Model.Patient>(p => p.IsDeleted == false)
                                                .OrderBy(p => p.FirstName)
                                                .ThenBy(p => p.LastName)
                                                .ThenBy(p => p.AssignedId)
                                                .ToList();

            foreach (Model.Patient patient in patients)
            {
                cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = string.Format("(Exp. No. {0}) {1} {2}", patient.AssignedId, patient.FirstName, patient.LastName), Value = patient });
            }
        }

        private bool AreValidFields(string totalAmountText, out int patientId, out decimal totalAmount)
        {
            patientId = -1;
            totalAmount = 0;

            if (cbPatients.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (chkIsInvoiced.IsChecked.Value && dtpInvoiceDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha de facturación válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (dtpPaidDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha de pago válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
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

            patientId = ((cbPatients.SelectedItem as Controllers.ComboBoxItem).Value as Model.Patient).PatientId;

            return true;
        }
        #endregion
	}
}