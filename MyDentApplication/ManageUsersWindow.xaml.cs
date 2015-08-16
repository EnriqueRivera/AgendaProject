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
using System.Collections.ObjectModel;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ManageUsersWindow.xaml
	/// </summary>
	public partial class ManageUsersWindow : Window
	{
        private Model.User _userLoggedIn;
        private UsersViewModel _usersViewModel;

        public ManageUsersWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            UpdateGridUsers();
		}

        private void UpdateGridUsers()
        {
            _usersViewModel = new UsersViewModel();
            this.DataContext = _usersViewModel;
        }

        private void btnDeleteUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.User userSelected = dgUsers.SelectedItem == null ? null : dgUsers.SelectedItem as Model.User;

            if (userSelected == null)
            {
                MessageBox.Show("Seleccione un usuario", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (userSelected.IsAdmin)
            {
                MessageBox.Show("No puede eliminar a este usuario", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar al usuario número '{0}'?",
                                        userSelected.AssignedUserId),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                userSelected.IsDeleted = true;
                userSelected.AssignedUserId = -1;

                if (Controllers.BusinessController.Instance.Update<Model.User>(userSelected))
                {
                    UpdateGridUsers();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el usuario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnAddUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new AddEditUsersModal(null).ShowDialog();
            UpdateGridUsers();
        }

        private void btnEditUser_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Model.User userSelected = dgUsers.SelectedItem == null ? null : dgUsers.SelectedItem as Model.User;

            if (userSelected == null)
            {
                MessageBox.Show("Seleccione un usuario", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditUsersModal(userSelected).ShowDialog();
                UpdateGridUsers();
            }
        }

        public class UsersViewModel
        {
            private ObservableCollection<Model.User> _allUsers = new ObservableCollection<Model.User>();

            public UsersViewModel()
            {
                _allUsers = new ObservableCollection<Model.User>(Controllers.BusinessController.Instance.FindBy<Model.User>(u => u.IsDeleted == false).ToList().ToList());
            }

            public ObservableCollection<Model.User> ObservableUsers
            {
                get { return _allUsers; }
            }
        }
	}
}