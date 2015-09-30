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
	/// Interaction logic for ConfigureEmailWindow.xaml
	/// </summary>
	public partial class ConfigureEmailWindow : Window
    {
        #region Constructors
        public ConfigureEmailWindow()
		{
			this.InitializeComponent();

            LoadEmailConfiguration();
		}
        #endregion

        #region Window event handlers
        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string host = txtHost.Text.Trim();
            string portText = txtPort.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = pbPassword.Password;
            string repeatPassword = pbRepeatPassword.Password;
            int port;

            if (AreValidFields(host, portText, username, password, repeatPassword, out port) == false)
            {
                return;
            }

            bool emailConfigurationUpdated =
                BusinessController.Instance.AddUpdateConfiguration(Utils.EMAIL_CONFIGURATION_PREFIX + Utils.HOST, host)
                & BusinessController.Instance.AddUpdateConfiguration(Utils.EMAIL_CONFIGURATION_PREFIX + Utils.PORT, port.ToString())
                & BusinessController.Instance.AddUpdateConfiguration(Utils.EMAIL_CONFIGURATION_PREFIX + Utils.ENABLE_SSL, chkSsl.IsChecked.ToString())
                & BusinessController.Instance.AddUpdateConfiguration(Utils.EMAIL_CONFIGURATION_PREFIX + Utils.USERNAME, username)
                & BusinessController.Instance.AddUpdateConfiguration(Utils.EMAIL_CONFIGURATION_PREFIX + Utils.PASSWORD, password);

            if (emailConfigurationUpdated == false)
            {
                MessageBox.Show("No se pudo actualizar la informacion de la cuenta de correo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnHotmailDefaultInfo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txtHost.Text = "smtp.live.com";
            txtPort.Text = "25";
            chkSsl.IsChecked = true;
        }

        private void btnGmailDefaultInfo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txtHost.Text = "smtp.gmail.com";
            txtPort.Text = "587";
            chkSsl.IsChecked = true;
        }
        #endregion

        #region Window's logic
        private void LoadEmailConfiguration()
        {
            List<Model.Configuration> emailConfigurations = BusinessController.Instance.FindBy<Model.Configuration>(c => c.Name.Contains(Utils.EMAIL_CONFIGURATION_PREFIX)).ToList();
            Model.Configuration host = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.HOST).FirstOrDefault();
            Model.Configuration port = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.PORT).FirstOrDefault();
            Model.Configuration ssl = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.ENABLE_SSL).FirstOrDefault();
            Model.Configuration username = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.USERNAME).FirstOrDefault();
            Model.Configuration password = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.PASSWORD).FirstOrDefault();

            txtHost.Text = host == null ? string.Empty : host.Value;
            txtPort.Text = port == null ? string.Empty : port.Value;
            chkSsl.IsChecked = ssl == null ? false : Convert.ToBoolean(ssl.Value);
            txtUsername.Text = username == null ? string.Empty : username.Value;
            pbPassword.Password = pbRepeatPassword.Password = password == null ? string.Empty : password.Value;
        }

        private bool AreValidFields(string host, string portText, string username, string password, string repeatPassword, out int port)
        {
            port = 0;

            if (string.IsNullOrEmpty(host))
            {
                MessageBox.Show("Ingrese un Host", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (int.TryParse(portText, out port) == false)
            {
                MessageBox.Show("Puerto inválido (debe ser un número)", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Ingrese un usuario", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Ingrese una contraseña", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (password != repeatPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}