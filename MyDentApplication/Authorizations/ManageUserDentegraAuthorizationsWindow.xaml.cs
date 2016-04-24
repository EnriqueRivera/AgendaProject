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
	/// Interaction logic for ManageUserDentegraAuthorizationsWindow.xaml
	/// </summary>
	public partial class ManageUserDentegraAuthorizationsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.DentegraAuthorization> _authorizationsViewModel;
        private Model.Patient _selectedPatient;
        #endregion

        #region Constructors
        public ManageUserDentegraAuthorizationsWindow(Model.Patient selectedPatient)
		{
            this.InitializeComponent();

            _selectedPatient = selectedPatient;
            lblExpNumber.ToolTip = lblExpNumber.Content = selectedPatient.AssignedId;
            lblPatientName.ToolTip = lblPatientName.Content = selectedPatient.FirstName + " " + selectedPatient.LastName;

            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnAddAuthorization_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditDentegraAuthorizationsModal(_selectedPatient, null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditAuthorization_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.DentegraAuthorization elegibilitySelected = dgDentegraAuthorization.SelectedItem == null ? null : dgDentegraAuthorization.SelectedItem as Model.DentegraAuthorization;

            if (elegibilitySelected == null)
            {
                MessageBox.Show("Seleccione una elegibilidad", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditDentegraAuthorizationsModal(_selectedPatient, elegibilitySelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteAuthorization_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.DentegraAuthorization elegibilitySelected = dgDentegraAuthorization.SelectedItem == null ? null : dgDentegraAuthorization.SelectedItem as Model.DentegraAuthorization;

            if (elegibilitySelected == null)
            {
                MessageBox.Show("Seleccione un elegibilidad", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                ("¿Está seguro(a) que desea eliminar la elegibilidad seleccionada?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                if (Controllers.BusinessController.Instance.Update<Model.DentegraAuthorization>(elegibilitySelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la elegibilidad", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            _authorizationsViewModel = new Controllers.CustomViewModel<Model.DentegraAuthorization>(t => t.PatientId == _selectedPatient.PatientId, "AuthorizationDate", "desc");
            this.DataContext = _authorizationsViewModel;
        }
        #endregion
    }
}