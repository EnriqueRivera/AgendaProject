using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using Controllers;
using System.Threading;

namespace MyDentApplication
{
    /// <summary>
    /// Interaction logic for SendEmailWindow.xaml
    /// </summary>
    public partial class SendEmailWindow : Window
    {
        #region Instance variables
        private string _host;
        private int _port;
        private string _username;
        private string _clientId;
        private string _clientSecret;
        private Thread _sendEmailThread;
        #endregion

        #region Delegates
        delegate void RefreshRemindersDelegate(string errorMessage);
        #endregion

        #region Constructors
        public SendEmailWindow()
		{
			this.InitializeComponent();

            if (LoadEmailConfiguration() == false)
            {
                MessageBox.Show("No se pudo cargar la información de la cuenta de correo configurada,\ndirijase al módulo de 'Configurar correo' para actualizar los datos correctamente de la cuenta de correo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                btnSendEmail.IsEnabled = false;
            }
		}
        #endregion

        #region Window event handlers
        private void btnSendEmail_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            try
            {
                string subject = txtSubject.Text.Trim();
                string body = rteBody.Text.Trim();
                List<Model.Patient> allPatientEmails = new List<Model.Patient>();

                if (chkAllPatients.IsChecked == true)
                {
                    allPatientEmails = Controllers.BusinessController.Instance.FindBy<Model.Patient>(u => u.IsDeleted == false && string.IsNullOrEmpty(u.Email) == false).ToList();
                }

                if (AreValidFields(subject, allPatientEmails) == false)
                {
                    return;
                }

                lblStatus.Visibility = System.Windows.Visibility.Visible;
                btnSendEmail.IsEnabled = false;

                List<string> toEmails = allPatientEmails.Select(patient => patient.Email).ToList();
                foreach (EmailContactControl emailControl in spEmailTo.Children)
                {
                    toEmails.Add((emailControl.EmailElement as Controllers.EmailContact).Email);
                }

                int intPort = Convert.ToInt32(_port);
                _sendEmailThread = new Thread(() => SendEmailThread(_host, intPort, _username, toEmails.ToArray(), subject, body, _clientId, _clientSecret));
                _sendEmailThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo enviar el correo.\n\nDetalle del error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
		}

        private void btnAddEmail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Controllers.EmailContact emailToAdd = new Controllers.EmailContact();
            new AddEmailModal(emailToAdd).ShowDialog();

            if (emailToAdd.IsValid)
            {
                EmailContactControl emailControl = new EmailContactControl(emailToAdd);
                emailControl.Margin = new Thickness(5, 0, 0, 0);
                emailControl.OnRemove += emailControl_OnRemoveEmail;

                spEmailTo.Children.Add(emailControl);
            }
        }

        void emailControl_OnRemoveEmail(object sender, bool e)
        {
            spEmailTo.Children.Remove(sender as EmailContactControl);
        }

        private void btnFindEmail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<Model.Patient> selectedPatients = new List<Model.Patient>();

            new FindPatientEmailModal(selectedPatients).ShowDialog();

            foreach (Model.Patient patient in selectedPatients)
            {
                if (string.IsNullOrEmpty(patient.Email) == false)
                {
                    Controllers.EmailContact emailToAdd = new Controllers.EmailContact()
                    {
                        IsValid = true,
                        IsPatient = true,
                        Email = patient.Email,
                        FullName = patient.FirstName + " " + patient.LastName
                    };

                    EmailContactControl emailControl = new EmailContactControl(emailToAdd);
                    emailControl.Margin = new Thickness(5, 0, 0, 0);
                    emailControl.OnRemove += emailControl_OnRemoveEmail;

                    spEmailTo.Children.Add(emailControl);
                }
            }
        }

        private void chkAllPatients_Checked_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFindEmail.IsEnabled = !chkAllPatients.IsChecked.Value;
        }

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_sendEmailThread != null)
            {
                MessageBox.Show("No puede cerrar la ventana hasta que finalice el envío del correo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                e.Cancel = true;
            }
        }
        #endregion

        #region Window's logic
        private async void SendEmailThread(string host, int port, string fromEmail, string[] toEmails, string subject, string body, string clientId, string clientSecret)
        {
            try
            {
                await Utils.SendMail(host, port, fromEmail, toEmails, subject, body, clientId, clientSecret);
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
                Dispatcher.Invoke(new RefreshRemindersDelegate(EmailSentNotify), errorMessage);
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
                EmptyForm();
            }
            else
            {
                MessageBox.Show("No se pudo enviar el correo.\n\nDetalle del error:\n" + errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            btnSendEmail.IsEnabled = true;            
        }

        private bool AreValidFields(string subject, List<Model.Patient> allPatientEmails)
        {
            if (spEmailTo.Children.Count == 0)
            {
                if (chkAllPatients.IsChecked == false)
                {
                    MessageBox.Show("Necesita proporcionar al menos un correo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;   
                }
                else if (allPatientEmails.Count == 0)
                {
                    MessageBox.Show("Ningún paciente registrado posee un correo válido, por favor proporcione al menos un correo", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;   
                }
            }

            string warningMessage = string.Empty;
            warningMessage += string.IsNullOrEmpty(subject) ? "\n-Asunto" : string.Empty;

            if (string.IsNullOrEmpty(warningMessage))
            {
                return true;
            }
            else if (MessageBox.Show
                                ("Faltan los siguientes campos:" + warningMessage + "\n\n¿Desea continuar?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void EmptyForm()
        {
            spEmailTo.Children.Clear();
            txtSubject.Text = string.Empty;
            rteBody.Text = string.Empty;
            chkAllPatients.IsChecked = false;
        }

        private bool LoadEmailConfiguration()
        {
            List<Model.Configuration> emailConfigurations = BusinessController.Instance.FindBy<Model.Configuration>(c => c.Name.Contains(Utils.EMAIL_CONFIGURATION_PREFIX)).ToList();
            Model.Configuration host = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.HOST).FirstOrDefault();
            Model.Configuration port = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.PORT).FirstOrDefault();
            Model.Configuration ssl = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.ENABLE_SSL).FirstOrDefault();
            Model.Configuration username = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.USERNAME).FirstOrDefault();
            Model.Configuration clientId = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.EMAIL_CLIENT_ID).FirstOrDefault();
            Model.Configuration clientSecret = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.EMAIL_CLIENT_SECRET).FirstOrDefault();

            if (host == null || port == null || ssl == null || username == null || clientId == null || clientSecret == null)
            {
                return false;
            }

            if (int.TryParse(port.Value, out _port) == false)
            {
                return false;
            }

            _host = host.Value;
            _username = username.Value;
            _clientId = clientId.Value;
            _clientSecret = clientSecret.Value;

            txtFrom.Text = _username;

            return true;
        }

        #endregion
    }
}