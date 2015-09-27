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
	/// Interaction logic for CleanedMaterialsDetailsModal.xaml
	/// </summary>
	public partial class CleanedMaterialsDetailsModal : Window
	{
        #region Instance variables
        private Model.CleanedMaterial _cleanedMaterialToView;
        #endregion

        #region Constructors
        public CleanedMaterialsDetailsModal(Model.CleanedMaterial cleanedMaterialToView)
		{
			this.InitializeComponent();

            _cleanedMaterialToView = cleanedMaterialToView;

            FillWindowInfo();
		}
        #endregion

        #region Window event handlers
        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.Close();
        }
        #endregion

        #region Window's logic
        private void FillWindowInfo()
        {
            //General info
            txtGeneralInfoDate.ToolTip = txtGeneralInfoDate.Content = _cleanedMaterialToView.CreatedDate.ToString("dd/MMMM/yyyy");
            txtGroupLetter.ToolTip = txtGroupLetter.Content = _cleanedMaterialToView.GroupLetter;
            txtGeneralInfoObservations.Text = _cleanedMaterialToView.Observations;
            chkCleaned.IsChecked = _cleanedMaterialToView.Cleaned != null;
            chkPackaging.IsChecked = _cleanedMaterialToView.Packaged != null;
            chkSterilized.IsChecked = _cleanedMaterialToView.Sterilized != null;

            //Clean
            if (_cleanedMaterialToView.Cleaned != null)
            {
                gbClean.Visibility = System.Windows.Visibility.Visible;
                txtCleanDate.ToolTip = txtCleanDate.Content = _cleanedMaterialToView.CleanedAction.ActionDate.ToString("dd/MMMM/yyyy");
                txtCleanShift.ToolTip = txtCleanShift.Content = _cleanedMaterialToView.CleanedAction.Shift;
                txtCleanUser.ToolTip = txtCleanUser.Content = _cleanedMaterialToView.CleanedAction.User.FirstName + " " + _cleanedMaterialToView.CleanedAction.User.LastName;
                txtCleanObservations.ToolTip = txtCleanObservations.Text = _cleanedMaterialToView.CleanedAction.Observations;
            }

            //Packaging
            if (_cleanedMaterialToView.Packaged != null)
            {
                gbPackaged.Visibility = System.Windows.Visibility.Visible;
                txtPackagedDate.ToolTip = txtPackagedDate.Content = _cleanedMaterialToView.PackagedAction.ActionDate.ToString("dd/MMMM/yyyy");
                txtPackagedShift.ToolTip = txtPackagedShift.Content = _cleanedMaterialToView.PackagedAction.Shift;
                txtPackagedUser.ToolTip = txtPackagedUser.Content = _cleanedMaterialToView.PackagedAction.User.FirstName + " " + _cleanedMaterialToView.PackagedAction.User.LastName;
                txtPackagedObservations.ToolTip = txtPackagedObservations.Text = _cleanedMaterialToView.PackagedAction.Observations;
            }

            //Sterilized
            if (_cleanedMaterialToView.Sterilized != null)
            {
                gbSterilized.Visibility = System.Windows.Visibility.Visible;
                txtSterilizedDate.ToolTip = txtSterilizedDate.Content = _cleanedMaterialToView.SterilizedAction.ActionDate.ToString("dd/MMMM/yyyy");
                txtSterilizedShift.ToolTip = txtSterilizedShift.Content = _cleanedMaterialToView.SterilizedAction.Shift;
                txtSterilizedUser.ToolTip = txtSterilizedUser.Content = _cleanedMaterialToView.SterilizedAction.User.FirstName + " " + _cleanedMaterialToView.SterilizedAction.User.LastName;
                txtSterilizedObservations.ToolTip = txtSterilizedObservations.Text = _cleanedMaterialToView.SterilizedAction.Observations;
            }
        }
        #endregion
    }
}