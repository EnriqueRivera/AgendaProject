using Microsoft.Win32;
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
using Controllers;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

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
        private bool _enableSsl;
        private string _username;
        private string _password;
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

                if (AreValidFields(subject, body, allPatientEmails) == false)
                {
                    return;
                }

                SmtpClient client = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Port = _port,
                    Host = _host,
                    EnableSsl = _enableSsl,
                    Credentials = new NetworkCredential(_username, _password)
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(_username),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                AddEmailsTo(mail, allPatientEmails);
                AddAttachments(mail);

                client.Send(mail);

                MessageBox.Show("Correo enviado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                EmptyForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo enviar el correo.\n\nDetalle del error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
		}

		private void btnSearchFiles_Click(object sender, System.Windows.RoutedEventArgs e)
		{

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;

            bool? userClickedOK = openFileDialog.ShowDialog();

            if (userClickedOK == true)
            {
                string[] selectedFiles = openFileDialog.FileNames;
                AttachSelectedFiles(selectedFiles);
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

        void emailControl_OnRemoveAttachFile(object sender, bool e)
        {
            spAttachedFiles.Children.Remove(sender as EmailContactControl);
        }

        private void btnFindEmail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<Model.Patient> selectedPatients = new List<Model.Patient>();

            new FindPatientEmailModal(selectedPatients).ShowDialog();

            foreach (Model.Patient patient in selectedPatients)
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

        private void chkAllPatients_Checked_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFindEmail.IsEnabled = !chkAllPatients.IsChecked.Value;
        }

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Window's logic
        private void AddEmailsTo(MailMessage mail, List<Model.Patient> allPatientEmails)
        {
            foreach (Model.Patient patient in allPatientEmails)
            {
                mail.To.Add(patient.Email);
            }

            foreach (EmailContactControl emailControl in spEmailTo.Children)
            {
                mail.To.Add((emailControl.EmailElement as Controllers.EmailContact).Email);
            }
        }

        private void AddAttachments(MailMessage mail)
        {
            foreach (EmailContactControl emailControl in spAttachedFiles.Children)
            {
                Attachment attachment = new Attachment((emailControl.EmailElement as Controllers.EmailAttachment).Path, MediaTypeNames.Application.Octet);
                mail.Attachments.Add(attachment);
            }
        }

        private bool AreValidFields(string subject, string body, List<Model.Patient> allPatientEmails)
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
            warningMessage += string.IsNullOrEmpty(body) ? "\n-Mensaje" : string.Empty;
            warningMessage += spAttachedFiles.Children.Count == 0 ? "\n-Archivos adjuntos" : string.Empty;

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
            spAttachedFiles.Children.Clear();
            chkAllPatients.IsChecked = false;
        }

        private bool LoadEmailConfiguration()
        {
            List<Model.Configuration> emailConfigurations = BusinessController.Instance.FindBy<Model.Configuration>(c => c.Name.Contains(Utils.EMAIL_CONFIGURATION_PREFIX)).ToList();
            Model.Configuration host = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.HOST).FirstOrDefault();
            Model.Configuration port = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.PORT).FirstOrDefault();
            Model.Configuration ssl = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.ENABLE_SSL).FirstOrDefault();
            Model.Configuration username = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.USERNAME).FirstOrDefault();
            Model.Configuration password = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.PASSWORD).FirstOrDefault();

            if (host == null || port == null || ssl == null || username == null || password == null)
            {
                return false;
            }

            if (int.TryParse(port.Value, out _port) == false || bool.TryParse(ssl.Value, out _enableSsl) == false)
            {
                return false;
            }

            _host = host.Value;
            _username = username.Value;
            _password = password.Value;

            txtFrom.Text = _username;

            return true;
        }

        private void AttachSelectedFiles(string[] selectedFiles)
        {
            foreach (string path in selectedFiles)
            {
                Controllers.EmailAttachment file = new Controllers.EmailAttachment()
                {
                    Path = path,
                    FileName = System.IO.Path.GetFileName(path)
                };

                EmailContactControl emailControl = new EmailContactControl(file);
                emailControl.Margin = new Thickness(5, 0, 0, 0);
                emailControl.OnRemove += emailControl_OnRemoveAttachFile;

                spAttachedFiles.Children.Add(emailControl);
            }
        }
        #endregion
    }
}