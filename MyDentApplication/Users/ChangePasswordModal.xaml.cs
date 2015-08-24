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
	/// Interaction logic for ChangePasswordModal.xaml
	/// </summary>
	public partial class ChangePasswordModal : Window
    {
        #region Instance variables
        private Model.User _userLoggedIn;
        #endregion

        #region Constructors
        public ChangePasswordModal(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
		}
        #endregion

        #region Window event handlers
        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string oldPassword = pbOldPassword.Password;
            string newPassword = pbNewPassword.Password;
            string repeatNewPassword = pbRepeatNewPassword.Password;

            if (string.IsNullOrEmpty(oldPassword))
            {
                MessageBox.Show("Introduzca la contraseña actual", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Introduzca la nueva contraseña", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrEmpty(repeatNewPassword))
            {
                MessageBox.Show("Repita la nueva contraseña", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (oldPassword != _userLoggedIn.Password)
            {
                MessageBox.Show("La contraseña actual no es válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (newPassword != repeatNewPassword)
            {
                MessageBox.Show("Las contraseñas nuevas no coinciden", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _userLoggedIn.Password = newPassword;

            if (Controllers.BusinessController.Instance.Update<Model.User>(_userLoggedIn))
            {
                MessageBox.Show("La contraseña ha sido cambiada", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al tratar de cambiar la contraseña", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();
        }
        #endregion
    }
}