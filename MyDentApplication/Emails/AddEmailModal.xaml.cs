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
	/// Interaction logic for AddEmailModal.xaml
	/// </summary>
	public partial class AddEmailModal : Window
    {
        #region Instance variables
        private Controllers.EmailContact _emailToAdd;
        #endregion

        #region Constructors
        public AddEmailModal(Controllers.EmailContact emailToAdd)
		{
			this.InitializeComponent();

            _emailToAdd = emailToAdd;
            _emailToAdd.IsValid = false;
		}
        #endregion

        #region Window event handlers
        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();
		}

		private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string email = txtEmail.Text.Trim();

            if (Controllers.Utils.IsValidEmail(email))
            {
                var patient = Controllers.BusinessController.Instance.FindBy<Model.Patient>(p => p.IsDeleted == false && p.Email == email).FirstOrDefault();

                if (patient != null)
	            {
		            _emailToAdd.IsPatient = true;
                    _emailToAdd.FullName = patient.FirstName + " " + patient.LastName;
	            }

                _emailToAdd.Email = email;
                _emailToAdd.IsValid = true;

                this.Close();
            }
            else
            {
                MessageBox.Show("Email inválido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}