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
	/// Interaction logic for CashRegisterWindow.xaml
	/// </summary>
	public partial class CashRegisterWindow : Window
	{
        #region Instance variables
        private Model.User _userLoggedIn;
        private Model.Patient _selectedPatient;
        private Model.Statement _statement;
        private decimal _totalAmountOfTreatments;
        private decimal _totalAmountOfPayments;
        private decimal _grandTotal;
        private decimal _positiveBalance;
        #endregion

        #region Constructors
        public CashRegisterWindow(Model.User userLoggedIn, Model.Statement statement)
		{
			this.InitializeComponent();

            FillPatients();

            _userLoggedIn = userLoggedIn;
            _statement = statement;

            if (_statement != null)
            {
                lblAccountStatusNumber.ToolTip = lblAccountStatusNumber.Content = "Estado de cuenta No. " + _statement.StatementId;
                cbPatients.IsEnabled = false;
                FillTreatments();
                FillPayments();
            }

            UpdateTotals();
        }
        #endregion

        #region Window event handlers
        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }

        private void btnAddPayment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PaymentControl paymentControl = new PaymentControl();
            paymentControl.OnPaymentDeleted += paymentControl_OnPaymentDeleted;
            paymentControl.OnPaymentEdited += paymentControl_OnPaymentEdited;

            new AddEditPaymentModal(null, (Controllers.PaymentType)Enum.Parse(typeof(Controllers.PaymentType), (sender as Button).Tag.ToString(), true), paymentControl).ShowDialog();

            if (paymentControl.Payment != null)
            {
                spPayments.Children.Add(paymentControl);
            }

            UpdateTotals();
        }

        void paymentControl_OnPaymentEdited(object sender, bool e)
        {
            UpdateTotals();
        }

        void paymentControl_OnPaymentDeleted(object sender, bool e)
        {
            spPayments.Children.Remove(sender as PaymentControl);
            UpdateTotals();
        }

        private void btnAddTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_selectedPatient == null)
            {
                MessageBox.Show("Seleccione un paciente para poder agregar un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                TreatmentPriceControl treatmentControl = new TreatmentPriceControl(_selectedPatient);
                treatmentControl.OnTreatmentDeleted += treatmentControl_OnTreatmentDeleted;
                treatmentControl.OnTreatmentEdited += treatmentControl_OnTreatmentEdited;

                new AddEditTreatmentPaymentModal(null, _selectedPatient);

                if (treatmentControl.Treatment != null)
                {
                    spPayments.Children.Add(treatmentControl);
                }

                UpdateTotals();
            }
            
        }

        void treatmentControl_OnTreatmentEdited(object sender, bool e)
        {
            UpdateTotals();
        }

        void treatmentControl_OnTreatmentDeleted(object sender, bool e)
        {
            spTreatments.Children.Remove(sender as TreatmentPriceControl);
            UpdateTotals();
        }

        private void cbPatients_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedPatient = (cbPatients.SelectedValue as Controllers.ComboBoxItem).Value as Model.Patient;

            FillPatientFields();
            UpdatePositiveBalances();
            //UpdateTreatments - pueden cambiar de uno que no tiene seguro medico a uno que si
        }
        #endregion

        #region Window's logic
        private void UpdatePositiveBalances()
        {
            RemoveAllPositiveBalances();

            if (_selectedPatient != null)
            {
                List<Model.PositiveBalance> activePositiveBalances = _selectedPatient.PositiveBalances
                                                                        .Where(pb => pb.AppliedDate != null)
                                                                        .ToList();
                foreach (var item in activePositiveBalances)
                {
                    Model.Payment paymentToAdd = new Model.Payment()
                    {
                        PaymentDate = DateTime.Now,
                        Amount = item.Amount,
                        Type = PaymentType.Efectivo.ToString(),
                        Observation = "Saldo a favor"
                    };

                    PaymentControl paymentControl = new PaymentControl(paymentToAdd, item)
                    {
                        Width = Double.NaN
                    };

                    spPayments.Children.Add(paymentControl);
                }
            }
        }

        private void RemoveAllPositiveBalances()
        {
            List<PaymentControl> paymentControls = new List<PaymentControl>();
            foreach (var item in spPayments.Children)
            {
                PaymentControl paymentControl = (item as PaymentControl);
                if (paymentControl.PositiveBalance != null)
                {
                    paymentControls.Add(paymentControl);
                }
            }

            foreach (var item in paymentControls)
            {
                spPayments.Children.Remove(item);
            }            
        }

        private void UpdateTotals()
        {
            _totalAmountOfTreatments = UpdateNumberOfTreatments();
            _totalAmountOfPayments = UpdateNumberOfPayments();
            _grandTotal = _totalAmountOfTreatments - _totalAmountOfPayments;
            _positiveBalance = 0m;

            if (_grandTotal < 0m)
	        {
                _positiveBalance = Math.Abs(_grandTotal);
		        _grandTotal = 0m;
	        }

            lblTotalAmountTreatments.ToolTip = lblTotalAmountTreatments.Text = "$" + _totalAmountOfTreatments.ToString("0.00");
            lblTotalAmountPayments.ToolTip = lblTotalAmountPayments.Text = "$" + _totalAmountOfPayments.ToString("0.00");
            lblGrandTotal.ToolTip = lblGrandTotal.Text = "$" + _grandTotal.ToString("0.00");
            lblPositiveBalance.ToolTip = lblPositiveBalance.Text = "$" + _positiveBalance.ToString("0.00");
        }

        private decimal UpdateNumberOfPayments()
        {
            int quantity = 0;
            decimal totalAmountOfPayments = 0m;

            foreach (var item in spPayments.Children)
            {
                Model.Payment payment = (item as PaymentControl).Payment;
                quantity++;
                totalAmountOfPayments += payment.Amount;
            }

            lblPaymentsCount.ToolTip = lblPaymentsCount.Content = "No. de pagos: " + spPayments.Children.Count;

            return totalAmountOfPayments;
        }

        private decimal UpdateNumberOfTreatments()
        {
            int quantity = 0;
            decimal totalAmountOfTreatments = 0m;

            foreach (var item in spTreatments.Children)
            {
                Model.TreatmentPayment treatment = (item as TreatmentPriceControl).Treatment;
                quantity += treatment.Quantity;
                totalAmountOfTreatments += treatment.Total;
            }

            lblTreatmentsCount.ToolTip = lblTreatmentsCount.Content = "No. de tratamientos: " + quantity;

            return totalAmountOfTreatments;
        }

        private void FillPatientFields()
        {
            if (_selectedPatient == null)
            {
                lblExpNo.ToolTip = lblExpNo.Text = string.Empty;
                lblPacientName.ToolTip = lblPacientName.Text = string.Empty;
                lblCellPhone.ToolTip = lblCellPhone.Text = string.Empty;
                lblHomePhone.ToolTip = lblHomePhone.Text = string.Empty;
                lblEmail.ToolTip = lblEmail.Text = string.Empty;
            }
            else
            {
                lblExpNo.ToolTip = lblExpNo.Text = _selectedPatient.PatientId.ToString();
                lblPacientName.ToolTip = lblPacientName.Text = _selectedPatient.FirstName + " " + _selectedPatient.LastName;
                lblCellPhone.ToolTip = lblCellPhone.Text = _selectedPatient.CellPhone;
                lblHomePhone.ToolTip = lblHomePhone.Text = _selectedPatient.HomePhone;
                lblEmail.ToolTip = lblEmail.Text = _selectedPatient.Email;
            }
        }

        private void FillPatients()
        {
            List<Model.Patient> patients = BusinessController.Instance.GetAll<Model.Patient>()
                                            .Where(p => p.IsDeleted == false)
                                            .OrderBy(p => p.FirstName)
                                            .ThenBy(p => p.LastName)
                                            .ToList();

            foreach (Model.Patient patient in patients)
            {
                cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = patient.FirstName + " " + patient.LastName, Value = patient });
            }
        }

        private void FillPayments()
        {
            foreach (Model.TreatmentPayment treatment in _statement.TreatmentPayments)
            {
                TreatmentPriceControl treatmentControl = new TreatmentPriceControl(treatment, _selectedPatient)
                {
                    Width = Double.NaN
                };

                spTreatments.Children.Add(treatmentControl);
            }
        }

        private void FillTreatments()
        {
            foreach (Model.Payment payment in _statement.Payments)
            {
                PaymentControl paymentControl = new PaymentControl(payment, null)
                {
                    Width = Double.NaN
                };

                spPayments.Children.Add(paymentControl);
            }
        }
        #endregion
    }
}