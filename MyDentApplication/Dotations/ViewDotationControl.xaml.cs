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

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ViewDotationControl.xaml
	/// </summary>
	public partial class ViewDotationControl : UserControl
	{
        #region Instance variables
        private Model.Dotation _dotation;
        private GradientBrush _pendingDotationColor;
        private GradientBrush _signedDotationColor;
        internal event EventHandler<bool> OnDotationUpdated;
        #endregion

        #region Getters and setters
        public Model.Dotation Dotation
        {
            get { return _dotation; }

            set
            {
                _dotation = value;
                UpdateDotationFields();
            }

        }
        #endregion

        #region Constructors
        public ViewDotationControl()
		{
			this.InitializeComponent();

            _pendingDotationColor = (GradientBrush)rcPendingDotations.Fill;
            _signedDotationColor = (GradientBrush)rcSignedDotations.Fill;
        }
        #endregion

        #region Window event handlers
        private void btnViewDotation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_dotation != null)
            {
                new ViewDotationModal(_dotation).ShowDialog();
                OnDotationUpdated(sender, true);
            }
        }
        #endregion

        #region Window's logic
        private void UpdateDotationFields()
        {
            if (_dotation != null)
            {
                rcColorMedicine.Fill = _dotation.UserId == null ? _pendingDotationColor : _signedDotationColor;
                lblDotationTimeCreation.ToolTip = lblDotationTimeCreation.Content = _dotation.DotationDate.ToString("HH:mm") + " hrs.";
                lblDotationAmount.ToolTip = lblDotationAmount.Content = "$" + string.Format("{0:n}", _dotation.Amount);
            }
        }
        #endregion
    }
}