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
	/// Interaction logic for AddEditTreatmentPricesModal.xaml
	/// </summary>
	public partial class AddEditTreatmentPricesModal : Window
	{
        #region Instance variables
        private Model.TreatmentPrice _treatmentToUpdate;
        private bool _isUpdateTreatmentInfo;
        private string _treatmentType;
        #endregion

        #region Constructors
        public AddEditTreatmentPricesModal(Model.TreatmentPrice treatmentToUpdate, string tratmentType)
		{
			this.InitializeComponent();

            _treatmentType = tratmentType;
            _treatmentToUpdate = treatmentToUpdate;
            _isUpdateTreatmentInfo = _treatmentToUpdate != null;

            if (_isUpdateTreatmentInfo)
            {
                PrepareWindowForUpdates();
            }

            if (_treatmentType == Controllers.Utils.TREATMENT_DENTISTRY)
            {
                lblTreatmentDuration.Visibility = System.Windows.Visibility.Visible;
                cbDiscount.Visibility = System.Windows.Visibility.Visible;
                lblPercentCaption.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.Height = 275;
            }
        }
        #endregion

        #region Window event handlers
        private void btnAddUpdateTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string treatmentKey = txtTreatmentKey.Text.Trim();
            string treatmentName = txtTreatmentName.Text.Trim();
            decimal price;

            if (AreValidFields(treatmentName, treatmentKey, out price) == false)
            {
                return;
            }

            if (_isUpdateTreatmentInfo)
            {
                _treatmentToUpdate.Name = treatmentName;
                _treatmentToUpdate.TreatmentKey = treatmentKey;
                _treatmentToUpdate.Price = price;
                _treatmentToUpdate.Discount = (int)cbDiscount.SelectedItem;
                _treatmentToUpdate.CreatedDate = DateTime.Now;

                UpdateTreatment(_treatmentToUpdate);
            }
            else
            {
                Model.TreatmentPrice treatmentToAdd = new Model.TreatmentPrice()
                {
                    Name = treatmentName,
                    TreatmentKey = treatmentKey,
                    Price = price,
                    Discount = (int)cbDiscount.SelectedItem,
                    Type = _treatmentType,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                AddTreatment(treatmentToAdd);
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
            this.Title = "Actualizar información del tratamiento y costo";
            btnAddUpdateTreatment.Content = "Actualizar";
            txtTreatmentName.ToolTip = txtTreatmentName.Text = _treatmentToUpdate.Name;
            txtTreatmentKey.ToolTip = txtTreatmentKey.Text = _treatmentToUpdate.TreatmentKey;
            txtCost.ToolTip = txtCost.Text = _treatmentToUpdate.Price.ToString();
            cbDiscount.SelectedValue = _treatmentToUpdate.Discount;
        }

        private bool AreValidFields(string treatmentName, string treatmentKey, out decimal price)
        {
            price = 0;

            if (string.IsNullOrEmpty(treatmentName))
            {
                MessageBox.Show("Ingrese el nombre del tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(treatmentKey))
            {
                MessageBox.Show("Ingrese la clave del tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (decimal.TryParse(txtCost.Text.ToString().Trim(), out price) == false || price < 0m)
            {
                MessageBox.Show("Ingrese un costo válido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private void AddTreatment(Model.TreatmentPrice treatmentToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.TreatmentPrice>(treatmentToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el tratamiento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTreatment(Model.TreatmentPrice treatmentToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.TreatmentPrice>(treatmentToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el tratamiento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}