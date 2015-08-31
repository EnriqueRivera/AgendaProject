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
	/// Interaction logic for ManageLaboratoryWorksWindow.xaml
	/// </summary>
	public partial class ManageLaboratoryWorksWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.LaboratoryWork> _laboratoryWorksViewModel;
        #endregion

        #region Constructors
        public ManageLaboratoryWorksWindow()
		{
			this.InitializeComponent();

            dtudSelectedMonth.Value = DateTime.Now;
            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnAddLaboratoryWork_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditLaboratoryWorksModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditLaboratoryWork_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.LaboratoryWork laboratoryWorkSelected = dgLaboratoryWokrs.SelectedItem == null ? null : dgLaboratoryWokrs.SelectedItem as Model.LaboratoryWork;

            if (laboratoryWorkSelected == null)
            {
                MessageBox.Show("Seleccione un trabajo de laboratorio", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditLaboratoryWorksModal(laboratoryWorkSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteLaboratoryWork_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.LaboratoryWork laboratoryWorkSelected = dgLaboratoryWokrs.SelectedItem == null ? null : dgLaboratoryWokrs.SelectedItem as Model.LaboratoryWork;

            if (laboratoryWorkSelected == null)
            {
                MessageBox.Show("Seleccione un trabajo de laboratorio", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                ("¿Está seguro(a) que desea eliminar el trabajo de laboratorio seleccionado?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                laboratoryWorkSelected.IsDeleted = true;

                if (Controllers.BusinessController.Instance.Update<Model.LaboratoryWork>(laboratoryWorkSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el trabajo de laboratorio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

		private void btnViewLaboratoryWorks_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            UpdateGrid();
		}
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            DateTime selectedDate = dtudSelectedMonth.Value.Value;

            _laboratoryWorksViewModel = cbFilter.SelectedIndex == 0
                                        ? new Controllers.CustomViewModel<Model.LaboratoryWork>(lw => lw.IsDeleted == false && lw.DeliveryDate.Month == selectedDate.Month && lw.DeliveryDate.Year == selectedDate.Year, "DeliveryDate", "asc")
                                        : new Controllers.CustomViewModel<Model.LaboratoryWork>(lw => lw.IsDeleted == false && lw.ReceivedDate.Value.Month == selectedDate.Month && lw.ReceivedDate.Value.Year == selectedDate.Year, "ReceivedDate", "asc");


            this.DataContext = _laboratoryWorksViewModel;
        }
        #endregion
	}
}