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
	/// Interaction logic for ManageUserAuthorizationsWindow.xaml
	/// </summary>
	public partial class ManageUserAuthorizationsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Authorization> _authorizationsViewModel;
        private Model.Patient _selectedPatient;
        #endregion

        public ManageUserAuthorizationsWindow(Model.Patient selectedPatient)
		{
			this.InitializeComponent();

            _selectedPatient = selectedPatient;
            lblExpNumber.ToolTip = lblExpNumber.Content = selectedPatient.AssignedId;
            lblPatientName.ToolTip = lblPatientName.Content = selectedPatient.FirstName + " " + selectedPatient.LastName;

            UpdateGrid();
		}

        #region Window event handlers
        private void btnAddAuthorization_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditAuthorizationsModal(_selectedPatient, null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditAuthorization_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Authorization authorizationSelected = dgAuthorization.SelectedItem == null ? null : dgAuthorization.SelectedItem as Model.Authorization;

            if (authorizationSelected == null)
            {
                MessageBox.Show("Seleccione una autorización", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditAuthorizationsModal(_selectedPatient, authorizationSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteAuthorization_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Authorization authorizationSelected = dgAuthorization.SelectedItem == null ? null : dgAuthorization.SelectedItem as Model.Authorization;

            if (authorizationSelected == null)
            {
                MessageBox.Show("Seleccione un autorización", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                ("¿Está seguro(a) que desea eliminar la autorizacón seleccionada?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                if (Controllers.BusinessController.Instance.Update<Model.Authorization>(authorizationSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la autorización", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            _authorizationsViewModel = new Controllers.CustomViewModel<Model.Authorization>(t => t.PatientId == _selectedPatient.PatientId, "AuthorizationDate", "desc");
            this.DataContext = _authorizationsViewModel;
        }
        #endregion
    }
}