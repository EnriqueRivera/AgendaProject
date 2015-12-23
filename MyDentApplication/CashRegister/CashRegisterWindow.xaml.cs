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
	/// Interaction logic for CashRegisterWindow.xaml
	/// </summary>
	public partial class CashRegisterWindow : Window
	{
        #region Instance variables
        private Model.User _userLoggedIn;
        private Model.Patient _selectedPatient;
        private Model.Statement _statement;
        private Model.PaymentFolio _paymentFolioGenerated;
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
                this.Title = "Abonar a estado de cuenta";
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
            this.Close();
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (_selectedPatient == null)
                {
                    MessageBox.Show("Seleccione un paciente para poder guardar", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                List<PaymentControl> paymentsToSave = GetPaymentsToSave();
                List<TreatmentPriceControl> treatmentsToSave = GetTreatmentsToSave();

                if (_statement == null)
                {
                    if (treatmentsToSave.Count == 0)
                    {
                        MessageBox.Show("Agregue al menos un tratamiento para poder guardar", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (_grandTotal == 0m)
                    {
                        if (_positiveBalance > 0m
                            && MessageBox.Show("Existe un saldo a favor\n¿Desea que se guarde para el paciente seleccionado?",
                                            "Advertencia",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Warning
                                        ) == MessageBoxResult.No)
                        {
                            return;
                        }

                        SavePayments(paymentsToSave);
                        SaveTreatments(treatmentsToSave);
                        CreatePaymentFolio(paymentsToSave, treatmentsToSave);
                        SavePositiveBalance();
                        PrepareWindowToPrintFolio();

                        MessageBox.Show("Datos guardados\n\nNúmero de folio generado: " + _paymentFolioGenerated.FolioNumber, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        Model.Statement currentStatement = _selectedPatient.Statements
                                                            .Where(s => s.IsPaid == false)
                                                            .FirstOrDefault();

                        if (currentStatement == null)
                        {
                            currentStatement = new Model.Statement();

                            if (MessageBox.Show("No se ha saldado por completo el monto de los tratamientos\n¿Desea abrir un estado de cuenta para este paciente?",
                                                    "Advertencia",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Warning
                                                ) == MessageBoxResult.Yes)
                            {
                                new StatementExpirationDateModal(currentStatement, _selectedPatient, _userLoggedIn).ShowDialog();

                                if (currentStatement.PatientId != 0)
                                {
                                    SavePayments(paymentsToSave);
                                    SaveTreatments(treatmentsToSave);
                                    CreateStatement(currentStatement, paymentsToSave, treatmentsToSave);
                                    CreatePaymentFolio(paymentsToSave, treatmentsToSave);
                                    PrepareWindowToPrintFolio();

                                    MessageBox.Show("Datos guardados\n\nNúmero de folio generado: " + _paymentFolioGenerated.FolioNumber, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                        }
                        else if (currentStatement.ExpirationDate < DateTime.Now.Date)
                        {
                            MessageBox.Show("Este paciente posee un estado de cuenta que ha expirado (Estado de cuenta número: " + currentStatement.StatementId + ")" +
                                            "\nEl monto faltante no puede ser agregado al estado de cuenta, por tal motivo tiene que liquidar estos tratamientos en este momento.", 
                                            "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            if (MessageBox.Show("Este paciente posee un estado de cuenta con número: " + currentStatement.StatementId + 
                                                "\n¿Desea guardar el monto faltante en el estado de cuenta del paciente?",
                                                    "Advertencia",
                                                    MessageBoxButton.YesNo,
                                                    MessageBoxImage.Warning
                                                ) == MessageBoxResult.Yes)
                            {
                                SavePayments(paymentsToSave);
                                SaveTreatments(treatmentsToSave);
                                UpdateStatement(currentStatement, paymentsToSave, treatmentsToSave);
                                CreatePaymentFolio(paymentsToSave, treatmentsToSave);
                                PrepareWindowToPrintFolio();

                                MessageBox.Show("Datos guardados\n\nNúmero de folio generado: " + _paymentFolioGenerated.FolioNumber, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }
                else
                {
                    if (paymentsToSave.Count == 0)
                    {
                        MessageBox.Show("Agregue al menos un pago para poder guardar", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (_grandTotal == 0m)
                    {
                        if (_positiveBalance > 0m
                            && MessageBox.Show("Existe un saldo a favor\n¿Desea que se guarde para el paciente seleccionado?",
                                            "Advertencia",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Warning
                                        ) == MessageBoxResult.No)
                        {
                            return;
                        }

                        SavePayments(paymentsToSave);
                        SaveTreatments(treatmentsToSave);
                        _statement.IsPaid = true;
                        UpdateStatement(_statement, paymentsToSave, treatmentsToSave);
                        CreatePaymentFolio(paymentsToSave, treatmentsToSave);
                        SavePositiveBalance();
                        PrepareWindowToPrintFolio();

                        MessageBox.Show("Datos guardados\n\nEl estado de cuenta fue marcado como liquidado.\nNúmero de folio generado: " + _paymentFolioGenerated.FolioNumber, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        SavePayments(paymentsToSave);
                        SaveTreatments(treatmentsToSave);
                        UpdateStatement(_statement, paymentsToSave, treatmentsToSave);
                        CreatePaymentFolio(paymentsToSave, treatmentsToSave);
                        PrepareWindowToPrintFolio();

                        MessageBox.Show("Datos guardados\n\nNúmero de folio generado: " + _paymentFolioGenerated.FolioNumber, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de guardar la información de la caja.\nDetalle del error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                return;
            }
            else if (_statement != null && _statement.ExpirationDate < DateTime.Now.Date)
            {
                MessageBox.Show("No puede agregar tratamientos a un estado de cuenta vencido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);    
            }
            else
            {
                TreatmentPriceControl treatmentControl = new TreatmentPriceControl(_selectedPatient);
                treatmentControl.OnTreatmentDeleted += treatmentControl_OnTreatmentDeleted;
                treatmentControl.OnTreatmentEdited += treatmentControl_OnTreatmentEdited;

                new AddEditTreatmentPaymentModal(null, _selectedPatient, treatmentControl).ShowDialog();

                if (treatmentControl.TreatmentPayment != null)
                {
                    spTreatments.Children.Add(treatmentControl);
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
            UpdateTotals();
        }

        private void btnPrintMail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }
        #endregion

        #region Window's logic
        private void CreateStatement(Model.Statement currentStatement, List<PaymentControl> paymentsToSave, List<TreatmentPriceControl> treatmentsToSave)
        {
            AddPaymentsAndTreatmentsToStatement(currentStatement, paymentsToSave, treatmentsToSave);
            if (BusinessController.Instance.Add<Model.Statement>(currentStatement) == false)
            {
                throw new Exception("No se pudo crear el estado de cuenta");
            }
        }

        private void UpdateStatement(Model.Statement currentStatement, List<PaymentControl> paymentsToSave, List<TreatmentPriceControl> treatmentsToSave)
        {
            AddPaymentsAndTreatmentsToStatement(currentStatement, paymentsToSave, treatmentsToSave);
            if (BusinessController.Instance.Update<Model.Statement>(currentStatement) == false)
            {
                throw new Exception("No se pudo actualizar el estado de cuenta");
            }
        }

        private void AddPaymentsAndTreatmentsToStatement(Model.Statement currentStatement, List<PaymentControl> paymentsToSave, List<TreatmentPriceControl> treatmentsToSave)
        {
            foreach (var item in paymentsToSave)
            {
                currentStatement.Payments.Add(item.Payment);
            }

            foreach (var item in treatmentsToSave)
            {
                currentStatement.TreatmentPayments.Add(item.TreatmentPayment);
            }
        }

        private void PrepareWindowToPrintFolio()
        {
            btnSave.IsEnabled = false;
            cbPatients.IsEnabled = false;
            btnAddTreatment.IsEnabled = false;
            btnAddCashPayment.IsEnabled = false;
            btnAddCreditCardPayment.IsEnabled = false;
            btnAddCheckPayment.IsEnabled = false;
            btnPrintMail.IsEnabled = true;
            lblPaymentFolioNumber.ToolTip = lblPaymentFolioNumber.Text = _paymentFolioGenerated == null ? string.Empty : _paymentFolioGenerated.FolioNumber.ToString();
        }

        private void CreatePaymentFolio(List<PaymentControl> paymentsToSave, List<TreatmentPriceControl> treatmentsToSave)
        {
            _paymentFolioGenerated = new Model.PaymentFolio()
            {
                FolioDate = DateTime.Now,
                UserId = _userLoggedIn.UserId,
                PatientId = _selectedPatient.PatientId
            };

            foreach (var item in paymentsToSave)
            {
                _paymentFolioGenerated.Payments.Add(item.Payment);
            }

            foreach (var item in treatmentsToSave)
            {
                _paymentFolioGenerated.TreatmentPayments.Add(item.TreatmentPayment);
            }

            if (BusinessController.Instance.Add<Model.PaymentFolio>(_paymentFolioGenerated) == false)
            {
                throw new Exception("No se pudo crear el folio");
            }
        }

        private void SavePositiveBalance()
        {
            if (_positiveBalance > 0m)
            {
                Model.PositiveBalance positiveBalanceToAdd = new Model.PositiveBalance()
                {
                    Amount = _positiveBalance,
                    PositiveBalanceDate = DateTime.Now,
                    AppliedDate = null,
                    PatientId = _selectedPatient.PatientId
                };

                if (BusinessController.Instance.Add<Model.PositiveBalance>(positiveBalanceToAdd) == false)
                {
                    throw new Exception("No se pudo guardar el saldo a favor");
                }   
            }
        }

        private void SavePayments(List<PaymentControl> paymentsToSave)
        {
            foreach (var item in paymentsToSave)
            {
                BusinessController.Instance.Add<Model.Payment>(item.Payment);
                item.UpdateData();

                if (item.PositiveBalance != null)
                {
                    item.PositiveBalance.AppliedDate = DateTime.Now;
                    BusinessController.Instance.Update<Model.PositiveBalance>(item.PositiveBalance);
                }
            }
        }

        private void SaveTreatments(List<TreatmentPriceControl> treatmentsToSave)
        {
            foreach (var item in treatmentsToSave)
            {
                BusinessController.Instance.Add<Model.TreatmentPayment>(item.TreatmentPayment);
                item.UpdateData();
            }
        }

        private List<PaymentControl> GetPaymentsToSave()
        {
            List<PaymentControl> paymentsToSave = new List<PaymentControl>();
            foreach (var item in spPayments.Children)
            {
                PaymentControl paymentControl = (item as PaymentControl);
                if (paymentControl.IsNewPayment())
                {
                    paymentsToSave.Add(paymentControl);
                }
            }

            return paymentsToSave;
        }

        private List<TreatmentPriceControl> GetTreatmentsToSave()
        {
            List<TreatmentPriceControl> treatmentsToSave = new List<TreatmentPriceControl>();
            foreach (var item in spTreatments.Children)
            {
                TreatmentPriceControl treatmentControl = (item as TreatmentPriceControl);
                if (treatmentControl.IsNewTreatment())
                {
                    treatmentsToSave.Add(treatmentControl);
                }
            }

            return treatmentsToSave;
        }

        private void UpdatePositiveBalances()
        {
            RemoveAllPositiveBalances();

            if (_selectedPatient != null)
            {
                List<Model.PositiveBalance> activePositiveBalances = _selectedPatient.PositiveBalances
                                                                        .Where(pb => pb.AppliedDate == null)
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
                Model.TreatmentPayment treatment = (item as TreatmentPriceControl).TreatmentPayment;
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
            List<Model.Patient> patients = BusinessController.Instance.FindBy<Model.Patient>(p => p.IsDeleted == false)
                                            .OrderBy(p => p.PatientId)
                                            .ThenBy(p => p.FirstName)
                                            .ThenBy(p => p.LastName)
                                            .ToList();

            foreach (Model.Patient patient in patients)
            {
                cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = string.Format("(Exp. No. {0}) {1} {2}", patient.PatientId, patient.FirstName, patient.LastName), Value = patient });
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (btnSave.IsEnabled == false)
	        {
                if (MessageBox.Show("¿Seguro(a) que desea cerrar esta ventana?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
	        }
            else if (MessageBox.Show("Los cambios no guardados se perderán\n¿Está seguro(a) que desea salir de la caja?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion
    }
}