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
using Microsoft.Win32;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for UpdateClinicHistoryWindow.xaml
	/// </summary>
	public partial class UpdateClinicHistoryWindow : Window
	{
        #region Instance variables
        private Model.Patient _patient;
        private bool _isUpdate;
        #endregion

        #region Constructors
        public UpdateClinicHistoryWindow(Model.Patient patient)
		{
			this.InitializeComponent();

            _patient = patient;
            _isUpdate = patient.ClinicHistoryId != null;

            FillGeneralInfo();

            if (_isUpdate)
            {
                FillAllClinicHistoryInfo();
            }
        }
        #endregion

        #region Window event handlers
        private void btnUpdateClinicHistory_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_isUpdate == false)
            {
                Model.ClinicHistory clinicHistory = new Model.ClinicHistory()
                {
                    UpdateDate = DateTime.Now
                };

                if (Controllers.BusinessController.Instance.Add<Model.ClinicHistory>(clinicHistory) == false)
                {
                    MessageBox.Show("No se pudo actualizar el historial clínico del paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _patient.ClinicHistoryId = clinicHistory.ClinicHistoryId;

                if (Controllers.BusinessController.Instance.Update<Model.Patient>(_patient) == false)
                {
                    MessageBox.Show("No se pudo actualizar el historial clínico del paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (SaveClinicHistoryChanges() == false)
            {
                MessageBox.Show("No se pudo actualizar el historial clínico del paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckBox_CheckedUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox selectedCheckBox = sender as CheckBox;
            if (selectedCheckBox.Tag != null)
            {
                string[] tag = selectedCheckBox.Tag.ToString().Split('|');
                if (tag.Count() > 1)
                {
                    string[] commentFields = tag[1].Split(',');
                    for (int i = 0; i < commentFields.Count(); i++)
                    {
                        TextBox textBoxComment = FindTextFieldByTag(commentFields[i]);
                        if (textBoxComment != null)
                        {
                            textBoxComment.IsEnabled = selectedCheckBox.IsChecked.Value;
                            textBoxComment.Text = textBoxComment.IsEnabled ? textBoxComment.Text : string.Empty;
                        }
                    }
                }
            }
        }

        private void btnFindImage_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                // Create OpenFileDialog 
                OpenFileDialog dlg = new OpenFileDialog();

                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".png";
                dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

                // Get the selected file name and display in a TextBox 
                if (dlg.ShowDialog() == true)
                {
                    LoadPatientPicutre(dlg.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo agregar la imagen seleccionada\n\nDetalle del error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Window's logic
        private bool SaveClinicHistoryChanges()
        {
            bool result = true;
            List<Grid> grids = GetAllGrids();

            foreach (var item in grids)
            {
                result &= SaveGridControlsInfo(item);
            }

            result &= AddUpdateClinicHistoryAttributeValue(Controllers.Utils.PATIENT_PICTURE, lblImagePath.Text);

            _patient.ClinicHistory.UpdateDate = DateTime.Now;

            return result && Controllers.BusinessController.Instance.Update<Model.ClinicHistory>(_patient.ClinicHistory);
        }

        private bool SaveGridControlsInfo(Grid grid)
        {
            bool result = true;

            foreach (var item in grid.Children)
            {
                if (item is TextBox)
                {
                    TextBox textField = item as TextBox;
                    if (textField.Tag != null)
                    {
                        string fieldName = textField.Tag.ToString();
                        string fieldValue = textField.Text.Trim();
                        result &= AddUpdateClinicHistoryAttributeValue(fieldName, fieldValue);
                    }
                }
                else if (item is CheckBox)
                {
                    CheckBox checkBox = item as CheckBox;
                    if (checkBox.Tag != null)
                    {
                        string fieldName = checkBox.Tag.ToString().Split('|')[0];
                        string fieldValue = checkBox.IsChecked.Value.ToString();
                        result &= AddUpdateClinicHistoryAttributeValue(fieldName, fieldValue);
                    }
                }
            }

            return result;
        }

        private bool AddUpdateClinicHistoryAttributeValue(string fieldName, string fieldValue)
        {
            Model.ClinicHistoryAttribute attribute = GetClinicHistoryAttributeValue(fieldName);

            if (attribute == null)
            {
                Model.ClinicHistoryAttribute newAttribute = new Model.ClinicHistoryAttribute()
                {
                    ClinicHistoryId = _patient.ClinicHistoryId.Value,
                    Name = fieldName,
                    Value = fieldValue
                };

                return Controllers.BusinessController.Instance.Add<Model.ClinicHistoryAttribute>(newAttribute);
            }
            else
            {
                attribute.Value = fieldValue;
                return Controllers.BusinessController.Instance.Update<Model.ClinicHistoryAttribute>(attribute);
            }
            
        }

        private void FillGeneralInfo()
        {
            txtExpNo.ToolTip = txtExpNo.Text = _patient.AssignedId.ToString();
            txtFullName.ToolTip = txtFullName.Text = _patient.FirstName + " " + _patient.LastName;
            txtHomePhone.ToolTip = txtHomePhone.Text = _patient.HomePhone;
            txtCellPhone.ToolTip = txtCellPhone.Text = _patient.CellPhone;
            txtEmail.ToolTip = txtEmail.Text = _patient.Email;
        }

        private void FillAllClinicHistoryInfo()
        {
            txtLastUpdateDate.ToolTip = txtLastUpdateDate.Text = _patient.ClinicHistory.UpdateDate.ToString("D");

            List<Grid> grids = GetAllGrids();

            foreach (var item in grids)
            {
                FillGridControlsInfo(item);
            }

            //Load patient picture
            Model.ClinicHistoryAttribute attribute = GetClinicHistoryAttributeValue(Controllers.Utils.PATIENT_PICTURE);
            if (attribute != null && string.IsNullOrEmpty(attribute.Value) == false)
            {
                LoadPatientPicutre(attribute.Value);
            }
        }

        private void LoadPatientPicutre(string fileName)
        {
            try
            {
                imgPatientPicture.Source = new BitmapImage(new Uri(fileName));
                lblImagePath.ToolTip = lblImagePath.Text = fileName;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar la foto del paciente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void FillGridControlsInfo(Grid grid)
        {
            foreach (var item in grid.Children)
            {
                if (item is TextBox)
                {
                    TextBox textField = item as TextBox;
                    if (textField.Tag != null)
                    {
                        string fieldName = textField.Tag.ToString();
                        Model.ClinicHistoryAttribute attribute = GetClinicHistoryAttributeValue(fieldName);

                        textField.Text = attribute == null ? string.Empty : attribute.Value;
                    }
                }
                else if (item is CheckBox)
                {
                    CheckBox checkBox = item as CheckBox;
                    if (checkBox.Tag != null)
                    {
                        string fieldName = checkBox.Tag.ToString().Split('|')[0];
                        Model.ClinicHistoryAttribute attribute = GetClinicHistoryAttributeValue(fieldName);
                        bool isChecked;
                        bool.TryParse(attribute == null ? string.Empty : attribute.Value, out isChecked);

                        checkBox.IsChecked = isChecked;
                    }
                }
            }
        }

        private Model.ClinicHistoryAttribute GetClinicHistoryAttributeValue(string fieldName)
        {
            return Controllers.BusinessController.Instance.FindBy<Model.ClinicHistoryAttribute>(c => c.ClinicHistoryId == _patient.ClinicHistoryId && c.Name == fieldName).FirstOrDefault();
        }

        private List<Grid> GetAllGrids()
        {
            List<Grid> grids = new List<Grid>();

            grids.Add(gdIdentificationData);
            grids.Add(gdHeredofamiliares);
            grids.Add(gdNoPatologicos);
            grids.Add(gdPatologicos);
            grids.Add(gdDolorDisfuncion);

            return grids;
        }

        private TextBox FindTextFieldByTag(string tag)
        {
            List<Grid> grids = GetAllGrids();

            foreach (var grid in grids)
            {
                foreach (var control in grid.Children)
                {
                    if (control is TextBox && (control as TextBox).Tag != null && (control as TextBox).Tag.ToString() == tag)
                    {
                        return control as TextBox;
                    }
                }
            }

            return null;
        }
        #endregion
    }
}