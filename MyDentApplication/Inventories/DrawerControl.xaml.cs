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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for DrawerControl.xaml
	/// </summary>
	public partial class DrawerControl : UserControl
	{
        #region Instance variables
        private Model.Drawer _selectedDrawer;
        private Model.User _userLoggedIn;
        internal event EventHandler OnDelete;
        #endregion

        #region Constructors
        public DrawerControl(Model.Drawer selectedDrawer, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _selectedDrawer = selectedDrawer;
            _userLoggedIn = userLoggedIn;

            FillDrawerInformation();
		}
        #endregion

        #region Window event handlers
        private void btnOpenDrawer_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new ManageInstrumentsWindow(_selectedDrawer, _userLoggedIn).ShowDialog();
		}

		private void btnEditDrawer_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditDrawersModal(_selectedDrawer).ShowDialog();
            FillDrawerInformation();
		}

		private void btnDeleteDrawer_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (MessageBox.Show(string.Format("¿Está seguro(a) que desea eliminar el cajón con el nombre de '{0}'?\nRecuerde que al hacer esto estará eliminando también su contenido.",
                                    _selectedDrawer.Name),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes
                                && MainWindow.IsValidAdminPassword(_userLoggedIn))
            {
                _selectedDrawer.IsDeleted = true;

                if (BusinessController.Instance.Update<Model.Drawer>(_selectedDrawer))
                {
                    OnDelete(_selectedDrawer, e);
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el cajón", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region Window's logic
        private void FillDrawerInformation()
        {
            int instrumentQuantity = BusinessController.Instance.FindBy<Model.Instrument>(i => i.IsDeleted == false && i.DrawerId == _selectedDrawer.DrawerId).ToList().Sum(i => i.Quantity);

            lblDrawerName.ToolTip = lblDrawerName.Text = _selectedDrawer.Name;
            lblInstrumentQuantity.ToolTip = lblInstrumentQuantity.Text = instrumentQuantity.ToString();
            lblCreatedDate.ToolTip = lblCreatedDate.Text = _selectedDrawer.CreatedDate.ToString("D");
        }
        #endregion
    }
}