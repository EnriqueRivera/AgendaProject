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
	/// Interaction logic for AddInstrumentModal.xaml
	/// </summary>
	public partial class AddInstrumentModal : Window
	{
        #region Instance variables
        private Model.Drawer _selectedDrawer;
        private Model.Instrument _instrumentToAdd;
        #endregion

        #region Constructors
        public AddInstrumentModal(Model.Drawer selectedDrawer)
		{
			this.InitializeComponent();

            _selectedDrawer = selectedDrawer;
            _instrumentToAdd = new Model.Instrument();

            UpdateNumberOfTreatments();
		}
        #endregion

        #region Window event handlers
        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}

		private void btnAddInstrument_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string instrumentName = txtInstrumentName.Text.Trim();
            string quantityText = txtQuantity.Text.Trim();
            string maxUsesText = txtMaxUses.Text.Trim();
            int quantity = 0;
            int maxUses = 0;

            if (AreValidFields(instrumentName, quantityText, maxUsesText, out quantity, out maxUses) == false)
            {
                return;
            }

            _instrumentToAdd.Name = instrumentName;
            _instrumentToAdd.Quantity = quantity;
            _instrumentToAdd.DrawerId = _selectedDrawer.DrawerId;
            _instrumentToAdd.UsesLeft = txtMaxUses.IsEnabled ? maxUses : new Nullable<int>();
            _instrumentToAdd.MaxUses = txtMaxUses.IsEnabled ? maxUses : new Nullable<int>();
            _instrumentToAdd.IsDeleted = false;

            AddInstrument(_instrumentToAdd);
        }

        private void viewTreatments_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new ManageInstrumentTreatmentsModal(_instrumentToAdd).ShowDialog();
            UpdateNumberOfTreatments();

        }
        #endregion

        #region Window's logic
        private void AddInstrument(Model.Instrument instrumentToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Instrument>(instrumentToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el instrumento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AreValidFields(string instrumentName, string quantityText, string maxUsesText, out int quantity, out int maxUses)
        {
            quantity = -1;
            maxUses = -1;

            if (string.IsNullOrEmpty(instrumentName))
            {
                MessageBox.Show("Introduzca un nombre para el instrumento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (int.TryParse(quantityText, out quantity) == false || quantity < 1)
            {
                MessageBox.Show("Cantidad inválida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (txtMaxUses.IsEnabled)
            {
                if (int.TryParse(maxUsesText, out maxUses) == false)
                {
                    MessageBox.Show("No. de usos inválido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }

                if (quantity != 0 && quantity != 1)
                {
                    MessageBox.Show("La cantidad debe ser 1 para los instrumentos utilizados en un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            return true;
        }

        private void UpdateNumberOfTreatments()
        {
            int usedOn = _instrumentToAdd.Treatments.Count;
            lblNumberOfTreatments.Content = lblNumberOfTreatments.ToolTip = usedOn + " Tratamiento(s)";
            txtMaxUses.IsEnabled = usedOn > 0;
        }
        #endregion
    }
}