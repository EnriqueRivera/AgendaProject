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
	/// Interaction logic for AddEditTechnicalsModal.xaml
	/// </summary>
	public partial class AddEditTechnicalsModal : Window
	{
        #region Instance variables
        private Model.Technical _technicalToUpdate;
        private bool _isUpdateTechnical;
        #endregion

        #region Constructors
        public AddEditTechnicalsModal(Model.Technical technicalToUpdate)
		{
			this.InitializeComponent();

            _technicalToUpdate = technicalToUpdate;
            _isUpdateTechnical = _technicalToUpdate != null;

            if (_isUpdateTechnical)
            {
                PrepareWindowForUpdates();
            }
        }
        #endregion

        #region Window event handlers
        private void btnAddUpdateTechnicals_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string technicalName = txtTechnicalName.Text.Trim();
            if (string.IsNullOrEmpty(technicalName))
            {
                MessageBox.Show("Ingrese un nombre para el técnico", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_isUpdateTechnical)
            {
                _technicalToUpdate.Name = technicalName;

                UpdateTechnical(_technicalToUpdate);
            }
            else
            {
                Model.Technical technicalToAdd = new Model.Technical()
                {
                    Name = technicalName,
                    IsDeleted = false
                };

                AddTechnical(technicalToAdd);
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
            this.Title = "Actualizar información del técnico";
            btnAddUpdateTechnicals.Content = "Actualizar";
            txtTechnicalId.Text = _technicalToUpdate.TechnicalId.ToString();
            txtTechnicalName.Text = _technicalToUpdate.Name;
        }

        private void AddTechnical(Model.Technical technicalToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Technical>(technicalToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el técnico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTechnical(Model.Technical technicalToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Technical>(technicalToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el técnico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}