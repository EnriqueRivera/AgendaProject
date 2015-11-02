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
	/// Interaction logic for ManageTreatmentsWindow.xaml
	/// </summary>
	public partial class ManageTreatmentsWindow : Window
    {
        #region Instance variables
        private Controllers.CustomViewModel<Model.Treatment> _agendaTratmentsViewModel;
        private Controllers.CustomViewModel<Model.BudgetTreatment> _budgetTratmentsViewModel;
        #endregion

        #region Constructors
        public ManageTreatmentsWindow()
		{
			this.InitializeComponent();

            UpdateGridAgendaTreatments();
            UpdateGridBudgetTreatments();
        }
        #endregion

        #region Window event handlers
        private void btnAddTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (tbTreatments.SelectedIndex == 0)
            {
                new AddEditTreatmentsModal(null).ShowDialog();
                UpdateGridAgendaTreatments();    
            }
            else
            {
                new AddEditBudgetTreatmentsModal(null).ShowDialog();
                UpdateGridBudgetTreatments();
            }            
        }

        private void btnEditTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (tbTreatments.SelectedIndex == 0)
            {
                Model.Treatment treatmentSelected = dgAgendaTreatments.SelectedItem == null ? null : dgAgendaTreatments.SelectedItem as Model.Treatment;

                if (treatmentSelected == null)
                {
                    MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    new AddEditTreatmentsModal(treatmentSelected).ShowDialog();
                    UpdateGridAgendaTreatments();
                }
            }
            else
            {
                Model.BudgetTreatment treatmentSelected = dgBudgetTreatments.SelectedItem == null ? null : dgBudgetTreatments.SelectedItem as Model.BudgetTreatment;

                if (treatmentSelected == null)
                {
                    MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    new AddEditBudgetTreatmentsModal(treatmentSelected).ShowDialog();
                    UpdateGridBudgetTreatments();
                }
            }
        }

        private void btnDeleteTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (tbTreatments.SelectedIndex == 0)
            {
                Model.Treatment treatmentSelected = dgAgendaTreatments.SelectedItem == null ? null : dgAgendaTreatments.SelectedItem as Model.Treatment;

                if (treatmentSelected == null)
                {
                    MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (MessageBox.Show
                                    (string.Format("¿Está seguro(a) que desea eliminar el tratamiento '{0}'?",
                                            treatmentSelected.Name),
                                        "Advertencia",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Warning
                                    ) == MessageBoxResult.Yes)
                {
                    treatmentSelected.IsDeleted = true;

                    if (Controllers.BusinessController.Instance.Update<Model.Treatment>(treatmentSelected))
                    {
                        UpdateGridAgendaTreatments();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el tratamiento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                Model.BudgetTreatment treatmentSelected = dgBudgetTreatments.SelectedItem == null ? null : dgBudgetTreatments.SelectedItem as Model.BudgetTreatment;

                if (treatmentSelected == null)
                {
                    MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (MessageBox.Show
                                    (string.Format("¿Está seguro(a) que desea eliminar el tratamiento '{0}'?",
                                            treatmentSelected.Name),
                                        "Advertencia",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Warning
                                    ) == MessageBoxResult.Yes)
                {
                    treatmentSelected.IsDeleted = true;

                    if (Controllers.BusinessController.Instance.Update<Model.BudgetTreatment>(treatmentSelected))
                    {
                        UpdateGridBudgetTreatments();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el tratamiento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            
        }
        #endregion

        #region Window's logic
        private void UpdateGridAgendaTreatments()
        {
            _agendaTratmentsViewModel = new Controllers.CustomViewModel<Model.Treatment>(u => u.IsDeleted == false, "TreatmentId", "asc");
            dgAgendaTreatments.DataContext = _agendaTratmentsViewModel;
        }

        private void UpdateGridBudgetTreatments()
        {
            _budgetTratmentsViewModel = new Controllers.CustomViewModel<Model.BudgetTreatment>(u => u.IsDeleted == false, "BudgetTreatmenttId", "asc");
            dgBudgetTreatments.DataContext = _budgetTratmentsViewModel;
        }
        #endregion
    }
}