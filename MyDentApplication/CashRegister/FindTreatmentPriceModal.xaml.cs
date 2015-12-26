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
	/// Interaction logic for FindTreatmentPriceModal.xaml
	/// </summary>
	public partial class FindTreatmentPriceModal : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.TreatmentPrice> _treatmentsViewModel;
        private List<Model.TreatmentPrice> _selectedTreatmentPrice;
        private string _searchTermPrevWindow;
        #endregion

        #region Constructors
        public FindTreatmentPriceModal(List<Model.TreatmentPrice> selectedTreatmentPrice, string searchTermPrevWindow)
        {
            this.InitializeComponent();

            _selectedTreatmentPrice = selectedTreatmentPrice;
            _searchTermPrevWindow = searchTermPrevWindow;

            UpdateGridTermPrevWindow();
        }
        #endregion

        #region Window event handlers
        private void btnRefreshTreatments_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateGridFilteredTreatments();
        }

        private void btnViewAllTreatmentPrices_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateGridAllTreatments();
        }

        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.TreatmentPrice selectedTreatment = dgTreatmentPrices.SelectedItem == null ? null : dgTreatmentPrices.SelectedItem as Model.TreatmentPrice;

            if (selectedTreatment == null)
            {
                MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _selectedTreatmentPrice.Add(selectedTreatment);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Window's logic
        private void UpdateGridTermPrevWindow()
        {
            _treatmentsViewModel = new Controllers.CustomViewModel<Model.TreatmentPrice>(u => u.IsDeleted == false && (u.TreatmentKey.Contains(_searchTermPrevWindow) || u.Name.Contains(_searchTermPrevWindow) || u.Type.Contains(_searchTermPrevWindow)), "TreatmentKey", "asc");
            this.DataContext = _treatmentsViewModel;
        }

        private void UpdateGridFilteredTreatments()
        {
            string searchTerm = txtSearchTerm.Text;

            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    _treatmentsViewModel = new Controllers.CustomViewModel<Model.TreatmentPrice>(u => u.IsDeleted == false && u.TreatmentKey.Contains(searchTerm), "TreatmentKey", "asc");
                    break;
                case 1:
                    _treatmentsViewModel = new Controllers.CustomViewModel<Model.TreatmentPrice>(u => u.IsDeleted == false && u.Name.Contains(searchTerm), "TreatmentKey", "asc");
                    break;
                case 2:
                    _treatmentsViewModel = new Controllers.CustomViewModel<Model.TreatmentPrice>(u => u.IsDeleted == false && u.Type.Contains(searchTerm), "TreatmentKey", "asc");
                    break;
                default:
                    break;
            }

            this.DataContext = _treatmentsViewModel;
        }

        private void UpdateGridAllTreatments()
        {
            _treatmentsViewModel = new Controllers.CustomViewModel<Model.TreatmentPrice>(u => u.IsDeleted == false, "TreatmentKey", "asc");
            this.DataContext = _treatmentsViewModel;
        }
        #endregion
    }
}