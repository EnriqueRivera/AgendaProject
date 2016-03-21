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
using System.Linq;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ManageInstrumentTreatmentsModal.xaml
	/// </summary>
	public partial class ManageInstrumentTreatmentsModal : Window
    {
        #region Instance variables
        private Model.Instrument _instrument;
        private Controllers.CustomViewModel<Model.Treatment> _instrumentTreatments;
        #endregion

        #region Constructors
        public ManageInstrumentTreatmentsModal(Model.Instrument instrument)
        {
            this.InitializeComponent();

            _instrument = instrument;
        }
        #endregion

        #region Window event handlers
        private void btnAddTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new AddInstrumentTreatmentsModal(_instrument).ShowDialog();

            UpdateGridTreatments();
        }

        private void btnRemoveTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.Treatment treatmentSelected = dgTreatments.SelectedItem == null ? null : dgTreatments.SelectedItem as Model.Treatment;

            if (treatmentSelected == null)
            {
                MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else 
            {
                _instrument.Treatments.Remove(treatmentSelected);
                UpdateGridTreatments();
            }       
        }
		
		private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.Close();
        }
        #endregion

        #region Window's logic
        private void UpdateGridTreatments()
        {
            _instrumentTreatments = new Controllers.CustomViewModel<Model.Treatment>(_instrument.Treatments.ToList());
            dgTreatments.DataContext = _instrumentTreatments;
        }
        #endregion
    }
}