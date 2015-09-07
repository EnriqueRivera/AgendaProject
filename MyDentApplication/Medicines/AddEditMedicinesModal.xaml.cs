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
	/// Interaction logic for AddEditMedicinesModal.xaml
	/// </summary>
	public partial class AddEditMedicinesModal : Window
    {
        #region Instance variables
        private Model.User _userLoggedIn;
        private Model.Medicine _medicineToUpdate;
        private bool _isUpdateMedicine;
        private bool _fromViewMedicinesControl;
        #endregion

        #region Constructors
        public AddEditMedicinesModal(Model.Medicine medicineToUpdate, Model.User userLoggedIn, bool fromViewMedicinesControl)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            _medicineToUpdate = medicineToUpdate;
            _fromViewMedicinesControl = fromViewMedicinesControl;
            _isUpdateMedicine = medicineToUpdate != null;

            if (_isUpdateMedicine)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddUpdateMedicine_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string medicineName = txtMedicineName.Text.Trim();
            string medicineBrand = txtMedicineBrand.Text.Trim();
            DateTime? expiredDate = dtudExpiredDate.Value;

            if (AreValidFields(medicineName, medicineBrand, expiredDate) == false)
            {
                return;
            }

            if (_isUpdateMedicine)
            {
                _medicineToUpdate.Name = medicineName;
                _medicineToUpdate.Brand = medicineBrand;
                _medicineToUpdate.Batch = txtMedicineBatch.Text.Trim();
                _medicineToUpdate.ExpiredDate = expiredDate.Value;
                _medicineToUpdate.Notes = txtMedicineNotes.Text.Trim();
                _medicineToUpdate.WasReplaced = chkWasMedicineChanged.IsChecked.Value;

                UpdateMedicine(_medicineToUpdate);
            }
            else
            {
                Model.Medicine medicineToAdd = new Model.Medicine()
                {
                    Name = medicineName,
                    Brand = medicineBrand,
                    Batch = txtMedicineBatch.Text.Trim(),
                    ExpiredDate = expiredDate.Value,
                    Notes = txtMedicineNotes.Text.Trim(),
                    WasReplaced = chkWasMedicineChanged.IsChecked.Value,
                    DataCapturerId = _userLoggedIn.UserId,
                    IsDeleted = false
                };

                AddMedicine(medicineToAdd);
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
            this.Title = "Actualizar información del medicamento";
            btnAddUpdateMedicine.Content = "Actualizar";

            txtMedicineName.Text = _medicineToUpdate.Name;
            txtMedicineBrand.Text = _medicineToUpdate.Brand;
            dtudExpiredDate.Value = _medicineToUpdate.ExpiredDate;
            txtMedicineBatch.Text = _medicineToUpdate.Batch;
            txtMedicineNotes.Text = _medicineToUpdate.Notes;
            chkWasMedicineChanged.IsChecked = _medicineToUpdate.WasReplaced;

            if (_fromViewMedicinesControl)
            {
                txtMedicineName.IsEnabled = false;
                txtMedicineBrand.IsEnabled = false;
                dtudExpiredDate.IsEnabled = false;
                txtMedicineBatch.IsEnabled = false;
            }
        }

        private void UpdateMedicine(Model.Medicine medicineToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Medicine>(medicineToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el medicamento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddMedicine(Model.Medicine medicineToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Medicine>(medicineToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el medicamento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AreValidFields(string medicineName, string medicineBrand, DateTime? expiredDate)
        {
            if (string.IsNullOrEmpty(medicineName))
            {
                MessageBox.Show("Ingrese un nombre para el medicamento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(medicineBrand))
            {
                MessageBox.Show("Ingrese una marca", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (expiredDate == null)
            {
                MessageBox.Show("Indique una fecha de caducidad válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}