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
	/// Interaction logic for AddEditDrawersModal.xaml
	/// </summary>
	public partial class AddEditDrawersModal : Window
    {
        #region Instance variables
        private Model.Drawer _drawerToUpdate;
        private bool _isUpdateDrawerInfo;
        #endregion

        #region Constructors
        public AddEditDrawersModal(Model.Drawer drawerToUpdate)
		{
			this.InitializeComponent();

            _drawerToUpdate = drawerToUpdate;
            _isUpdateDrawerInfo = _drawerToUpdate != null;

            if (_isUpdateDrawerInfo)
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

        private void btnAddUpdateDrawer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string drawerName = txtDrawerName.Text.Trim();

            if (AreValidFields(drawerName) == false)
            {
                return;
            }

            if (_isUpdateDrawerInfo)
            {
                _drawerToUpdate.Name = drawerName;

                UpdateDrawer(_drawerToUpdate);
            }
            else
            {
                Model.Drawer drawerToAdd = new Model.Drawer()
                {
                    Name = drawerName,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                AddDrawer(drawerToAdd);
            }
        }
        #endregion

        #region Window's logic
        private bool AreValidFields(string drawerName)
        {
            if (string.IsNullOrEmpty(drawerName))
            {
                MessageBox.Show("Ingrese el nombre del cajón", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private void AddDrawer(Model.Drawer drawerToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Drawer>(drawerToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el cajón", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateDrawer(Model.Drawer drawerToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Drawer>(drawerToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el cajón", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrepareWindowForUpdates()
        {
            txtDrawerName.ToolTip = txtDrawerName.Text = _drawerToUpdate.Name;
            this.Title = "Actualizar información del cajón";
            btnAddUpdateDrawer.Content = "Actualizar";
        }
        #endregion
    }
}