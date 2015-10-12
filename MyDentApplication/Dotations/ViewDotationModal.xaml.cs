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
	/// Interaction logic for ViewDotationModal.xaml
	/// </summary>
	public partial class ViewDotationModal : Window
	{
        #region Instance variables
        private Model.Dotation _dotationToView;
        #endregion

        #region Constructors
        public ViewDotationModal(Model.Dotation dotationToView)
		{
			this.InitializeComponent();

            _dotationToView = dotationToView;
            LoadDotationInfo();
		}
        #endregion

        #region Window event handlers
        private void btnSign_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.User userResult = new Model.User();
            new RequestCredentialsModal(userResult).ShowDialog();

            if (userResult.UserId > 0)
            {
                _dotationToView.UserId = userResult.UserId;
                _dotationToView.SignedDate = DateTime.Now;

                if (Controllers.BusinessController.Instance.Update<Model.Dotation>(_dotationToView))
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo firmar la dotación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}
        #endregion

        #region Window's logic
        private void LoadDotationInfo()
        {
            lblDotationDate.ToolTip = lblDotationDate.Content = _dotationToView.DotationDate.ToString("D") + _dotationToView.DotationDate.ToString(", HH:mm:ss") + " hrs";
            lblDotationAmount.ToolTip = lblDotationAmount.Content = "$" + _dotationToView.Amount;

            if (_dotationToView.UserId != null)
            {
                lblDotationSignedBy.ToolTip = lblDotationSignedBy.Content = _dotationToView.User.FirstName + " " + _dotationToView.User.LastName;
                lblDotationSignedDate.ToolTip = lblDotationSignedDate.Content = _dotationToView.SignedDate.Value.ToString("D") + _dotationToView.SignedDate.Value.ToString(", HH:mm:ss") + " hrs";
                btnSign.IsEnabled = false;
            }
        }
        #endregion
	}
}