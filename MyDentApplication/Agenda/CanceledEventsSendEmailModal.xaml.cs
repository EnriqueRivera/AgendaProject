using Controllers;
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
using System.Net.Mail;
using System.Net;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for CanceledEventsSendEmailModal.xaml
	/// </summary>
	public partial class CanceledEventsSendEmailModal : Window
    {
        #region Instance variables
        private List<Model.Event> _canceledEventsInARow;
        private List<Model.Event> _canceledEventsOfSameTreatment;
        private Model.Patient _patient;
        private Model.Treatment _treatment;
        private Thread _sendEmailThread;
        #endregion

        #region Delegates
        delegate void SendEmailDelegate(string errorMessage);
        #endregion

        #region Constructors
        public CanceledEventsSendEmailModal(List<Model.Event> canceledEventsInARow, List<Model.Event> canceledEventsOfSameTreatment, Model.Patient patient, Model.Treatment treatment)
        {
            this.InitializeComponent();

            _canceledEventsInARow = canceledEventsInARow;
            _canceledEventsOfSameTreatment = canceledEventsOfSameTreatment;
            _patient = patient;
            _treatment = treatment;

            lblEmailLogMessage.Content = string.Format("Enviando correo(s) al paciente {1} {2} (Exp. No. {0})\npor los siguientes motivos:\n", _patient.AssignedId, _patient.FirstName, _patient.LastName);
            lblEmailLogMessage.Content += _canceledEventsInARow == null 
                                                ? string.Empty 
                                                : "\n- 3 citas canceladas consecutivas.";
            lblEmailLogMessage.Content += _canceledEventsOfSameTreatment == null 
                                                ? string.Empty
                                                : string.Format("\n- 3 citas canceladas para el tratamiento de '{0}'.", _treatment.Name);
        }
        #endregion

        #region Window event handlers
        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SendMail();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_sendEmailThread != null)
            {
                MessageBox.Show("No puede cerrar la ventana hasta que finalice el envío de correos"
                                , "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                e.Cancel = true;
            }
        }
        #endregion

        #region Window's logic
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
                MessageBox.Show("No se envió el correo(s) porque no se pudo cargar la información " + 
                                "de la cuenta de correo configurada,\ndirijase al módulo de 'Configurar correo'" + 
                                " para actualizar los datos correctamente de la cuenta de correo."
                                , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (string.IsNullOrEmpty(_patient.Email))
            {
                MessageBox.Show("No se envió el correo(s) porque el paciente no cuenta con un correo electrónico."
                                , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (_canceledEventsInARow != null)
                {
                    string subject = "MyDent - 3 citas canceladas consecutivas";
                    string body = GetDefaultMessageForCanceledEventsInARow();

                    SendMail(host.Value, port.Value, ssl.Value, username.Value, password.Value, _patient.Email, subject, body);
                }

                if (_canceledEventsOfSameTreatment != null)
                {
                    if (string.IsNullOrEmpty(_treatment.AbsenceMessage))
                    {
                        MessageBox.Show(string.Format(
                                            "El correo por 3 citas cenceladas para determinado tratamiento " +
                                            "no pudo ser enviado porque el tratamiento de '{0}' no posee un " +
                                            "mensaje para el correo de inasistencia.",
                                            _treatment.Name
                                        )
                                        , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        string subject = string.Format("MyDent - 3 citas canceladas para el tratamiento de '{0}'", _treatment.Name);

                        SendMail(host.Value, port.Value, ssl.Value, username.Value, password.Value, _patient.Email, subject, _treatment.AbsenceMessage);
                    }
                }
            }
        }

        private string GetDefaultMessageForCanceledEventsInARow()
        {
            return "Estimado paciente, <br/> Debido a que canceló sus últimas 3 citas se le " +
                    "ha penalizado, de forma que solamente pueda agendar su siguiente cita en un " +
                    "horario de 1:00 p.m a 3:00 p.m y 7:00 p.m en delante. <br />Si desea saber más a " +
                    "detalle sobre esta penalización favor de comunicarse al consultorio.<br /><br />" + 
                    "Por su atención gracias.";
        }

        private void SendMail(string host, string port, string ssl, string username, string password, string email, string subject, string body)
        {
            try
            {
                lblStatus.Visibility = System.Windows.Visibility.Visible;

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
                    Subject = subject,
                    Body = body,
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

            if (string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show("Correo enviado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo enviar el correo.\n\nDetalle del error:\n" + errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}