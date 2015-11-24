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
using System.Linq;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ManageDrawersWindow.xaml
	/// </summary>
	public partial class ManageDrawersWindow : Window
    {
        #region Instance variables
        private Model.User _userLoggedIn;
        #endregion

        #region Constructors
        public ManageDrawersWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            UpdateDrawers();
		}
        #endregion

        #region Window event handlers
        void controlToAdd_OnDelete(object sender, EventArgs e)
        {
            UpdateDrawers();
        }

        private void btnAddDrawer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new AddEditDrawersModal(null).ShowDialog();
            UpdateDrawers();
        }
        #endregion

        #region Window's logic
        private void UpdateDrawers()
        {
            List<Model.Drawer> drawers = Controllers.BusinessController.Instance.FindBy<Model.Drawer>(d => d.IsDeleted == false).OrderBy(d => d.DrawerId).ToList();

            lblDrawerQuantity.ToolTip = lblDrawerQuantity.Text = drawers.Count.ToString();
            spDrawers.Children.Clear();

            foreach (Model.Drawer drawer in drawers)
            {
                DrawerControl controlToAdd = new DrawerControl(drawer, _userLoggedIn);

                controlToAdd.OnDelete += controlToAdd_OnDelete;
                controlToAdd.Margin = new Thickness(5);
                controlToAdd.Width = Double.NaN;

                spDrawers.Children.Add(controlToAdd);   
            }
        }
        #endregion
    }
}