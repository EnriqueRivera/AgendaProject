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
	/// Interaction logic for ManageBanksWindow.xaml
	/// </summary>
	public partial class ManageBanksWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Bank> _banksViewModel;
        #endregion

        #region Constructors
        public ManageBanksWindow()
		{
			this.InitializeComponent();

            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnAddBank_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditBanksModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnEditBank_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Bank bankSelected = dgBanks.SelectedItem == null ? null : dgBanks.SelectedItem as Model.Bank;

            if (bankSelected == null)
            {
                MessageBox.Show("Seleccione un banco", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditBanksModal(bankSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteBank_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Bank bankSelected = dgBanks.SelectedItem == null ? null : dgBanks.SelectedItem as Model.Bank;

            if (bankSelected == null)
            {
                MessageBox.Show("Seleccione un banco", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el banco número '{0}'?",
                                        bankSelected.BankId),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                bankSelected.IsDeleted = true;

                if (Controllers.BusinessController.Instance.Update<Model.Bank>(bankSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el banco", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            _banksViewModel = new Controllers.CustomViewModel<Model.Bank>(t => t.IsDeleted == false, "BankId", "asc");
            this.DataContext = _banksViewModel;
        }
        #endregion
    }
}