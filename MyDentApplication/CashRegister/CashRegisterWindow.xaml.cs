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
using System.Net.Mail;
using System.Net;
using System.Threading;

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
        private Thread _sendEmailThread;
        private bool _isStatement;
        #endregion

        #region Delegates
        delegate void SendEmailDelegate(string errorMessage);
        #endregion

        #region Constructors
        public CashRegisterWindow(Model.User userLoggedIn, Model.Statement statement, Model.Patient patient)
		{
			this.InitializeComponent();

            FillPatients();

            _userLoggedIn = userLoggedIn;
            _statement = statement;
            _selectedPatient = patient;
            _isStatement = _statement != null;

            if (_statement != null)
            {
                this.Title = "Abonar/Liquidar estado de cuenta";
                lblAccountStatusNumber.ToolTip = lblAccountStatusNumber.Content = _statement.StatementId.ToString();
                gbAccountStatusNumber.Visibility = System.Windows.Visibility.Visible;
                btnClearForm.Visibility = System.Windows.Visibility.Hidden;
                cbPatients.IsEnabled = false;
                FillTreatments();
                FillPayments();

                SelectPatient();
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
            FinishTransaction();
        }

        private void ShowStatementNumberGenerated()
        {
            lblGeneratedStatementNumberCaption.Visibility = System.Windows.Visibility.Visible;
            lblGeneratedStatementNumber.ToolTip = lblGeneratedStatementNumber.Text = _statement.StatementId.ToString();
        }

        private void btnAddPayment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_selectedPatient == null)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                PaymentControl paymentControl = new PaymentControl();
                paymentControl.OnPaymentDeleted += paymentControl_OnPaymentDeleted;
                paymentControl.OnPaymentEdited += paymentControl_OnPaymentEdited;

                new AddEditPaymentModal(null, (Controllers.PaymentType)Enum.Parse(typeof(Controllers.PaymentType), (sender as Button).Tag.ToString(), true), paymentControl, null).ShowDialog();

                if (paymentControl.Payment != null)
                {
                    spPayments.Children.Insert(0, paymentControl);
                    UpdateTotals();

                    CheckIfTransactionCanBeFinished();
                }
            }
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
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);    
            }
            else if (_statement != null && _statement.ExpirationDate < DateTime.Now.Date)
            {
                MessageBox.Show("No puede agregar tratamientos a un estado de cuenta vencido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);    
            }
            else
            {
                TreatmentPriceControl treatmentControl = new TreatmentPriceControl();
                treatmentControl.OnTreatmentDeleted += treatmentControl_OnTreatmentDeleted;
                treatmentControl.OnTreatmentEdited += treatmentControl_OnTreatmentEdited;

                new AddEditTreatmentPaymentModal(null, treatmentControl).ShowDialog();

                if (treatmentControl.TreatmentPayment != null)
                {
                    spTreatments.Children.Insert(0, treatmentControl);
                    UpdateTotals();

                    CheckIfTransactionCanBeFinished();
                }
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
            _selectedPatient = cbPatients.SelectedValue == null ? null : (cbPatients.SelectedValue as Controllers.ComboBoxItem).Value as Model.Patient;

            FillPatientFields();
            UpdatePositiveBalances();
            UpdateTotals();
        }

        private void btnPrintMail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SendMail();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_sendEmailThread != null)
            {
                MessageBox.Show("No puede cerrar la ventana hasta que finalice el envío del correo"
                                , "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                e.Cancel = true;
            }
            else if (btnSave.IsEnabled == false)
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

        private void btnClearForm_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MessageBox.Show("Los cambios no guardados se perderán\n¿Está seguro(a) que desea limpiar la caja?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                ClearForm();
            }
        }
        #endregion

        #region Window's logic
        private void SendMail()
        {
            FillPatientFields();

            List<Model.Configuration> emailConfigurations = BusinessController.Instance.FindBy<Model.Configuration>(c => c.Name.Contains(Utils.EMAIL_CONFIGURATION_PREFIX)).ToList();
            Model.Configuration host = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.HOST).FirstOrDefault();
            Model.Configuration port = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.PORT).FirstOrDefault();
            Model.Configuration ssl = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.ENABLE_SSL).FirstOrDefault();
            Model.Configuration username = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.USERNAME).FirstOrDefault();
            Model.Configuration password = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.PASSWORD).FirstOrDefault();

            if (host == null || port == null || ssl == null || username == null || password == null)
            {
                MessageBox.Show("No se pudo cargar la información de la cuenta de correo configurada," +
                                "\ndirijase al módulo de 'Configurar correo' para actualizar los datos correctamente de la cuenta de correo."
                                , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string email = _selectedPatient.Email;

            if (string.IsNullOrEmpty(email))
            {
                new RequestEmailModal(_selectedPatient).ShowDialog();

                if (string.IsNullOrEmpty(_selectedPatient.Email))
	            {
	                return;	 
	            }

                email = _selectedPatient.Email;

                if (MessageBox.Show("¿Desea que este correo sea guardado en la información del paciente?",
                                            "Advertencia",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Warning
                                        ) == MessageBoxResult.Yes)
                {
                    if (BusinessController.Instance.Update<Model.Patient>(_selectedPatient) == false)
                    {
                        MessageBox.Show("No se pudo guardar el correo en la información del paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _selectedPatient.Email = string.Empty;
                    }
                    else
                    {
                        FillPatientFields();
                    }
                }
                else
                {
                    _selectedPatient.Email = string.Empty;
                }
            }
            else if (MessageBox.Show("El correo se enviará a '" + email + "' ¿Desea continuar?",
                                            "Advertencia",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Warning
                                        ) == MessageBoxResult.No)
            {
                return;
            }

            SendMail(host.Value, port.Value, ssl.Value, username.Value, password.Value, email);
        }

        private void SendMail(string host, string port, string ssl, string username, string password, string email)
        {
            try
            {
                lblStatus.Visibility = System.Windows.Visibility.Visible;
                btnPrintMail.IsEnabled = false;
                btnClearForm.IsEnabled = false;
                btnCancel.IsEnabled = false;

                SmtpClient client = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Port = Convert.ToInt32(port),
                    Host = host,
                    EnableSsl = Convert.ToBoolean(ssl),
                    Credentials = new NetworkCredential(username, password)
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(username),
                    Subject = "MyDent - Folio de transacción #" + _paymentFolioGenerated.FolioNumber,
                    Body = GenerateEmailBody(),
                    IsBodyHtml = true
                };

                mail.To.Add(email);

                _sendEmailThread = new Thread(() => SendEmailThread(client, mail));
                _sendEmailThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de enviar el correo.\nDetalle del error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GenerateEmailBody()
        {
            StringBuilder body = new StringBuilder();
            DateTime today = DateTime.Now;
            decimal totalAmountTreatmentPayments = 0m;
            decimal totalAmountPayments = 0m;
            bool thereAreTreatments = _paymentFolioGenerated.TreatmentPayments.Count > 0;
            bool thereArePayments = _paymentFolioGenerated.Payments.Count > 0;
            string treatmentsTable = Utils.BuildTreatmentPricesTable(_paymentFolioGenerated.TreatmentPayments.ToList(), out totalAmountTreatmentPayments);
            string paymentsTable = Utils.BuildPaymentsTable(_paymentFolioGenerated.Payments.ToList(), out totalAmountPayments);

            body.AppendFormat("<div><strong>Paciente:</strong> {0}</div>", string.Format("(Exp. No. {0}) {1} {2}", _selectedPatient.PatientId, _selectedPatient.FirstName, _selectedPatient.LastName));
            body.AppendFormat("<div><strong>Número de folio de la transacción:</strong> {0}</div>", _paymentFolioGenerated.FolioNumber);
            body.AppendFormat("<div><strong>Fecha y hora de la transacción:</strong> {0}</div>", Utils.FirstCharToUpper(today.ToString("D")) + " a las " + today.ToString("HH:mm") + " hrs.");

            if (_statement != null)
            {
                body.AppendFormat("<div><strong>La siguiente información fue agregada a su estado de cuenta con número:</strong> {0}</div>", _statement.StatementId);
                body.AppendFormat("<div><strong>Fecha de expiración del estado de cuenta:</strong> {0}</div>", Utils.FirstCharToUpper(_statement.ExpirationDate.ToString("D")));
            }

            if (thereAreTreatments)
            {
                body.Append("<div>&nbsp;</div>");
                body.Append("<div><strong>Registro de tratamientos:</strong></div>");
                body.Append(treatmentsTable);
            }

            if (thereArePayments)
            {
                body.Append("<div>&nbsp;</div>");
                body.Append("<div><strong>Registro de pagos:</strong></div>");
                body.Append(paymentsTable);   
            }

            body.Append("<div>&nbsp;</div>");

            if (thereAreTreatments)
                body.AppendFormat("<div><strong>Monto total de tratamientos:</strong> ${0}</div>", totalAmountTreatmentPayments.ToString("0.00")); 

            if (thereArePayments)
                body.AppendFormat("<div><strong>Monto total de pagos:</strong> ${0}</div>", totalAmountPayments.ToString("0.00"));   

            if (_isStatement == false && _statement == null)
		        body.AppendFormat("<div style='color:green;'><strong>Saldo a favor:</strong> ${0}</div>", _positiveBalance.ToString("0.00"));

            if (_isStatement)
            {
                if (_grandTotal == 0m)
                {
                    body.AppendFormat("<div style='color:green;'><strong>Saldo a favor:</strong> ${0}</div>", _positiveBalance.ToString("0.00"));
                    body.Append("<div>&nbsp;</div>");
                    body.Append("<div style='color:green;'><strong>¡Estado de cuenta liquidado!</strong></div>");    
                }
                else
                {
                    body.AppendFormat("<div style='color:red;'><strong>Pendiente por pagar:</strong> ${0}</div>", _grandTotal.ToString("0.00"));
                }                
            }

            return body.ToString();
        }

        private void SendEmailThread(SmtpClient client, MailMessage mail)
        {
            try
            {
                client.Send(mail);
                EmailSentNotify(string.Empty);
            }
            catch (Exception ex)
            {
                EmailSentNotify(ex.Message);
            }
        }

        void EmailSentNotify(string errorMessage)
        {
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                Dispatcher.Invoke(new SendEmailDelegate(EmailSentNotify), errorMessage);
                return;
            }

            EmailSent(errorMessage);
        }

        private void EmailSent(string errorMessage)
        {
            _sendEmailThread = null;
            lblStatus.Visibility = System.Windows.Visibility.Hidden;
            btnPrintMail.IsEnabled = true;
            btnClearForm.IsEnabled = true;
            btnCancel.IsEnabled = true;

            if (string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Correo enviado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo enviar el correo.\n\nDetalle del error:\n" + errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateStatement(Model.Statement currentStatement)
        {
            if (BusinessController.Instance.Add<Model.Statement>(currentStatement) == false)
            {
                throw new Exception("No se pudo crear el estado de cuenta");
            }
        }

        private void UpdateStatement(Model.Statement currentStatement)
        {
            if (BusinessController.Instance.Update<Model.Statement>(currentStatement) == false)
            {
                throw new Exception("No se pudo actualizar el estado de cuenta");
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
            lblPaymentFolioNumber.ToolTip = lblPaymentFolioNumber.Text = _paymentFolioGenerated == null 
                                                                            ? string.Empty 
                                                                            : _paymentFolioGenerated.FolioNumber.ToString();
        }

        private void CreatePaymentFolio()
        {
            _paymentFolioGenerated = new Model.PaymentFolio()
            {
                FolioDate = DateTime.Now,
                UserId = _userLoggedIn.UserId,
                PatientId = _selectedPatient.PatientId
            };

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
                item.Payment.FolioNumber = _paymentFolioGenerated.FolioNumber;
                item.Payment.StatementId = _statement == null ? item.Payment.StatementId : _statement.StatementId;
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
                item.TreatmentPayment.FolioNumber = _paymentFolioGenerated.FolioNumber;
                item.TreatmentPayment.StatementId = _statement == null ? item.TreatmentPayment.StatementId : _statement.StatementId;

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

            //cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = "", Value = null });

            foreach (Model.Patient patient in patients)
            {
                cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = string.Format("(Exp. No. {0}) {1} {2}", patient.PatientId, patient.FirstName, patient.LastName), Value = patient });
            }

            //cbPatients.SelectedIndex = 0;
        }

        private void FillTreatments()
        {
            List<Model.TreatmentPayment> treatments = _statement.TreatmentPayments
                                                .OrderByDescending(p => p.TreatmentDate)
                                                .ToList();

            foreach (var treatment in treatments)
            {
                TreatmentPriceControl treatmentControl = new TreatmentPriceControl(treatment)
                {
                    Width = Double.NaN
                };

                spTreatments.Children.Add(treatmentControl);
            }
        }

        private void FillPayments()
        {
            List<Model.Payment> payments = _statement.Payments
                                            .OrderByDescending(p => p.PaymentDate)
                                            .ToList();

            foreach (var payment in payments)
            {
                PaymentControl paymentControl = new PaymentControl(payment, null)
                {
                    Width = Double.NaN
                };

                spPayments.Children.Add(paymentControl);
            }
        }

        private void SelectPatient()
        {
            for (int i = 0; i < cbPatients.Items.Count; i++)
            {
                Model.Patient patient = (cbPatients.Items[i] as Controllers.ComboBoxItem).Value as Model.Patient;
                if (patient != null && patient.PatientId == _selectedPatient.PatientId)
                {
                    cbPatients.SelectedIndex = i;
                    break;
                }
            }
        }

        private void ClearForm()
        {
            _statement = null;
            _paymentFolioGenerated = null;

            btnSave.IsEnabled = true;
            cbPatients.IsEnabled = true;
            btnAddTreatment.IsEnabled = true;
            btnAddCashPayment.IsEnabled = true;
            btnAddCreditCardPayment.IsEnabled = true;
            btnAddCheckPayment.IsEnabled = true;
            btnPrintMail.IsEnabled = false;
            lblPaymentFolioNumber.ToolTip = lblPaymentFolioNumber.Text = string.Empty;

            spTreatments.Children.Clear();
            spPayments.Children.Clear();

            lblGeneratedStatementNumberCaption.Visibility = System.Windows.Visibility.Hidden;
            lblGeneratedStatementNumber.ToolTip = lblGeneratedStatementNumber.Text = string.Empty;

            cbPatients.SelectedIndex = -1;

            UpdateTotals();
        }

        private void CheckIfTransactionCanBeFinished()
        {
            bool finishTransaction = (_isStatement || GetTreatmentsToSave().Count > 0) && _grandTotal == 0;

            if (finishTransaction
                && MessageBox.Show("Se ha cubierto el costo total de los tratamientos ¿Desea finalizar la transacción y generar un número de folio?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                FinishTransaction();
            }
        }

        private void FinishTransaction()
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

                        CreatePaymentFolio();
                        SavePayments(paymentsToSave);
                        SaveTreatments(treatmentsToSave);
                        SavePositiveBalance();
                        PrepareWindowToPrintFolio();

                        MessageBox.Show("Datos guardados\n\nNúmero de folio generado: " + _paymentFolioGenerated.FolioNumber, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        SendMail();
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
                                    CreatePaymentFolio();
                                    CreateStatement(currentStatement);
                                    _statement = currentStatement;
                                    SavePayments(paymentsToSave);
                                    SaveTreatments(treatmentsToSave);
                                    PrepareWindowToPrintFolio();
                                    ShowStatementNumberGenerated();

                                    MessageBox.Show("Datos guardados\n\nNúmero de folio generado: " + _paymentFolioGenerated.FolioNumber
                                                    + "\nNúmero del estado de cuenta generado: " + _statement.StatementId
                                                    , "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                    SendMail();
                                }
                            }
                        }
                        else if (currentStatement.ExpirationDate < DateTime.Now.Date)
                        {
                            MessageBox.Show("Este paciente posee un estado de cuenta que ha expirado (Estado de cuenta número: " + currentStatement.StatementId + ")" +
                                            "\nEl monto faltante no puede ser agregado al estado de cuenta, por tal motivo tiene que liquidar los tratamientos seleccionados en este momento.",
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
                                CreatePaymentFolio();
                                _statement = currentStatement;
                                SavePayments(paymentsToSave);
                                SaveTreatments(treatmentsToSave);
                                PrepareWindowToPrintFolio();                                                               

                                ShowStatementNumberGenerated();

                                MessageBox.Show("Datos guardados\n\nNúmero de folio generado: " + _paymentFolioGenerated.FolioNumber
                                                    + "\nNúmero del estado de cuenta modificado: " + _statement.StatementId
                                                    , "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                                SendMail();
                            }
                        }
                    }
                }
                else
                {
                    if (paymentsToSave.Count == 0 && treatmentsToSave.Count == 0)
                    {
                        MessageBox.Show("Agregue al menos un pago o un tratamiento para poder guardar", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
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

                        CreatePaymentFolio();
                        _statement.IsPaid = true;
                        UpdateStatement(_statement);
                        SavePayments(paymentsToSave);
                        SaveTreatments(treatmentsToSave);                        
                        SavePositiveBalance();
                        PrepareWindowToPrintFolio();

                        MessageBox.Show("Datos guardados\n\nEl estado de cuenta fue marcado como liquidado.\nNúmero de folio generado: " + _paymentFolioGenerated.FolioNumber, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        SendMail();
                    }
                    else
                    {
                        if (MessageBox.Show("¿Está seguro(a) que desea guardar los cambios realizados en el estado de cuenta del paciente?",
                                            "Advertencia",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Warning
                                        ) == MessageBoxResult.Yes)
                        {
                            CreatePaymentFolio();
                            SavePayments(paymentsToSave);
                            SaveTreatments(treatmentsToSave);
                            PrepareWindowToPrintFolio();

                            MessageBox.Show("Datos guardados\n\nNúmero de folio generado: " + _paymentFolioGenerated.FolioNumber, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                            SendMail();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al tratar de guardar la información de la caja.\nDetalle del error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}