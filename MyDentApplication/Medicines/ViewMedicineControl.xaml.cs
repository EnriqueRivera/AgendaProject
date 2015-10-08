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
	/// Interaction logic for ViewMedicineControl.xaml
	/// </summary>
	public partial class ViewMedicineControl : UserControl
	{
        #region Instance variables
        private Model.Medicine _medicine;
        private GradientBrush _expiredMedicineColor;
        private GradientBrush _replacedMedicineColor;
        internal event EventHandler<bool> OnMedicineUpdated;
        #endregion

        #region Getters and setters
        public Model.Medicine Medicine
        {
            get { return _medicine; }

            set
            {
                _medicine = value;
                UpdateMedicineFields();
            }

        }
        #endregion

        #region Constructors
        public ViewMedicineControl()
		{
			this.InitializeComponent();

            _expiredMedicineColor = (GradientBrush)rcExpiredMedicineColor.Fill;
            _replacedMedicineColor = (GradientBrush)rcReplacedMedicineColor.Fill;
		}
        #endregion

        #region Window event handlers
        private void btnViewMedicine_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (_medicine != null)
            {
                new AddEditMedicinesModal(_medicine, null, true).ShowDialog();
                OnMedicineUpdated(sender, true);
            }
        }
        #endregion

        #region Window's logic
        private void UpdateMedicineFields()
        {
            if (_medicine != null)
            {
                rcColorMedicine.Fill = _medicine.WasReplaced ? _replacedMedicineColor : _expiredMedicineColor;
                lblMedicineName.ToolTip = lblMedicineName.Content = _medicine.Name;
                lblMedicineBrand.ToolTip = lblMedicineBrand.Content = _medicine.Brand;
            }
        }
        #endregion
    }
}