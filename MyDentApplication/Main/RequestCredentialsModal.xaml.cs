﻿using System;
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
	/// Interaction logic for RequestCredentialsModal.xaml
	/// </summary>
	public partial class RequestCredentialsModal : Window
	{
        #region Instance variables
        private Model.User _userResult;
        #endregion

        #region Constructors
        public RequestCredentialsModal(Model.User userResult)
		{
			this.InitializeComponent();

            _userResult = userResult;
		}
        #endregion

        #region Window event handlers
        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
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
                _userResult.UserId = userResult.UserId;
                _userResult.FirstName = userResult.FirstName;
                _userResult.LastName = userResult.LastName;
                _userResult.IsAdmin = true;

                this.Close();
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
        }
        #endregion
    }
}