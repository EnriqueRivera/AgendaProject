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
	/// Interaction logic for ManageProvidersWindow.xaml
	/// </summary>
	public partial class ManageProvidersWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.ResourceProvider> _providersViewModel;
        #endregion

        #region Constructors
        public ManageProvidersWindow()
		{
			this.InitializeComponent();

            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnAddProvider_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditProvidersModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditProvider_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.ResourceProvider providerSelected = dgProviders.SelectedItem == null ? null : dgProviders.SelectedItem as Model.ResourceProvider;

            if (providerSelected == null)
            {
                MessageBox.Show("Seleccione un proveedor", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditProvidersModal(providerSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteProvider_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.ResourceProvider providerSelected = dgProviders.SelectedItem == null ? null : dgProviders.SelectedItem as Model.ResourceProvider;

            if (providerSelected == null)
            {
                MessageBox.Show("Seleccione un proveedor", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el proveedor número '{0}'?",
                                        providerSelected.ProviderId),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                providerSelected.IsDeleted = true;

                if (Controllers.BusinessController.Instance.Update<Model.ResourceProvider>(providerSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el proveedor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            _providersViewModel = new Controllers.CustomViewModel<Model.ResourceProvider>(rp => rp.IsDeleted == false, "ProviderId", "asc");
            this.DataContext = _providersViewModel;
        }
        #endregion
    }
}