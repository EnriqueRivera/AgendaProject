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
	/// Interaction logic for AddEditCleanedMaterialsModal.xaml
	/// </summary>
	public partial class AddEditCleanedMaterialsModal : Window
	{
        #region Instance variables
        private Model.CleanedMaterial _cleanedMaterialToUpdate;
        private bool _isUpdateCleanedMaterial;
        #endregion

        #region Constructors
        public AddEditCleanedMaterialsModal(Model.CleanedMaterial cleanedMaterialToUpdate)
		{
			this.InitializeComponent();

            _cleanedMaterialToUpdate = cleanedMaterialToUpdate;

            _isUpdateCleanedMaterial = cleanedMaterialToUpdate != null;
            txtDate.ToolTip = txtDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");

            if (_isUpdateCleanedMaterial)
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

		private void btnAddCleanedMaterial_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (_isUpdateCleanedMaterial)
            {
                _cleanedMaterialToUpdate.Observations = txtCleanedMaterialObservations.Text;

                UpdateCleanedMaterial(_cleanedMaterialToUpdate);
            }
            else
            {
                Model.CleanedMaterial cleanedMaterialToAdd = new Model.CleanedMaterial()
                {
                    CreatedDate = DateTime.Now,
                    GroupLetter = "",
                    Cleaned = null,
                    Packaged = null,
                    Sterilized = null,
                    Observations = txtCleanedMaterialObservations.Text,
                    IsDeleted = false
                };

                AddCleanedMaterial(cleanedMaterialToAdd);
            }
		}
        #endregion

        #region Window's logic
        private void AddCleanedMaterial(Model.CleanedMaterial cleanedMaterialToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.CleanedMaterial>(cleanedMaterialToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el registro de limpieza", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateCleanedMaterial(Model.CleanedMaterial cleanedMaterialToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.CleanedMaterial>(cleanedMaterialToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el registro de limpieza", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar registro de limpieza de instrumentos";
            btnAddCleanedMaterial.Content = "Actualizar";
            txtDate.ToolTip = txtDate.Text = _cleanedMaterialToUpdate.CreatedDate.ToString("dd/MMMM/yyyy");
            txtCleanedMaterialObservations.Text = _cleanedMaterialToUpdate.Observations;
        }
        #endregion
    }
}