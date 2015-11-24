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
	/// Interaction logic for ManageInstrumentsWindow.xaml
	/// </summary>
	public partial class ManageInstrumentsWindow : Window
	{
        #region Instance variables
        private Model.Drawer _selectedDrawer;
        private Controllers.CustomViewModel<Model.InventoryAvailability> _inventoryViewModel;
        private DateTime _selectedDate;
        private Model.User _userLoggedIn;
        #endregion

        #region Constructors
        public ManageInstrumentsWindow(Model.Drawer selectedDrawer, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _selectedDrawer = selectedDrawer;
            dtudSelectedMonth.Value = DateTime.Now;
            _userLoggedIn = userLoggedIn;

            UpdateGridAndSignatures();
        }
        #endregion

        #region Window event handlers
        private void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateGridAndSignatures();
        }

        private void btnDeleteInstrument_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.InventoryAvailability selectedInstrument = dgInstruments.SelectedItem == null ? null : dgInstruments.SelectedItem as Model.InventoryAvailability;

            if (selectedInstrument == null)
            {
                MessageBox.Show("Seleccione un instrumento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (MessageBox.Show(string.Format("¿Está seguro(a) que desea eliminar el instrumento con el nombre de '{0}'?\nRecuerde que al hacer esto ya no podrá visualizar el historial de este instrumento.",
                                    selectedInstrument.InstrumentName),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes
                                && MainWindow.IsValidAdminPassword(_userLoggedIn))
                {
                    Model.Instrument instrumentToUpdate = Controllers.BusinessController.Instance.FindById<Model.Instrument>(selectedInstrument.InstrumentId);

                    if (instrumentToUpdate != null)
                    {
                        instrumentToUpdate.IsDeleted = true;

                        if (Controllers.BusinessController.Instance.Update<Model.Instrument>(instrumentToUpdate))
                        {
                            UpdateGrid();
                            return;
                        }
                    }

                    MessageBox.Show("No se pudo eliminar el cajón", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }            
        }

        private void btnEditInstrument_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.InventoryAvailability selectedInstrument = dgInstruments.SelectedItem == null ? null : dgInstruments.SelectedItem as Model.InventoryAvailability;
            DateTime currentDate = DateTime.Now;

            if (selectedInstrument == null)
            {
                MessageBox.Show("Seleccione un instrumento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new EditInstrumentModal(selectedInstrument.InstrumentId, selectedInstrument.InstrumentName).ShowDialog();
                UpdateGrid();
            }
        }

        private void btnAddInstrument_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new AddInstrumentModal(_selectedDrawer).ShowDialog();
            UpdateGrid();
        }

        private void btnAddEditRevision_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.InventoryAvailability selectedInstrument = dgInstruments.SelectedItem == null ? null : dgInstruments.SelectedItem as Model.InventoryAvailability;
            DateTime currentDate = DateTime.Now;

            if (selectedInstrument == null)
            {
                MessageBox.Show("Seleccione un instrumento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (_selectedDate.Year != currentDate.Year || _selectedDate.Month != currentDate.Month)
            {
                MessageBox.Show("No puede agregar/editar una revisión de un mes diferente al mes en curso", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (btnSignature.IsEnabled == false)
            {
                MessageBox.Show("No puede agregar/editar una revisión de un mes que ya ha sido firmado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditInstrumentRevisionModal(selectedInstrument).ShowDialog();
                UpdateGrid();
            }
        }

        private void btnSignature_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<Model.InventoryAvailability> currentInventoryAvailability = (this.DataContext as Controllers.CustomViewModel<Model.InventoryAvailability>).ObservableData.ToList();

            if (currentInventoryAvailability.Count(i => i.IsChecked == false) > 0)
            {
                MessageBox.Show("Debe marcar como revisados todos los instrumentos para poder firmar", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Introduzca credenciales para la primera firma", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                Model.User user1 = new Model.User();
                new RequestCredentialsModal(user1).ShowDialog();

                if (user1.UserId > 0)
                {
                    MessageBox.Show("Introduzca credenciales para la segunda firma", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    Model.User user2 = new Model.User();
                    new RequestCredentialsModal(user2).ShowDialog();

                    if (user2.UserId > 0)
                    {
                        if (user1.UserId == user2.UserId)
                        {
                            MessageBox.Show("Las firmas no pueden ser las mismas", "Información", MessageBoxButton.OK, MessageBoxImage.Information);        
                        }
                        else
                        {
                            Model.InventorySignature signatureToAdd = new Model.InventorySignature()
                            {
                                Signature1 = user1.UserId,
                                Signature2 = user2.UserId,
                                SignatureDate = _selectedDate
                            };

                            if (Controllers.BusinessController.Instance.Add<Model.InventorySignature>(signatureToAdd))
                            {
                                UpdateSignaturesInfo();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo agregar la firmas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Window's logic
        private void UpdateGridAndSignatures()
        {
            _selectedDate = dtudSelectedMonth.Value.Value;

            UpdateSignaturesInfo();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            lblSelectedMonth.ToolTip = lblSelectedMonth.Content = _selectedDate.ToString("MMMM/yyyy");
                        
            List<Model.InventoryAvailability> inventoryAvailability = Controllers.BusinessController.Instance.GetInventoryAvailability(_selectedDrawer.DrawerId, _selectedDate.Year, _selectedDate.Month);

            //if (btnSignature.IsEnabled == false)
            //{
            //    inventoryAvailability = inventoryAvailability.Where(i => i.IsChecked.Value).ToList();
            //}

            _inventoryViewModel = new Controllers.CustomViewModel<Model.InventoryAvailability>(inventoryAvailability);
            this.DataContext = _inventoryViewModel;
        }

        private void UpdateSignaturesInfo()
        {
            Model.InventorySignature signatures = Controllers.BusinessController.Instance.FindBy<Model.InventorySignature>(s => s.SignatureDate.Year == _selectedDate.Year && s.SignatureDate.Month == _selectedDate.Month).FirstOrDefault();

            if (signatures != null)
            {
                lblSignature.ToolTip = lblSignature.Content = signatures.User.FirstName + " " + signatures.User.LastName;
                lblSignature1.ToolTip = lblSignature1.Content = signatures.User1.FirstName + " " + signatures.User1.LastName;
                btnSignature.IsEnabled = false;
            }
            else
            {
                lblSignature.ToolTip = lblSignature.Content = string.Empty;
                lblSignature1.ToolTip = lblSignature1.Content = string.Empty;
                btnSignature.IsEnabled = true;
            }
        }
        #endregion
    }
}