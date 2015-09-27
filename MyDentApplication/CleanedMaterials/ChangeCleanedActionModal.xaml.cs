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
using System.Globalization;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ChangeCleanedActionModal.xaml
	/// </summary>
	public partial class ChangeCleanedActionModal : Window
	{
        #region Instance variables
        private Model.CleanedMaterial _cleanedMaterialToUpdate;
        private Model.User _userLoggedIn;
        private CleaningType _cleaningTypeToUpdate;
        #endregion

        #region Constructors
        public ChangeCleanedActionModal(Model.CleanedMaterial cleanedMaterialToUpdate, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            _cleanedMaterialToUpdate = cleanedMaterialToUpdate;
            txtDate.ToolTip = txtDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");

            _cleaningTypeToUpdate = GetCleaningType();

            switch (_cleaningTypeToUpdate)
            {
                case CleaningType.Cleaned:
                    this.Title += "Lavado";
                    break;
                case CleaningType.Packaged:
                    this.Title += "Empaquetado";
                    break;
                case CleaningType.Sterilized:
                    this.Title += "Esterilizado";
                    txtGroup.ToolTip = txtGroup.Text = GetNextGroupLetter();
                    break;
                default:
                    break;
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddCleanedAction_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.CleanedAction cleanedAction = new Model.CleanedAction()
            {
                Shift = cmbShift.SelectedItem.ToString(),
                UserId = _userLoggedIn.UserId,
                ActionDate = DateTime.Now,
                Observations = txtCleanedActionObservations.Text.Trim()
            };

            if (Controllers.BusinessController.Instance.Add<Model.CleanedAction>(cleanedAction))
            {
                switch (_cleaningTypeToUpdate)
                {
                    case CleaningType.Cleaned:
                        _cleanedMaterialToUpdate.Cleaned = cleanedAction.CleanedActionId;
                        break;
                    case CleaningType.Packaged:
                        _cleanedMaterialToUpdate.Packaged = cleanedAction.CleanedActionId;
                        break;
                    case CleaningType.Sterilized:
                        _cleanedMaterialToUpdate.Sterilized = cleanedAction.CleanedActionId;
                        _cleanedMaterialToUpdate.GroupLetter = txtGroup.Text;
                        break;
                    default:
                        break;
                }

                if (Controllers.BusinessController.Instance.Update<Model.CleanedMaterial>(_cleanedMaterialToUpdate))
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo cambiar el estado del registro de limpieza", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo cambiar el estado del registro de limpieza", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}
        #endregion

        #region Window's logic
        private CleaningType GetCleaningType()
        {
            if (_cleanedMaterialToUpdate.Cleaned == null)
            {
                return CleaningType.Cleaned;
            }
            else if (_cleanedMaterialToUpdate.Packaged == null)
            {
                return CleaningType.Packaged;
            }
            else
            {
                return CleaningType.Sterilized;
            }
        }

        private string GetNextGroupLetter()
        {
            string nextLetter = "A";
            Model.CleanedMaterial lastCleanedMaterial = Controllers.BusinessController.Instance.FindBy<Model.CleanedMaterial>(c => c.IsDeleted == false && c.Sterilized != null)
                                                                                .OrderByDescending(c => c.CreatedDate)
                                                                                .ThenByDescending(c => c.GroupLetter)
                                                                                .Take(1)
                                                                                .FirstOrDefault();

            if (lastCleanedMaterial != null)
            {
                var chars = lastCleanedMaterial.GroupLetter.ToCharArray();
                if (chars.Length > 0 && chars[0] != 'Z')
                {
                    int unicode = chars[0];
                    unicode++;
                    nextLetter = ((char)unicode).ToString();
                }
            }

            return nextLetter;
        }
        #endregion

        enum CleaningType
        {
            Cleaned,
            Packaged,
            Sterilized
        }
	}

    public class BoolValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1)
            {
                return values[0] != null;
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}