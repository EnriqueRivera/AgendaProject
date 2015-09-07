using System;
using System.Collections.Generic;
using System.Globalization;
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
	/// Interaction logic for ManageMedicinesWindow.xaml
	/// </summary>
	public partial class ManageMedicinesWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Medicine> _medicinesViewModel;
        private Model.User _userLoggedIn;
        private bool _monthlyMedicinesFilter = true;
        #endregion

        #region Constructors
        public ManageMedicinesWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            dtudMedicines.Value = DateTime.Now;
		}
        #endregion

        #region Window event handlers
        private void btnDeleteMedicine_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Medicine medicineSelected = dgMedicines.SelectedItem == null ? null : dgMedicines.SelectedItem as Model.Medicine;

            if (medicineSelected == null)
            {
                MessageBox.Show("Seleccione un medicamento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el medicamento '{0}'?",
                                        medicineSelected.Name),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                medicineSelected.IsDeleted = true;

                if (Controllers.BusinessController.Instance.Update<Model.Medicine>(medicineSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el medicamento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

		private void btnEditMedicine_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Medicine medicineSelected = dgMedicines.SelectedItem == null ? null : dgMedicines.SelectedItem as Model.Medicine;

            if (medicineSelected == null)
            {
                MessageBox.Show("Seleccione un medicamento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditMedicinesModal(medicineSelected, null, false).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnAddMedicine_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditMedicinesModal(null, _userLoggedIn, false).ShowDialog();
            UpdateGrid();
		}

        private void dtudMedicines_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _monthlyMedicinesFilter = true;
            UpdateGrid();
        }

        private void btnRefreshMedicines_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _monthlyMedicinesFilter = true;
            UpdateGrid();
        }

        private void btnViewAllMedicines_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _monthlyMedicinesFilter = false;
            UpdateGrid();
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            if (_monthlyMedicinesFilter)
            {
                UpdateGridMonthlyMedicines();
            }
            else
            {
                UpdateGridAllMedicines();
            }
        }

        private void UpdateGridMonthlyMedicines()
        {
            DateTime today = dtudMedicines.Value.Value;
            _medicinesViewModel = new Controllers.CustomViewModel<Model.Medicine>(m => m.IsDeleted == false && m.ExpiredDate.Month == today.Month && m.ExpiredDate.Year == today.Year, "Name", "asc");
            this.DataContext = _medicinesViewModel;
        }

        private void UpdateGridAllMedicines()
        {
            _medicinesViewModel = new Controllers.CustomViewModel<Model.Medicine>(m => m.IsDeleted == false, "Name", "asc");
            this.DataContext = _medicinesViewModel;
        }
        #endregion
    }

    public class MonthYearValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
            {
                if (values[0] is DateTime)
                {
                    return ((DateTime)values[0]).ToString("MMMM/yyyy");
                }
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}