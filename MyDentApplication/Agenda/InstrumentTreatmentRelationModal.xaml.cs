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
	/// Interaction logic for InstrumentTreatmentRelationModal.xaml
	/// </summary>
	public partial class InstrumentTreatmentRelationModal : Window
    {
        #region Instance variables
        List<Model.Instrument> _instrumentsWithTreatment;
        Model.Event _eventToAdd;
        #endregion

        #region Constructors
        public InstrumentTreatmentRelationModal(List<Model.Instrument> instrumentsWithTreatment, Model.Event eventToAdd)
		{
			this.InitializeComponent();

            _instrumentsWithTreatment = instrumentsWithTreatment;
            _eventToAdd = eventToAdd;

            FillInstruments();
		}
        #endregion

        #region Window event handlers
        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}

		private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Instrument selectedInstrument = (cbInstruments.SelectedItem as Controllers.ComboBoxItem).Value as Model.Instrument;
            
            if (selectedInstrument == null)
            {
                MessageBox.Show("Seleccione un instrumento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (selectedInstrument.UsesLeft == 0)
            {
                MessageBox.Show("Al instrumento seleccionado ya no le quedan usos", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _eventToAdd.InstrumentId = selectedInstrument.InstrumentId;
                this.Close();
            }
        }
        #endregion

        #region Window's logic
        private void FillInstruments()
        {
            foreach (Model.Instrument instrument in _instrumentsWithTreatment)
            {
                cbInstruments.Items.Add(new Controllers.ComboBoxItem() { Text = instrument.Name + " (Usos restantes: " + instrument.UsesLeft + ")", Value = instrument });
            }

            cbInstruments.SelectedIndex = 0;
        }
        #endregion
    }
}