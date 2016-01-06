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
using System.Threading;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
    {
        #region Instance variables       
        private Thread _loginThread;
        #endregion

        #region Delegates
        delegate void CheckLoginDelegate(Model.User userResult);
        #endregion

        #region Constructors
        public LoginWindow()
		{
			this.InitializeComponent();

            txtUserId.Focus();
		}
        #endregion

        #region Window event handlers
        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}

		private void btnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            int userId;
            string userIdText = txtUserId.Text.Trim();
            string password = pbPassword.Password;

            if (int.TryParse(userIdText, out userId) == false || userId < 1)
            {
                MessageBox.Show("Introduzca un número de usuario válido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Introduzca una contraseña", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            lblLoginStatus.Visibility = System.Windows.Visibility.Visible;
            EnableLoginControls(false);

            _loginThread = new Thread(() => Login(userId, password));
            _loginThread.Start();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if (_loginThread != null)
            {
                e.Cancel = true;
            }
            else
            {
                Controllers.BusinessController.Instance.CloseConnection();
            }
		}
        #endregion

        #region Window's logic
        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Login(int userId, string password)
        {
            try
            {
                Model.User userResult = Controllers.BusinessController.Instance.FindBy<Model.User>(u => u.AssignedUserId == userId && u.Password == password && u.IsDeleted == false).FirstOrDefault();
                DispatcherCheckLogin(userResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void DispatcherCheckLogin(Model.User userResult)
        {
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                Dispatcher.Invoke(new CheckLoginDelegate(DispatcherCheckLogin), userResult);
                return;
            }

            CheckLogin(userResult);
        }

        private void EnableLoginControls(bool enable)
        {
            btnLogin.IsEnabled = enable;
            btnClose.IsEnabled = enable;
            txtUserId.IsEnabled = enable;
            pbPassword.IsEnabled = enable;
        }

        private void CheckLogin(Model.User userResult)
        {
            _loginThread = null;
            lblLoginStatus.Visibility = System.Windows.Visibility.Hidden;
            EnableLoginControls(true);

            if (userResult == null)
            {
                MessageBox.Show("Número de usuario y/o contraseña incorrectos", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MainWindow.RegisterLoginAction(true, userResult.UserId);

                this.Hide();
                new MainWindow(userResult).ShowDialog();

                pbPassword.Password = txtUserId.Text = string.Empty;
                CenterWindowOnScreen();
                this.Show();
            }
        }
        #endregion
    }
}