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

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for SendEmailWindow.xaml
	/// </summary>
	public partial class SendEmailWindow : Window
    {
        #region Constructors
        public SendEmailWindow()
		{
			this.InitializeComponent();
		}
        #endregion

        #region Window event handlers
        private void btnSendEmail_Click(object sender, System.Windows.RoutedEventArgs e)
		{

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
        #endregion

        #region Window's logic
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