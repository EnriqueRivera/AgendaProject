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
	/// Interaction logic for AddEditUsersModal.xaml
	/// </summary>
	public partial class AddEditUsersModal : Window
    {
        #region Instance variables
        private Model.User _userToUpdate;
        private bool _isUpdateUserInfo;
        #endregion

        #region Constructors
        public AddEditUsersModal(Model.User userToUpdate)
		{
			this.InitializeComponent();

            _userToUpdate = userToUpdate;
            _isUpdateUserInfo = userToUpdate != null;

            if (_isUpdateUserInfo)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.Close();
        }

        private void btnAddUpdateUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int userId;
            string userIdText = txtUserId.Text.Trim();
            string userFirstName = txtUserFirstName.Text.Trim();
            string userLastName = txtUserLastName.Text.Trim();
            string userPassword = pbPassword.Password;
            string userRepeatPassword = pbRepeatPassword.Password;

            if (AreValidFields(userIdText, userFirstName, userLastName, userPassword, userRepeatPassword, out userId) == false)
            {
                return;
            }

            if (_isUpdateUserInfo)
            {
                _userToUpdate.AssignedUserId = userId;
                _userToUpdate.FirstName = userFirstName;
                _userToUpdate.LastName = userLastName;
                _userToUpdate.Password = userPassword;

                UpdateUser(_userToUpdate);
            }
            else
            {
                Model.User userToAdd = new Model.User()
                {
                    AssignedUserId = userId,
                    FirstName = userFirstName,
                    LastName = userLastName,
                    Password = userPassword,
                    IsAdmin = false,
                    IsDeleted = false
                };

                AddUser(userToAdd);
            }
        }
        #endregion

        #region Window's logic
        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información de usuario";
            btnAddUpdateUser.Content = "Actualizar";
            txtUserId.Text = _userToUpdate.AssignedUserId.ToString();
            txtUserFirstName.Text = _userToUpdate.FirstName;
            txtUserLastName.Text = _userToUpdate.LastName;
            pbRepeatPassword.Password = pbPassword.Password = _userToUpdate.Password;
        }

        private bool AreValidFields(string userIdText, string userFirstName, string userLastName, string userPassword, string userRepeatPassword, out int userId)
        {
            if (int.TryParse(userIdText, out userId) == false || userId < 1)
            {
                MessageBox.Show("No. de usuario inválido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(userFirstName))
            {
                MessageBox.Show("Ingrese un nombre", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(userLastName))
            {
                MessageBox.Show("Ingrese un apellido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(userPassword))
            {
                MessageBox.Show("Ingrese una contraseña", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(userRepeatPassword))
            {
                MessageBox.Show("Repita la contraseña", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (userPassword != userRepeatPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if ((_isUpdateUserInfo && _userToUpdate.AssignedUserId != userId && UserIdExists(userId)) 
                || (_isUpdateUserInfo == false && UserIdExists(userId)))
            {
                MessageBox.Show("No se puede " + (_isUpdateUserInfo ? "actualizar" : "agregar") + " el usuario porque ya existe otro usuario con el mismo Número de identificación", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private bool UserIdExists(int userId)
        {
            return Controllers.BusinessController.Instance.FindBy<Model.User>(u => u.AssignedUserId == userId).Count() != 0;
        }

        private void UpdateUser(Model.User userToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.User>(userToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddUser(Model.User userToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.User>(userToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}