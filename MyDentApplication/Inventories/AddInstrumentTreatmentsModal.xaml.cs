using Controllers;
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
	/// Interaction logic for AddInstrumentTreatmentsModal.xaml
	/// </summary>
	public partial class AddInstrumentTreatmentsModal : Window
    {
        #region Instance variables
        private Model.Instrument _instrument;
        #endregion

        #region Constructors
        public AddInstrumentTreatmentsModal(Model.Instrument instrument)
        {
            this.InitializeComponent();

            _instrument = instrument;
            FillTreatments();
        }
        #endregion

        #region Window event handlers
        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (cbTratmentName.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tratamiento", "Informaicón", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Model.Treatment selectedTreatment = (cbTratmentName.SelectedValue as Controllers.ComboBoxItem).Value as Model.Treatment;

            if (_instrument.Treatments.Any(j => j.TreatmentId == selectedTreatment.TreatmentId))
            {
                MessageBox.Show("Este tratamiento ya está ligado al instrumento seleccionado", "Informaicón", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _instrument.Treatments.Add(selectedTreatment);
            this.Close();
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}
        #endregion

        #region Window's logic
        private void FillTreatments()
        {
            List<Model.Treatment> treatments = BusinessController.Instance.FindBy<Model.Treatment>(t => t.IsDeleted == false)
                                                .OrderBy(t => t.Name)
                                                .ThenBy(t => t.Duration)
                                                .ToList();

            foreach (Model.Treatment treatment in treatments)
            {
                cbTratmentName.Items.Add(new Controllers.ComboBoxItem() { Text = treatment.Name, Value = treatment });
            }
        }
        #endregion
    }
}