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
	/// Interaction logic for ManageContactsWindow.xaml
	/// </summary>
	public partial class ManageContactsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Contact> _contactsViewModel;
        private bool _filterUsed = false;
        #endregion

        #region Constructors
        public ManageContactsWindow()
		{
			this.InitializeComponent();

            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnDeleteContact_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Contact contactSelected = dgContacts.SelectedItem == null ? null : dgContacts.SelectedItem as Model.Contact;

            if (contactSelected == null)
            {
                MessageBox.Show("Seleccione un contacto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el contacto '{0}'?",
                                        contactSelected.FirstName + " " + contactSelected.LastName),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                if (Controllers.BusinessController.Instance.Delete<Model.Contact>(contactSelected))
                {
                    UpdateGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el contacto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

		private void btnEditContact_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.Contact contactSelected = dgContacts.SelectedItem == null ? null : dgContacts.SelectedItem as Model.Contact;

            if (contactSelected == null)
            {
                MessageBox.Show("Seleccione un contacto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditContactsModal(contactSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnAddContact_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            new AddEditContactsModal(null).ShowDialog();
            UpdateGrid();
		}

		private void btnViewAllContacts_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            _filterUsed = false;
            UpdateGrid();
		}

		private void btnRefreshContacts_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            _filterUsed = true;
            UpdateGrid();
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            if (_filterUsed)
            {
                UpdateGridFilteredContacts();
            }
            else
            {
                UpdateGridAllContacts();
            }
        }

        private void UpdateGridFilteredContacts()
        {
            string searchTerm = txtSearchTerm.Text;

            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    _contactsViewModel = new Controllers.CustomViewModel<Model.Contact>(c => c.FirstName.Contains(searchTerm), "FirstName", "asc");
                    break;
                case 1:
                    _contactsViewModel = new Controllers.CustomViewModel<Model.Contact>(c => c.LastName.Contains(searchTerm), "FirstName", "asc");
                    break;
                case 2:
                    _contactsViewModel = new Controllers.CustomViewModel<Model.Contact>(c => c.Address.Contains(searchTerm), "FirstName", "asc");
                    break;
                default:
                    break;
            }

            this.DataContext = _contactsViewModel;
        }

        private void UpdateGridAllContacts()
        {
            _contactsViewModel = new Controllers.CustomViewModel<Model.Contact>(c => true, "FirstName", "asc");
            this.DataContext = _contactsViewModel;
        }
        #endregion
    }
}