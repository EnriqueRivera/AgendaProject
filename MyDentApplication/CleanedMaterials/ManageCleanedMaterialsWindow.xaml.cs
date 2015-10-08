using Controllers;
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
	/// Interaction logic for ManageCleanedMaterialsWindow.xaml
	/// </summary>
	public partial class ManageCleanedMaterialsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.CleanedMaterial> _cleanedMaterialsViewModel;
        private Model.User _userLoggedIn;
        #endregion

        #region Constructors
        public ManageCleanedMaterialsWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();
            _userLoggedIn = userLoggedIn;
            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnDeleteCleanedMaterial_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.CleanedMaterial cleanedMaterialSelected = dgCleanedMaterials.SelectedItem == null ? null : dgCleanedMaterials.SelectedItem as Model.CleanedMaterial;

            if (cleanedMaterialSelected == null)
            {
                MessageBox.Show("Seleccione un registro de limpieza", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                ("¿Está seguro(a) que desea eliminar el registro seleccionado?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                cleanedMaterialSelected.IsDeleted = true;

                if (Controllers.BusinessController.Instance.Update<Model.CleanedMaterial>(cleanedMaterialSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

		private void btnAddCleanedMaterial_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditCleanedMaterialsModal(null).ShowDialog();
            UpdateGrid();
		}

        private void btnEditCleanedMaterial_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.CleanedMaterial cleanedMaterialSelected = dgCleanedMaterials.SelectedItem == null ? null : dgCleanedMaterials.SelectedItem as Model.CleanedMaterial;

            if (cleanedMaterialSelected == null)
            {
                MessageBox.Show("Seleccione un registro de limpieza", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditCleanedMaterialsModal(cleanedMaterialSelected).ShowDialog();
                UpdateGrid();
            }
        }

        private void btnMarkReSterilized_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MarkAsSterelized();
        }

        private void btnMarkSterilized_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MarkAsSterelized();
        }

        private void btnMarkCleaned_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.CleanedMaterial cleanedMaterialSelected = dgCleanedMaterials.SelectedItem == null ? null : dgCleanedMaterials.SelectedItem as Model.CleanedMaterial;

            if (cleanedMaterialSelected == null)
            {
                MessageBox.Show("Seleccione un registro de limpieza", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new ChangeCleanedActionModal(cleanedMaterialSelected, CleaningType.CLEANED, _userLoggedIn).ShowDialog();
                UpdateGrid();
            }
        }

        private void btnMarkPackaged_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.CleanedMaterial cleanedMaterialSelected = dgCleanedMaterials.SelectedItem == null ? null : dgCleanedMaterials.SelectedItem as Model.CleanedMaterial;

            if (cleanedMaterialSelected == null)
            {
                MessageBox.Show("Seleccione un registro de limpieza", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new ChangeCleanedActionModal(cleanedMaterialSelected, CleaningType.PACKAGED, _userLoggedIn).ShowDialog();
                UpdateGrid();
            }
        }

        private void btnViewCleanedMaterial_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.CleanedMaterial cleanedMaterialSelected = dgCleanedMaterials.SelectedItem == null ? null : dgCleanedMaterials.SelectedItem as Model.CleanedMaterial;

            if (cleanedMaterialSelected == null)
            {
                MessageBox.Show("Seleccione un registro de limpieza", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new CleanedMaterialsDetailsModal(cleanedMaterialSelected).ShowDialog();
            }
        }

        private void dgCleanedMaterials_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateMarkButtonsLogic();
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            _cleanedMaterialsViewModel = new Controllers.CustomViewModel<Model.CleanedMaterial>(i => i.IsDeleted == false, "CreatedDate", "desc");

            this.DataContext = _cleanedMaterialsViewModel;

            UpdateMarkButtonsLogic();
        }

        private void EnableMarkButtons(bool enableCleaned, bool enablePackaged, bool enableSterilized, bool enableReSterilized)
        {
            btnMarkCleaned.IsEnabled = enableCleaned;
            btnMarkPackaged.IsEnabled = enablePackaged;
            btnMarkSterilized.IsEnabled = enableSterilized;
            btnMarkReSterilized.IsEnabled = enableReSterilized;
        }

        private void UpdateMarkButtonsLogic()
        {
            Model.CleanedMaterial cleanedMaterialSelected = dgCleanedMaterials.SelectedItem == null ? null : dgCleanedMaterials.SelectedItem as Model.CleanedMaterial;

            if (cleanedMaterialSelected == null)
            {
                EnableMarkButtons(false, false, false, false);
            }
            else if (cleanedMaterialSelected.Cleaned == null && cleanedMaterialSelected.Sterilized == null)
            {
                EnableMarkButtons(true, false, false, true);
            }
            else if (cleanedMaterialSelected.Packaged == null && cleanedMaterialSelected.Sterilized == null)
            {
                EnableMarkButtons(false, true, false, false);
            }
            else if (cleanedMaterialSelected.Sterilized == null)
            {
                EnableMarkButtons(false, false, true, false);
            }
            else
            {
                EnableMarkButtons(false, false, false, false);
            }
        }

        private void MarkAsSterelized()
        {
            Model.CleanedMaterial cleanedMaterialSelected = dgCleanedMaterials.SelectedItem == null ? null : dgCleanedMaterials.SelectedItem as Model.CleanedMaterial;

            if (cleanedMaterialSelected == null)
            {
                MessageBox.Show("Seleccione un registro de limpieza", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new ChangeCleanedActionModal(cleanedMaterialSelected, CleaningType.STERILIZED, _userLoggedIn).ShowDialog();
                UpdateGrid();
            }
        }
        #endregion
    }
}