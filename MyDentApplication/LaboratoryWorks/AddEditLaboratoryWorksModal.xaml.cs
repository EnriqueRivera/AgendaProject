using Controllers;
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
	/// Interaction logic for AddEditLaboratoryWorksModal.xaml
	/// </summary>
	public partial class AddEditLaboratoryWorksModal : Window
	{
        #region Instance variables
        private Model.LaboratoryWork _laboratoryWorkToUpdate;
        private bool _isUpdateLaboratoryWork;
        #endregion

        #region Constructors
        public AddEditLaboratoryWorksModal(Model.LaboratoryWork laboratoryWorkToUpdate)
		{
			this.InitializeComponent();

            _laboratoryWorkToUpdate = laboratoryWorkToUpdate;
            _isUpdateLaboratoryWork = laboratoryWorkToUpdate != null;
            dtpDeliveryDate.SelectedDate = DateTime.Now;
            FillPatients();
            FillTechnicals();

            if (_isUpdateLaboratoryWork)
            {
                PrepareWindowForUpdates();
            }
		}
        #endregion

        #region Window event handlers
        private void btnAddUpdateLaboratoryWork_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            int patientId;
            int technicalId;
            string workName = txtWorkName.Text.Trim();

            if (AreValidFields(workName, out patientId, out technicalId) == false)
            {
                return;
            }

            if (_isUpdateLaboratoryWork)
            {
                _laboratoryWorkToUpdate.PatientId = patientId;
                _laboratoryWorkToUpdate.WorkName = workName;
                _laboratoryWorkToUpdate.DeliveryDate = dtpDeliveryDate.SelectedDate.Value;
                _laboratoryWorkToUpdate.ReceivedDate = dtpReceivedDate.SelectedDate;
                _laboratoryWorkToUpdate.TechnicalId = technicalId;

                UpdateLaboratoryWork(_laboratoryWorkToUpdate);
            }
            else
            {
                Model.LaboratoryWork laboratoryWorkToAdd = new Model.LaboratoryWork()
                {
                    PatientId = patientId,
                    WorkName = workName,
                    DeliveryDate = dtpDeliveryDate.SelectedDate.Value,
                    ReceivedDate = dtpReceivedDate.SelectedDate,
                    TechnicalId = technicalId,
                    IsDeleted = false
                };

                AddLaboratoryWork(laboratoryWorkToAdd);
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}
        #endregion

        #region Window's logic
        private void UpdateLaboratoryWork(Model.LaboratoryWork laboratoryWorkToUpdate)
        {
            if (Controllers.BusinessController.Instance.Update<Model.LaboratoryWork>(laboratoryWorkToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el trabajo de laboratorio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddLaboratoryWork(Model.LaboratoryWork laboratoryWorkToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.LaboratoryWork>(laboratoryWorkToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el trabajo de laboratorio", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillPatients()
        {
            List<Model.Patient> patients = BusinessController.Instance.FindBy<Model.Patient>(p => p.IsDeleted == false)
                                                .OrderBy(p => p.FirstName)
                                                .ThenBy(p => p.LastName)
                                                .ThenBy(p => p.AssignedId)
                                                .ToList();

            foreach (Model.Patient patient in patients)
            {
                cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = string.Format("{1} {2} (Exp. No. {0})", patient.AssignedId, patient.FirstName, patient.LastName), Value = patient });
            }
        }

        private void FillTechnicals()
        {
            List<Model.Technical> technicals = BusinessController.Instance.FindBy<Model.Technical>(t => t.IsDeleted == false)
                                                .OrderBy(t => t.Name)
                                                .ToList();

            foreach (Model.Technical technical in technicals)
            {
                cbTechnical.Items.Add(new Controllers.ComboBoxItem() { Text = technical.Name, Value = technical });
            }
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del trabajo de laboratorio";
            btnAddUpdateLaboratoryWork.Content = "Actualizar";
            txtWorkName.Text = _laboratoryWorkToUpdate.WorkName;
            dtpDeliveryDate.SelectedDate = _laboratoryWorkToUpdate.DeliveryDate;
            chkReceivedWork.IsChecked = _laboratoryWorkToUpdate.ReceivedDate != null;
            dtpReceivedDate.SelectedDate = _laboratoryWorkToUpdate.ReceivedDate;

            //Select patient
            for (int i = 0; i < cbPatients.Items.Count; i++)
            {
                if ((cbPatients.Items[i] as Controllers.ComboBoxItem).Text == _laboratoryWorkToUpdate.Patient.FirstName + " " + _laboratoryWorkToUpdate.Patient.LastName)
                {
                    cbPatients.SelectedIndex = i;
                    break;
                }
            }

            //Select technical
            for (int i = 0; i < cbTechnical.Items.Count; i++)
            {
                if ((cbTechnical.Items[i] as Controllers.ComboBoxItem).Text == _laboratoryWorkToUpdate.Technical.Name)
                {
                    cbTechnical.SelectedIndex = i;
                    break;
                }
            }
        }

        private bool AreValidFields(string workName, out int patientId, out int technicalId)
        {
            patientId = -1;
            technicalId = -1;

            if (cbPatients.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (string.IsNullOrEmpty(workName))
            {
                MessageBox.Show("Ingrese un nombre para el trabajo de laboratorio", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (dtpDeliveryDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha de entrega válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (chkReceivedWork.IsChecked.Value)
            {
                if (dtpReceivedDate.SelectedDate == null)
                {
                    MessageBox.Show("Seleccione una fecha de recepción válida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }

                if (dtpDeliveryDate.SelectedDate.Value.Date > dtpReceivedDate.SelectedDate.Value.Date)
                {
                    MessageBox.Show("La fecha de entrega no puede ser mayor a la fecha de recepción", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }   
            }

            if (cbTechnical.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un técnico", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            patientId = ((cbPatients.SelectedItem as Controllers.ComboBoxItem).Value as Model.Patient).PatientId;
            technicalId = ((cbTechnical.SelectedItem as Controllers.ComboBoxItem).Value as Model.Technical).TechnicalId;

            return true;
        }

        private void chkReceivedWork_CheckedUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (chkReceivedWork.IsChecked.Value)
	        {
		        dtpReceivedDate.IsEnabled = true;
                dtpReceivedDate.SelectedDate = _isUpdateLaboratoryWork && _laboratoryWorkToUpdate.ReceivedDate != null 
                                                ? _laboratoryWorkToUpdate.ReceivedDate 
                                                : DateTime.Now;
	        }
            else
            {
                dtpReceivedDate.IsEnabled = false;
                dtpReceivedDate.SelectedDate = null;
            }
            
        }
        #endregion
	}
}