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
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			this.InitializeComponent();

            txtUserId.Focus();
		}

		private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}

		private void btnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            int userId;
            string userIdText = txtUserId.Text.Trim();
            string password = pbPassword.Password;

            if (string.IsNullOrEmpty(userIdText) || int.TryParse(userIdText, out userId) == false)
            {
                MessageBox.Show("Introduzca un número de usuario válido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Introduzca una contraseña", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Model.User userResult = Controllers.BusinessController.Instance.FindBy<Model.User>(u => u.AssignedUserId == userId && u.Password == password && u.IsDeleted == false).FirstOrDefault();

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

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Controllers.BusinessController.Instance.CloseConnection();
		}
	}
}