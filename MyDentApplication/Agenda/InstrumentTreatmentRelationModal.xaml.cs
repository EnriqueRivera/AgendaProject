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
            List<Model.Instrument> selectedInstruments = lstInstruments.Items
                                                            .Cast<Controllers.ComboBoxItem>()
                                                            .Select(i => i.Value as Model.Instrument)
                                                            .ToList();

            if (selectedInstruments.Count == 0)
            {
                MessageBox.Show("Seleccione un instrumento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _eventToAdd.Instruments = selectedInstruments;
                this.Close();
            }
        }

        private void btnAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (Controllers.ComboBoxItem item in lstOptions.SelectedItems)
            {
                bool instrumentExists = false;
                foreach (Controllers.ComboBoxItem instrument in lstInstruments.Items)
                {
                    if ((item.Value as Model.Instrument).InstrumentId == (instrument.Value as Model.Instrument).InstrumentId)
                    {
                        instrumentExists = true;
                        break;
                    }
                }

                if (!instrumentExists)
                {
                    lstInstruments.Items.Add(item);
                }
            }
        }

        private void btnRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            for (int i = lstInstruments.SelectedItems.Count - 1; i >= 0; i--)
            {
                lstInstruments.Items.RemoveAt(i);
            }
        }
        #endregion

        #region Window's logic
        private void FillInstruments()
        {
            foreach (Model.Instrument instrument in _instrumentsWithTreatment)
            {
                lstOptions.Items.Add(new Controllers.ComboBoxItem() { Text = instrument.Name + " (Usos restantes: " + instrument.UsesLeft + ")", Value = instrument });
            }
        }
        #endregion
    }
}