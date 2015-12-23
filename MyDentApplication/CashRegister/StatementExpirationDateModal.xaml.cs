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
	/// Interaction logic for StatementExpirationDateModal.xaml
	/// </summary>
	public partial class StatementExpirationDateModal : Window
	{
        #region Instance variables
        private Model.Statement _statement;
        private Model.Patient _patient;
        private Model.User _user;
        #endregion

        #region Constructors
        public StatementExpirationDateModal(Model.Statement statement, Model.Patient patient, Model.User user)
		{
			this.InitializeComponent();

            _statement = statement;
            _patient = patient;
            _user = user;
		}
        #endregion

        #region Window event handlers
        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (dtpExpirationDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (dtpExpirationDate.SelectedDate.Value.Date <= DateTime.Now.Date)
            {
                MessageBox.Show("No puede seleccionar una fecha pasada", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _statement.ExpirationDate = dtpExpirationDate.SelectedDate.Value;
                _statement.PatientId = _patient.PatientId;
                _statement.CreationDate = DateTime.Now;
                _statement.IsPaid = false;
                _statement.UserId = _user.UserId;

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