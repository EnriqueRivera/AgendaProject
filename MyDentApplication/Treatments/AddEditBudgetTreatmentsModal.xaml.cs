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
	/// Interaction logic for AddEditBudgetTreatmentsModal.xaml
	/// </summary>
	public partial class AddEditBudgetTreatmentsModal : Window
	{
        #region Instance variables
        private Model.BudgetTreatment _treatmentToUpdate;
        private bool _isUpdateTreatment;
        #endregion

        #region Constructors
        public AddEditBudgetTreatmentsModal(Model.BudgetTreatment treatmentToUpdate)
		{
			this.InitializeComponent();

            _treatmentToUpdate = treatmentToUpdate;
            _isUpdateTreatment = _treatmentToUpdate != null;

            if (_isUpdateTreatment)
            {
                PrepareWindowForUpdates();
            }
        }
        #endregion

        #region Window event handlers
        private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddUpdateTreatments_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string treatmentName = txtTreatmentName.Text.Trim();
            if (string.IsNullOrEmpty(treatmentName))
            {
                MessageBox.Show("Ingrese un nombre para el tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Controllers.BusinessController.Instance.FindBy<Model.BudgetTreatment>(bt => bt.Name == treatmentName).Count() > 0)
            {
                MessageBox.Show("Ese tratamiento ya existe, ingrese otro nombre", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (_isUpdateTreatment)
            {
                string oldConcept = _treatmentToUpdate.Name;

                if (oldConcept == treatmentName)
                {
                    this.Close();
                }
                else
                {
                    _treatmentToUpdate.Name = treatmentName;

                    UpdateTreatment(_treatmentToUpdate, oldConcept);    
                }
                
            }
            else
            {
                Model.BudgetTreatment treatmentToAdd = new Model.BudgetTreatment()
                {
                    Name = treatmentName,
                    IsDeleted = false
                };

                AddTreatment(treatmentToAdd);
            }
        }
        #endregion

        #region Window's logic
        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar tratamiento para presupuesto";
            btnAddUpdateTreatments.Content = "Actualizar";
            txtTreatmentName.Text = _treatmentToUpdate.Name;
        }

        private void AddTreatment(Model.BudgetTreatment treatmentToAdd)
        {
            if (Controllers.BusinessController.Instance.Add<Model.BudgetTreatment>(treatmentToAdd))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el tratamiento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateTreatment(Model.BudgetTreatment treatmentToUpdate, string oldConcept)
        {
            if (UpdateBudgetDetailsConcept(treatmentToUpdate.Name, oldConcept) && 
                Controllers.BusinessController.Instance.Update<Model.BudgetTreatment>(treatmentToUpdate))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el tratamiento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool UpdateBudgetDetailsConcept(string newConcept, string oldConcept)
        {
            List<Model.BudgetDetail> conceptsToUpdate = Controllers.BusinessController.Instance.FindBy<Model.BudgetDetail>(bd => bd.Concept == oldConcept).ToList();
            bool conceptsUpdated = true;

            foreach (Model.BudgetDetail budgetDetail in conceptsToUpdate)
            {
                budgetDetail.Concept = newConcept;
                conceptsUpdated &= Controllers.BusinessController.Instance.Update<Model.BudgetDetail>(budgetDetail);
            }

            return conceptsUpdated;
        }
        #endregion
    }
}