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
	/// Interaction logic for AddEditBudgetsModal.xaml
	/// </summary>
	public partial class AddEditBudgetsModal : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.BudgetDetail> _budgetsViewModel;
        private List<Model.BudgetDetail> _budgetDetailsList = new List<Model.BudgetDetail>();
        private Model.Budget _budgetToUpdate;
        private bool _isUpdateBudget;
        private bool _dataSaved = false;
        #endregion

        #region Constructors
        public AddEditBudgetsModal(Model.Budget budgetToUpdate)
		{
			this.InitializeComponent();

            _budgetToUpdate = budgetToUpdate;
            _isUpdateBudget = _budgetToUpdate != null;

            if (_isUpdateBudget)
            {
                PrepareWindowForUpdates();
            }

            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnFindPatient_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            List<Model.Patient> selectedPatients = new List<Model.Patient>();
            new FindPatientEmailModal(selectedPatients).ShowDialog();

            if (selectedPatients.Count > 0)
            {
                txtPatientName.Text = string.Format("(Exp. No. {0}) {1} {2}", selectedPatients[0].PatientId, selectedPatients[0].FirstName, selectedPatients[0].LastName);
                txtPatientName.Tag = selectedPatients[0];
            }
		}

		private void btnAddBudgetDetail_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.BudgetDetail budgetDetailToAdd = new Model.BudgetDetail()
            {
                BudgetDetailId = -1
            };

            new AddEditBudgetDetailsModal(budgetDetailToAdd).ShowDialog();

            if (budgetDetailToAdd.BudgetDetailId != -1)
            {
                _budgetDetailsList.Add(budgetDetailToAdd);
                UpdateGrid();
            }
		}

		private void btnEditBudgetDetail_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.BudgetDetail budgetDetailSelected = dgBudgetDetails.SelectedItem == null ? null : dgBudgetDetails.SelectedItem as Model.BudgetDetail;

            if (budgetDetailSelected == null)
            {
                MessageBox.Show("Seleccione un concepto del presupuesto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditBudgetDetailsModal(budgetDetailSelected).ShowDialog();
                UpdateGrid();
            }
		}

		private void btnDeleteBudgetDetail_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            Model.BudgetDetail budgetDetailSelected = dgBudgetDetails.SelectedItem == null ? null : dgBudgetDetails.SelectedItem as Model.BudgetDetail;

            if (budgetDetailSelected == null)
            {
                MessageBox.Show("Seleccione un presupuesto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el concepto '{0}'?",
                                        budgetDetailSelected.Concept),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                _budgetDetailsList.Remove(budgetDetailSelected);
                UpdateGrid();
            }
		}

		private void btnAddUpdateBudget_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            string budgetName = txtBudgetName.Text.Trim();

            if (AreValidFields(budgetName) == false)
            {
                return;
            }

            if (_isUpdateBudget)
            {
                _budgetToUpdate.Name = budgetName;
                _budgetToUpdate.ExpiredDate = dtpExpDate.SelectedDate.Value;
                _budgetToUpdate.Notes = txtNotes.Text;
                _budgetToUpdate.GrandTotal = Convert.ToDecimal(txtGrandTotalDiscount.Text);
                _budgetToUpdate.PatientId = (txtPatientName.Tag as Model.Patient).PatientId;

                if (Controllers.BusinessController.Instance.Update<Model.Budget>(_budgetToUpdate))
                {
                    List<Model.BudgetDetail> budgetDetails = Controllers.BusinessController.Instance.FindBy<Model.BudgetDetail>(bd => bd.BudgetId == _budgetToUpdate.BudgetId).ToList();

                    List<Model.BudgetDetail> budgetDetailsToAdd = new List<Model.BudgetDetail>();
                    List<Model.BudgetDetail> budgetDetailsToUpdate = new List<Model.BudgetDetail>();
                    GetBudgetDetailsActions(budgetDetails, budgetDetailsToAdd, budgetDetailsToUpdate);

                    bool budgetDetailsUpdated = true;

                    //Add new budget details
                    foreach (Model.BudgetDetail budgetDetail in budgetDetailsToAdd)
                    {
                        budgetDetail.BudgetId = _budgetToUpdate.BudgetId;
                        budgetDetailsUpdated &= Controllers.BusinessController.Instance.Add<Model.BudgetDetail>(budgetDetail);
                    }

                    //Update budget details
                    foreach (Model.BudgetDetail budgetDetail in budgetDetailsToUpdate)
                    {
                        budgetDetailsUpdated &= Controllers.BusinessController.Instance.Update<Model.BudgetDetail>(budgetDetail);
                    }

                    //Delete budget details
                    foreach (Model.BudgetDetail budgetDetail in budgetDetails)
                    {
                        budgetDetailsUpdated &= Controllers.BusinessController.Instance.Delete<Model.BudgetDetail>(budgetDetail);
                    }

                    if (budgetDetailsUpdated == false)
                    {
                        MessageBox.Show("El presupuesto fue actualizado, pero ocurrió un error al tratar de actualizar los conceptos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        _dataSaved = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el presupuesto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Model.Budget budgetToAdd = new Model.Budget()
                {
                    Name = budgetName,
                    ExpiredDate = dtpExpDate.SelectedDate.Value,
                    Notes = txtNotes.Text,
                    GrandTotal = Convert.ToDecimal(txtGrandTotalDiscount.Text),
                    IsDeleted = false,
                    PatientId = (txtPatientName.Tag as Model.Patient).PatientId
                };

                if (Controllers.BusinessController.Instance.Add<Model.Budget>(budgetToAdd))
                {
                    bool budgetDetailsAdded = true;

                    foreach (Model.BudgetDetail budgetDetail in _budgetDetailsList)
                    {
                        budgetDetail.BudgetId = budgetToAdd.BudgetId;
                        budgetDetailsAdded &= Controllers.BusinessController.Instance.Add<Model.BudgetDetail>(budgetDetail);
                    }

                    if (budgetDetailsAdded == false)
                    {
                        MessageBox.Show("El presupuesto fue creado, pero ocurrió un error al tratar de guardar los conceptos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);    
                    }

                    _dataSaved = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudo crear el presupuesto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
		}

		private void btnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_dataSaved == false && 
                MessageBox.Show("¿Está seguro(a) que desea cerrar esta ventana sin haber guardado los cambios del presupuesto?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) != MessageBoxResult.Yes)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Window's logic
        private void GetBudgetDetailsActions(List<Model.BudgetDetail> budgetDetails, List<Model.BudgetDetail> budgetDetailsToAdd, List<Model.BudgetDetail> budgetDetailsToUpdate)
        {
            foreach (Model.BudgetDetail item in _budgetDetailsList)
            {
                if (item.BudgetDetailId == 0)
                {
                    budgetDetailsToAdd.Add(item);
                }
                else if (budgetDetails.Contains(item))
                {
                    budgetDetails.Remove(item);
                    budgetDetailsToUpdate.Add(item);
                }
            }
        }

        private bool AreValidFields(string budgetName)
        {
            if (string.IsNullOrEmpty(budgetName))
            {
                MessageBox.Show("Ingrese el nombre del presupuesto", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (txtPatientName.Tag == null)
            {
                MessageBox.Show("Seleccione un paciente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (dtpExpDate.SelectedDate == null)
            {
                MessageBox.Show("Seleccione un día para la vigencia", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            DateTime selectedDate = new DateTime(dtpExpDate.SelectedDate.Value.Year,
                                                    dtpExpDate.SelectedDate.Value.Month,
                                                    dtpExpDate.SelectedDate.Value.Day,
                                                    23, 59, 59);
            if (selectedDate < DateTime.Now)
            {
                MessageBox.Show("La fecha seleccionada para la vigencia no puede ser una fecha ya transcurrida", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private void PrepareWindowForUpdates()
        {
            this.Title = "Actualizar información del presupuesto";
            btnAddUpdateBudget.Content = "Actualizar presupuesto";
            txtBudgetName.Text = _budgetToUpdate.Name;
            txtPatientName.ToolTip = txtPatientName.Text = string.Format("(Exp. No. {0}) {1} {2}", _budgetToUpdate.Patient.PatientId, _budgetToUpdate.Patient.FirstName, _budgetToUpdate.Patient.LastName);
            txtPatientName.Tag = _budgetToUpdate.Patient;
            txtNotes.Text = _budgetToUpdate.Notes;
            dtpExpDate.SelectedDate = _budgetToUpdate.ExpiredDate;

            _budgetDetailsList = Controllers.BusinessController.Instance.FindBy<Model.BudgetDetail>(bd => bd.BudgetId == _budgetToUpdate.BudgetId).OrderBy(bd => bd.BudgetDetailId).ToList();
        }

        private void UpdateGrid()
        {
            _budgetsViewModel = new Controllers.CustomViewModel<Model.BudgetDetail>(_budgetDetailsList);
            this.DataContext = _budgetsViewModel;

            txtTotalNumberOfEvents.ToolTip = txtTotalNumberOfEvents.Text = Convert.ToInt32(_budgetDetailsList.Sum(bd => bd.NumberOfEvents)).ToString();
            txtGrandTotal.ToolTip = txtGrandTotal.Text = _budgetDetailsList.Sum(bd => bd.NetTotal).ToString();
            txtGrandTotalDiscount.ToolTip = txtGrandTotalDiscount.Text = _budgetDetailsList.Sum(bd => bd.TotalDiscount).ToString();
        }
        #endregion
    }
}