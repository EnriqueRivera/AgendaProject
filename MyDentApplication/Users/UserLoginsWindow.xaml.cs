using System;
using System.Collections.Generic;
using System.Data.Objects;
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
	/// Interaction logic for UserLoginsWindow.xaml
	/// </summary>
	public partial class UserLoginsWindow : Window
    {
        #region Instance variables
        private Controllers.CustomViewModel<Model.Login> _userLoginsViewModel;
        private Model.User _userToCheck;
        #endregion

        #region Constructors
        public UserLoginsWindow(Model.User userToCheck)
		{
			this.InitializeComponent();

            _userToCheck = userToCheck;
            LoadEventInfo();

            dpUserLogins.SelectedDate = DateTime.Now;
		}
        #endregion

        #region Window event handlers
        private void dpUserLogins_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateGrid();
        }
        #endregion

        #region Window's logic
        private void LoadEventInfo()
        {
            lblUserId.ToolTip = lblUserId.Text = _userToCheck.AssignedUserId.ToString();
            lblUserFirstName.ToolTip = lblUserFirstName.Text = _userToCheck.FirstName;
            lblUserLastName.ToolTip = lblUserLastName.Text = _userToCheck.LastName;
        }

        private void UpdateGrid()
        {
            _userLoginsViewModel = new Controllers.CustomViewModel<Model.Login>
                                    (login => login.UserId == _userToCheck.UserId && 
                                        EntityFunctions.TruncateTime(login.LoginDate) == EntityFunctions.TruncateTime(dpUserLogins.SelectedDate)
                                        , "LoginDate", "asc");

            this.DataContext = _userLoginsViewModel;
        }
        #endregion
    }

    public class LoginActionAndTimeValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
            {
                if (values[0] is bool)
                {
                    return ((Boolean)values[0]) ? "Inicio de sesión" : "Cierre de sesión";
                }

                if (values[0] is DateTime)
                {
                    return ((DateTime)values[0]).ToString("HH:mm:ss") + " hrs";
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