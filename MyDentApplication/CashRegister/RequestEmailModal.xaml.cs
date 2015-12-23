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
	/// Interaction logic for RequestEmailModal.xaml
	/// </summary>
	public partial class RequestEmailModal : Window
    {
        #region Instance variables
        Model.Patient _patient;
        #endregion

        #region Constructors
        public RequestEmailModal(Model.Patient patient)
		{
			this.InitializeComponent();

            _patient = patient;
		}
        #endregion

        #region Window event handlers
        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string email = txtEmail.Text.Trim();

            if (Controllers.Utils.IsValidEmail(email) == false)
            {
                MessageBox.Show("Email inválido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _patient.Email = email;

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