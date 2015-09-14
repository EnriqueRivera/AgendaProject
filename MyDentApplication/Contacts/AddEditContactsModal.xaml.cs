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
	/// Interaction logic for AddEditContactsModal.xaml
	/// </summary>
	public partial class AddEditContactsModal : Window
	{
        #region Instance variables
        private Model.Contact _contactToUpdate;
        private bool _isUpdateContact;
        #endregion

        #region Constructors
        public AddEditContactsModal(Model.Contact contactToUpdate)
		{
			this.InitializeComponent();

            _contactToUpdate = contactToUpdate;
            _isUpdateContact = contactToUpdate != null;

            if (_isUpdateContact)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddUpdateContact_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string firstName = txtContactFirstName.Text.Trim();
            string lastName = txtContactLastName.Text.Trim();
            string cellPhone = txtContactCellPhone.Text.Trim();
            string homePhone = txtContactHomePhone.Text.Trim();
            string address = txtContactAddress.Text.Trim();

            if (AreValidFields(firstName, lastName, cellPhone, homePhone, address) == false)
            {
                return;
            }

            if (_isUpdateContact)
            {
                _contactToUpdate.FirstName = firstName;
                _contactToUpdate.LastName = lastName;
                _contactToUpdate.CellPhone = cellPhone;
                _contactToUpdate.HomePhone = homePhone;
                _contactToUpdate.Address = address;

                UpdateContact(_contactToUpdate);
            }
            else
            {
                Model.Contact contactToAdd = new Model.Contact()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    CellPhone = cellPhone,
                    HomePhone = homePhone,
                    Address = address
                };

                AddContact(contactToAdd);
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
        }
        #endregion

        #region Window's logic
        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del contacto";
            btnAddUpdateContact.Content = "Actualizar";

            txtContactFirstName.Text = _contactToUpdate.FirstName;
            txtContactLastName.Text = _contactToUpdate.LastName;
            txtContactCellPhone.Text = _contactToUpdate.CellPhone;
            txtContactHomePhone.Text = _contactToUpdate.HomePhone;
            txtContactAddress.Text = _contactToUpdate.Address;
        }

        private void AddContact(Model.Contact contactToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.Contact>(contactToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el contacto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateContact(Model.Contact contactToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.Contact>(contactToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el contacto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool AreValidFields(string firstName, string lastName, string cellPhone, string homePhone, string address)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Ingrese un nombre", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Ingrese un apellido", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(cellPhone) && string.IsNullOrEmpty(homePhone))
            {
                MessageBox.Show("Ingrese al menos un teléfono", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Ingrese una dirección", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}