using System;
using System.Collections.Generic;
using System.Data.Objects;
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
	/// Interaction logic for ManageDotationsWindow.xaml
	/// </summary>
	public partial class ManageDotationsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Dotation> _dotationsViewModel;
        #endregion

        #region Constructors
        public ManageDotationsWindow()
		{
			this.InitializeComponent();

            dpDotations.SelectedDate = DateTime.Now;  
		}
        #endregion

        #region Window event handlers
        private void btnDeleteDotation_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Dotation dotationSelected = dgDotations.SelectedItem == null ? null : dgDotations.SelectedItem as Model.Dotation;

            if (dotationSelected == null)
            {
                MessageBox.Show("Seleccione una dotación", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (dotationSelected.UserId != null)
            {
                MessageBox.Show("No puede eliminar una dotación que ya ha sido firmada", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                ("¿Está seguro(a) que desea eliminar la dotación seleccionada?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                if (Controllers.BusinessController.Instance.Delete<Model.Dotation>(dotationSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la dotación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

		private void btnEditDotation_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Dotation dotationSelected = dgDotations.SelectedItem == null ? null : dgDotations.SelectedItem as Model.Dotation;

            if (dotationSelected == null)
            {
                MessageBox.Show("Seleccione una dotación", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (dotationSelected.UserId != null)
            {
                MessageBox.Show("No puede editar una dotación que ya ha sido firmada", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditDotationsModal(dotationSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnAddDotation_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditDotationsModal(null).ShowDialog();
            UpdateGrid();
		}

		private void dpDotations_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
            UpdateGrid();
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            _dotationsViewModel = new Controllers.CustomViewModel<Model.Dotation>(d => EntityFunctions.TruncateTime(d.DotationDate) == EntityFunctions.TruncateTime(dpDotations.SelectedDate.Value), "DotationDate", "asc");
            this.DataContext = _dotationsViewModel;
        }
        #endregion
    }
}