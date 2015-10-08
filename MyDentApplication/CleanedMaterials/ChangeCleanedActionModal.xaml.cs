﻿using System;
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
using Controllers;

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
        public ChangeCleanedActionModal(Model.CleanedMaterial cleanedMaterialToUpdate, CleaningType cleaningTypeToUpdate, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            _cleanedMaterialToUpdate = cleanedMaterialToUpdate;
            txtDate.ToolTip = txtDate.Text = DateTime.Now.ToString("dd/MMMM/yyyy");

            _cleaningTypeToUpdate = cleaningTypeToUpdate;

            switch (_cleaningTypeToUpdate)
            {
                case CleaningType.CLEANED:
                    this.Title += "Lavado";
                    break;
                case CleaningType.PACKAGED:
                    this.Title += "Empaquetado";
                    break;
                case CleaningType.STERILIZED:
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
                    case CleaningType.CLEANED:
                        _cleanedMaterialToUpdate.Cleaned = cleanedAction.CleanedActionId;
                        break;
                    case CleaningType.PACKAGED:
                        _cleanedMaterialToUpdate.Packaged = cleanedAction.CleanedActionId;
                        break;
                    case CleaningType.STERILIZED:
                        _cleanedMaterialToUpdate.Sterilized = cleanedAction.CleanedActionId;
                        _cleanedMaterialToUpdate.GroupLetter = txtGroup.Text;
                        break;
                    default:
                        break;
                }

                if (Controllers.BusinessController.Instance.Update<Model.CleanedMaterial>(_cleanedMaterialToUpdate))
                {
                    if (_cleaningTypeToUpdate == CleaningType.STERILIZED)
                    {
                        string reminderMessage = string.Format("Los instrumentos del grupo '{0}' tienen que ser Re-Esterilizados ya que han pasado 30 días desde su esterilización.",
                                                    _cleanedMaterialToUpdate.GroupLetter);

                        Model.Reminder reminderToAdd = new Model.Reminder()
                        {
                            Message = reminderMessage,
                            AppearDate = cleanedAction.ActionDate.AddDays(30.0),
                            CreatedDate = DateTime.Now,
                            RequireAdmin = true,
                            Seen = false,
                            SeenBy = null,
                            AutoGenerated = true
                        };

                        if (Controllers.BusinessController.Instance.Add<Model.Reminder>(reminderToAdd) == false)
                        {
                            MessageBox.Show("No se pudo generar un recordatorio para este grupo de esterilización", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

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