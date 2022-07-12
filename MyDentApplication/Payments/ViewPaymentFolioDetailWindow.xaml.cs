using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Net.Mail;
using Controllers;
using System.Net;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ViewPaymentFolioDetailWindow.xaml
	/// </summary>
	public partial class ViewPaymentFolioDetailWindow : Window
    {
        #region Instance variables
        private Model.PaymentFolio _folio;
        private Controllers.CustomViewModel<Model.TreatmentPayment> _treatmentsViewModel;
        private Controllers.CustomViewModel<Model.Payment> _paymentsViewModel;
        private List<Model.TreatmentPayment> _treatments;
        private List<Model.Payment> _payments;
        private decimal _totalAmountOfTreatments;
        private decimal _totalAmountOfPayments;
        private int _numberOfTreatments;
        private int _numberOfPayments;
        private Thread _sendEmailThread;
        private Model.User _userLoggedIn;
        #endregion

        #region Delegates
        delegate void SendEmailDelegate(string errorMessage);
        #endregion

        public ViewPaymentFolioDetailWindow(Model.PaymentFolio folio, Model.User userLoggedIn)
        {
            this.InitializeComponent();

            _folio = folio;
            _userLoggedIn = userLoggedIn;

            lblPatientName.ToolTip = lblPatientName.Content = string.Format("(Exp. No. {0}) {1} {2}", _folio.Patient.AssignedId, _folio.Patient.FirstName, _folio.Patient.LastName);
            lblFolioNumber.ToolTip = lblFolioNumber.Content = _folio.FolioNumber.ToString();

            UpdateFolioInfo();
        }

        #region Window event handlers
        private void btnGeneratePdf_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string statementName = "Folio numero " + _folio.FolioNumber;

            try
            {
                // Configure save file dialog box
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = statementName + "_" + DateTime.Now.ToString("dd-MMMM-yyyy"); // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "Text documents (.pdf)|*.pdf"; // Filter files by extension

                if (dlg.ShowDialog() == true)
                {
                    ExportToPdf(dlg.FileName);
                    MessageBox.Show("PDF generado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar el PDF.\n\n Detalle del error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSendMail_Click(object sender, System.Windows.RoutedEventArgs e)
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
        }
        #endregion

        #region Window's logic
        private void ExportToPdf(string path)
        {
            BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\ARIAL.TTF", BaseFont.IDENTITY_H, true);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var boldFontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
            var boldFontMessage = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 10);

            iTextSharp.text.pdf.PdfPTable treatmentsTable = MainWindow.GetTableWithHeaders(dgTreatments, bf);
            iTextSharp.text.pdf.PdfPTable paymentsTable = MainWindow.GetTableWithHeaders(dgPayments, bf);
            FillTreatmentsTable(treatmentsTable, bf);
            FillPaymentsTable(paymentsTable, bf);

            //Create the PDF Document
            using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f))
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (var writer = PdfWriter.GetInstance(pdfDoc, fs))
                    {
                        pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                        pdfDoc.Open();

                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));

                        var paragraph = new iTextSharp.text.Paragraph("");
                        paragraph.Add(new Chunk("Folio", boldFontTitle));
                        paragraph.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(paragraph);

                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));

                        paragraph = new iTextSharp.text.Paragraph("");
                        paragraph.Add(new Chunk("Paciente: ", boldFont));
                        paragraph.Add(string.Format("(Exp. No. {0}) {1} {2}", _folio.Patient.AssignedId, _folio.Patient.FirstName, _folio.Patient.LastName));
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paragraph);

                        paragraph = new iTextSharp.text.Paragraph("");
                        paragraph.Add(new Chunk("Número de folio : ", boldFont));
                        paragraph.Add(_folio.FolioNumber.ToString());
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paragraph);

                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));

                        paragraph = new iTextSharp.text.Paragraph("");
                        paragraph.Add(new Chunk("Registro de tratamientos", boldFont));
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paragraph);

                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));
                        pdfDoc.Add(treatmentsTable);

                        paragraph = new iTextSharp.text.Paragraph(lblTreatmentsCount.Content.ToString());
                        paragraph.Alignment = Element.ALIGN_RIGHT;
                        pdfDoc.Add(paragraph);

                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));

                        paragraph = new iTextSharp.text.Paragraph(" ");
                        paragraph.Add(new Chunk("Registro de pagos", boldFont));
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paragraph);

                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));
                        pdfDoc.Add(paymentsTable);

                        paragraph = new iTextSharp.text.Paragraph(lblPaymentsCount.Content.ToString());
                        paragraph.Alignment = Element.ALIGN_RIGHT;
                        pdfDoc.Add(paragraph);

                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));

                        paragraph = new iTextSharp.text.Paragraph("");
                        paragraph.Add(new Chunk("Resumen", boldFont));
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paragraph);

                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));

                        paragraph = new iTextSharp.text.Paragraph("");
                        paragraph.Add(new Chunk("Monto total de tratamientos: ", boldFont));
                        paragraph.Add(lblTotalAmountTreatments.Text);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paragraph);

                        paragraph = new iTextSharp.text.Paragraph("");
                        paragraph.Add(new Chunk("Monto total de pagos: ", boldFont));
                        paragraph.Add(lblTotalAmountPayments.Text);
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paragraph);
                        
                        pdfDoc.Close();
                    }
                }
            }
        }

        private void FillTreatmentsTable(PdfPTable treatmentsTable, BaseFont bf)
        {
            foreach (var item in _treatments)
            {
                MainWindow.AddCell(treatmentsTable, bf, string.Format("{0} - {1} ({2})",
                                                                        item.TreatmentPrice.TreatmentKey,
                                                                        item.TreatmentPrice.Name,
                                                                        item.TreatmentPrice.Type));
                MainWindow.AddCell(treatmentsTable, bf, item.Quantity.ToString());
                MainWindow.AddCell(treatmentsTable, bf, "$" + item.Price.ToString("0.00"));
                MainWindow.AddCell(treatmentsTable, bf, item.Discount.ToString() + "%");
                MainWindow.AddCell(treatmentsTable, bf, "$" + item.Total.ToString("0.00"));
                MainWindow.AddCell(treatmentsTable, bf, item.TreatmentDate.ToString("dd/MMMM/yyyy"));
            }
        }

        private void FillPaymentsTable(PdfPTable paymentsTable, BaseFont bf)
        {
            foreach (var item in _payments)
            {
                MainWindow.AddCell(paymentsTable, bf, item.Type);
                MainWindow.AddCell(paymentsTable, bf, item.Bank == null ? string.Empty : item.Bank.Name);
                MainWindow.AddCell(paymentsTable, bf, "$" + item.Amount.ToString("0.00"));
                MainWindow.AddCell(paymentsTable, bf, item.VoucherCheckNumber == null ? string.Empty : item.VoucherCheckNumber);
                MainWindow.AddCell(paymentsTable, bf, item.PaymentDate.ToString("dd/MMMM/yyyy"));
                MainWindow.AddCell(paymentsTable, bf, item.Observation);
            }
        }

        private void SendMail()
        {
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

            string email = _folio.Patient.Email;

            if (string.IsNullOrEmpty(email))
            {
                new RequestEmailModal(_folio.Patient).ShowDialog();

                if (string.IsNullOrEmpty(_folio.Patient.Email))
                {
                    return;
                }

                email = _folio.Patient.Email;

                if (MessageBox.Show("¿Desea que este correo sea guardado en la información del paciente?",
                                            "Advertencia",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Warning
                                        ) == MessageBoxResult.Yes)
                {
                    if (BusinessController.Instance.Update<Model.Patient>(_folio.Patient) == false)
                    {
                        MessageBox.Show("No se pudo guardar el correo en la información del paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _folio.Patient.Email = string.Empty;
                    }
                }
                else
                {
                    _folio.Patient.Email = string.Empty;
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
                btnSendMail.IsEnabled = false;
                btnGeneratePdf.IsEnabled = false;
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
                    Subject = "MyDent - Folio de transacción #" + _folio.FolioNumber,
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
            bool thereAreTreatments = _treatments.Count > 0;
            bool thereArePayments = _payments.Count > 0;
            string treatmentsTable = Utils.BuildTreatmentPricesTable(_treatments, out _totalAmountOfTreatments);
            string paymentsTable = Utils.BuildPaymentsTable(_payments.ToList(), out _totalAmountOfPayments);

            body.AppendFormat("<div><strong>Paciente:</strong> {0}</div>", string.Format("(Exp. No. {0}) {1} {2}", _folio.Patient.AssignedId, _folio.Patient.FirstName, _folio.Patient.LastName));
            body.AppendFormat("<div><strong>Número de folio:</strong> {0}</div>", _folio.FolioNumber);
            body.AppendFormat("<div><strong>Asistente:</strong> {0}</div>", _userLoggedIn.FirstName + " " + _userLoggedIn.LastName);

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
                body.AppendFormat("<div><strong>Monto total de tratamientos:</strong> ${0}</div>", _totalAmountOfTreatments.ToString("0.00"));

            if (thereArePayments)
                body.AppendFormat("<div><strong>Monto total de pagos:</strong> ${0}</div>", _totalAmountOfPayments.ToString("0.00"));
            
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
            btnSendMail.IsEnabled = true;
            btnGeneratePdf.IsEnabled = true;
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

        private void UpdateFolioInfo()
        {
            _treatments = _folio.TreatmentPayments
                            .OrderBy(t => t.TreatmentDate)
                            .ToList();

            _payments = _folio.Payments
                            .OrderBy(p => p.PaymentDate)
                            .ToList();

            _treatmentsViewModel = new Controllers.CustomViewModel<Model.TreatmentPayment>(_treatments);
            _paymentsViewModel = new Controllers.CustomViewModel<Model.Payment>(_payments);

            dgTreatments.DataContext = _treatmentsViewModel;
            dgPayments.DataContext = _paymentsViewModel;

            UpdateTotals();

            int statementId;
            if (FolioBelongsToStatement(out statementId))
            {
                lblStatementMessage.Visibility = System.Windows.Visibility.Visible;
                lblStatementMessage.ToolTip = lblStatementMessage.Content = lblStatementMessage.Content.ToString() + statementId;
            }
        }

        private bool FolioBelongsToStatement(out int statementId)
        {
            if (_treatments.Count > 0 && _treatments[0].StatementId.HasValue)
            {
                statementId = _treatments[0].StatementId.Value;
                return true;
            }

            if (_payments.Count > 0 && _payments[0].StatementId.HasValue)
            {
                statementId = _payments[0].StatementId.Value;
                return true;
            }

            statementId = 0;
            return false;
        }

        private void UpdateTotals()
        {
            _totalAmountOfTreatments = 0m;
            _totalAmountOfPayments = 0m;
            _numberOfTreatments = 0;
            _numberOfPayments = 0;

            foreach (var item in _treatments)
            {
                _totalAmountOfTreatments += item.Total;
                _numberOfTreatments += item.Quantity;
            }

            foreach (var item in _payments)
            {
                _totalAmountOfPayments += item.Amount;
                _numberOfPayments++;
            }

            lblTreatmentsCount.ToolTip = lblTreatmentsCount.Content = "No. de tratamientos: " + _numberOfTreatments;
            lblPaymentsCount.ToolTip = lblPaymentsCount.Content = "No. de pagos: " + _numberOfPayments;
            lblTotalAmountTreatments.ToolTip = lblTotalAmountTreatments.Text = "$" + _totalAmountOfTreatments.ToString("0.00");
            lblTotalAmountPayments.ToolTip = lblTotalAmountPayments.Text = "$" + _totalAmountOfPayments.ToString("0.00");
        }
        #endregion

    }
}