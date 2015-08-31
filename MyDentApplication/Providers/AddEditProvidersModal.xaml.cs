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
	/// Interaction logic for AddEditProvidersModal.xaml
	/// </summary>
	public partial class AddEditProvidersModal : Window
	{
        #region Instance variables
        private Model.ResourceProvider _providerToUpdate;
        private bool _isUpdateProvider;
        #endregion

        #region Constructors
        public AddEditProvidersModal(Model.ResourceProvider providerToUpdate)
		{
			this.InitializeComponent();

            _providerToUpdate = providerToUpdate;
            _isUpdateProvider = _providerToUpdate != null;

            if (_isUpdateProvider)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddUpdateProviders_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string providerName = txtProviderName.Text.Trim();
            if (string.IsNullOrEmpty(providerName))
            {
                MessageBox.Show("Ingrese un nombre para el proveedor", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_isUpdateProvider)
            {
                _providerToUpdate.Name = providerName;

                UpdateProvider(_providerToUpdate);
            }
            else
            {
                Model.ResourceProvider providerToAdd = new Model.ResourceProvider()
                {
                    Name = providerName,
                    IsDeleted = false
                };

                AddProvider(providerToAdd);
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}
        #endregion

        #region Window's logic
        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del proveedor";
            btnAddUpdateProviders.Content = "Actualizar";
            txtProviderId.Text = _providerToUpdate.ProviderId.ToString();
            txtProviderName.Text = _providerToUpdate.Name;
        }

        private void AddProvider(Model.ResourceProvider providerToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.ResourceProvider>(providerToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el proveedor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateProvider(Model.ResourceProvider providerToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.ResourceProvider>(providerToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el proveedor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}