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
	/// Interaction logic for ManageTreatmentPricesWindow.xaml
	/// </summary>
	public partial class ManageTreatmentPricesWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.TreatmentPrice> _tratmentsViewModel;
        #endregion

        #region Constructors
        public ManageTreatmentPricesWindow()
		{
			this.InitializeComponent();

            UpdateGrid();
        }
        #endregion

        #region Window event handlers
        private void btnAddTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new AddEditTreatmentPricesModal(null).ShowDialog();
            UpdateGrid();
        }

        private void btnEditTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.TreatmentPrice treatmentSelected = dgTreatments.SelectedItem == null ? null : dgTreatments.SelectedItem as Model.TreatmentPrice;

            if (treatmentSelected == null)
            {
                MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditTreatmentPricesModal(treatmentSelected).ShowDialog();
                UpdateGrid();
            }
        }

        private void btnDeleteTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.TreatmentPrice treatmentSelected = dgTreatments.SelectedItem == null ? null : dgTreatments.SelectedItem as Model.TreatmentPrice;

            if (treatmentSelected == null)
            {
                MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el tratamiento '{0}'?",
                                        treatmentSelected.Name),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                treatmentSelected.IsDeleted = true;

                if (Controllers.BusinessController.Instance.Update<Model.TreatmentPrice>(treatmentSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el tratamiento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            _tratmentsViewModel = new Controllers.CustomViewModel<Model.TreatmentPrice>(u => u.IsDeleted == false, "TreatmentPriceId", "asc");
            this.DataContext = _tratmentsViewModel;
        }
        #endregion
    }
}