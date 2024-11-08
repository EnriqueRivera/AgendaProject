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
            string clientId = pbClientId.Password;
            string clientSecret = pbClientSecret.Password;
            int port;

            if (AreValidFields(host, portText, username, clientId, clientSecret, out port) == false)
            {
                return;
            }

            bool emailConfigurationUpdated =
                BusinessController.Instance.AddUpdateConfiguration(Utils.EMAIL_CONFIGURATION_PREFIX + Utils.HOST, host)
                & BusinessController.Instance.AddUpdateConfiguration(Utils.EMAIL_CONFIGURATION_PREFIX + Utils.PORT, port.ToString())
                & BusinessController.Instance.AddUpdateConfiguration(Utils.EMAIL_CONFIGURATION_PREFIX + Utils.ENABLE_SSL, chkSsl.IsChecked.ToString())
                & BusinessController.Instance.AddUpdateConfiguration(Utils.EMAIL_CONFIGURATION_PREFIX + Utils.EMAIL_CLIENT_ID, clientId)
                & BusinessController.Instance.AddUpdateConfiguration(Utils.EMAIL_CONFIGURATION_PREFIX + Utils.EMAIL_CLIENT_SECRET, clientSecret);

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
            txtPort.Text = "587";
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
            Model.Configuration clientId = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.EMAIL_CLIENT_ID).FirstOrDefault();
            Model.Configuration clientSecret = emailConfigurations.Where(c => c.Name == Utils.EMAIL_CONFIGURATION_PREFIX + Utils.EMAIL_CLIENT_SECRET).FirstOrDefault();

            txtHost.Text = host == null ? string.Empty : host.Value;
            txtPort.Text = port == null ? string.Empty : port.Value;
            chkSsl.IsChecked = ssl == null ? false : Convert.ToBoolean(ssl.Value);
            txtUsername.Text = username == null ? string.Empty : username.Value;
            pbClientId.Password = clientId == null ? string.Empty : clientId.Value;
            pbClientSecret.Password = clientSecret == null ? string.Empty : clientSecret.Value;
        }

        private bool AreValidFields(string host, string portText, string username, string clientId, string clientSecret, out int port)
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

            if (Utils.IsValidEmail(username) == false)
            {
                MessageBox.Show("Correo inválido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(clientId))
            {
                MessageBox.Show("Ingrese una Client Id", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(clientSecret))
            {
                MessageBox.Show("Ingrese una Client Secret", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}