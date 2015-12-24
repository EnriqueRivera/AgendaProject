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
	/// Interaction logic for ManagePatientStatementsWindow.xaml
	/// </summary>
	public partial class ManagePatientStatementsWindow : Window
    {
        #region Instance variables
        private Controllers.CustomViewModel<Model.Statement> _statementsViewModel;
        private Model.User _userLoggedIn;
        private Model.Patient _selectedPatient;
        private List<Model.Statement> _patientStatements;
        #endregion

        #region Constructors
        public ManagePatientStatementsWindow(Model.User userLoggedIn, Model.Patient selectedPatient, List<Model.Statement> patientStatements)
        {
            this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            _selectedPatient = selectedPatient;
            _patientStatements = patientStatements;

            lblPatientName.ToolTip = lblPatientName.Content = string.Format("(Exp. No. {0}) {1} {2}", _selectedPatient.PatientId, _selectedPatient.FirstName, _selectedPatient.LastName);

            UpdateGrid();
        }
        #endregion

        #region Window event handlers
        private void btnViewStatement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.Statement selectedStatement = dgStatements.SelectedItem == null ? null : dgStatements.SelectedItem as Model.Statement;

            if (selectedStatement == null)
            {
                MessageBox.Show("Seleccione un estado de cuenta", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new ViewStatementWindow(selectedStatement).ShowDialog();
            }
        }

        private void btnAddPaymentToStatement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.Statement selectedStatement = dgStatements.SelectedItem == null ? null : dgStatements.SelectedItem as Model.Statement;

            if (selectedStatement == null)
            {
                MessageBox.Show("Seleccione un estado de cuenta", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (selectedStatement.IsPaid)
            {
                MessageBox.Show("No se puede abonar a un estado de cuenta liquidado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new CashRegisterWindow(_userLoggedIn, selectedStatement, _selectedPatient).ShowDialog();
                UpdateGrid();
            }
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            _statementsViewModel = new Controllers.CustomViewModel<Model.Statement>(_patientStatements);
            this.DataContext = _statementsViewModel;
        }        
        #endregion
    }
}