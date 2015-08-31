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
	/// Interaction logic for ManageTechnicalsWindow.xaml
	/// </summary>
	public partial class ManageTechnicalsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Technical> _technicalsViewModel;
        #endregion

        #region Constructors
        public ManageTechnicalsWindow()
		{
			this.InitializeComponent();

            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnAddTechnical_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditTechnicalsModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditTechnical_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Technical technicalSelected = dgTechnicals.SelectedItem == null ? null : dgTechnicals.SelectedItem as Model.Technical;

            if (technicalSelected == null)
            {
                MessageBox.Show("Seleccione un técnico", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditTechnicalsModal(technicalSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteTechnical_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Technical technicalSelected = dgTechnicals.SelectedItem == null ? null : dgTechnicals.SelectedItem as Model.Technical;

            if (technicalSelected == null)
            {
                MessageBox.Show("Seleccione un técnico", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el técnico número '{0}'?",
                                        technicalSelected.TechnicalId),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                technicalSelected.IsDeleted = true;

                if (Controllers.BusinessController.Instance.Update<Model.Technical>(technicalSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el técnico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            _technicalsViewModel = new Controllers.CustomViewModel<Model.Technical>(t => t.IsDeleted == false, "TechnicalId", "asc");
            this.DataContext = _technicalsViewModel;
        }
        #endregion
    }
}