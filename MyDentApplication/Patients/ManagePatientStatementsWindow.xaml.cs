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
        private Model.Statement _selectedStatement;
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

        private void dgMedicines_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedStatement = dgStatements.SelectedItem == null ? null : dgStatements.SelectedItem as Model.Statement;
            btnAddPaymentToStatement.IsEnabled = _selectedStatement != null && _selectedStatement.IsPaid == false;
        }
        #endregion

        #region Window event handlers
        private void btnViewStatement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_selectedStatement == null)
            {
                MessageBox.Show("Seleccione un estado de cuenta", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Abrir ventana para ver estado de cuenta
            }
        }

        private void btnAddPaymentToStatement_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_selectedStatement == null)
            {
                MessageBox.Show("Seleccione un estado de cuenta", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (_selectedStatement.IsPaid)
            {
                MessageBox.Show("No se puede abonar a un estado de cuenta liquidado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Abrir caja
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