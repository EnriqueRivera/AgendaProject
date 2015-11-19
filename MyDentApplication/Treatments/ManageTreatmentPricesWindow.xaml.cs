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
using Controllers;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ManageTreatmentPricesWindow.xaml
	/// </summary>
	public partial class ManageTreatmentPricesWindow : Window
	{
        #region Instance variables
        private CustomViewModel<Model.TreatmentPrice> _dentristyViewModel;
        private CustomViewModel<Model.TreatmentPrice> _painClinicViewModel;
        private CustomViewModel<Model.TreatmentPrice> _painClinicHIViewModel;
        private CustomViewModel<Model.TreatmentPrice> _endodonticsViewModel;
        private CustomViewModel<Model.TreatmentPrice> _endodonticsHIViewModel;
        private CustomViewModel<Model.TreatmentPrice> _orthodonticsViewModel;
        private CustomViewModel<Model.TreatmentPrice> _orthodonticsHIViewModel;
        private CustomViewModel<Model.TreatmentPrice> _cmfViewModel;
        private CustomViewModel<Model.TreatmentPrice> _cmfHIViewModel;
        private CustomViewModel<Model.TreatmentPrice> _periodonticsViewModel;
        private CustomViewModel<Model.TreatmentPrice> _periodonticsHIViewModel;
        private CustomViewModel<Model.TreatmentPrice> _pediatricDentalViewModel;
        private CustomViewModel<Model.TreatmentPrice> _pediatricDentalHIViewModel;
        private Model.User _userLoggedIn;
        #endregion

        #region Constructors
        public ManageTreatmentPricesWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            dtudSelectedYear.Value = DateTime.Now;
            UpdateAllGrid();
        }
        #endregion

        #region Window event handlers
        private void btnAddTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (tcTratments.SelectedIndex)
            {
                case 0:
                    new AddEditTreatmentPricesModal(null, Utils.TREATMENT_DENTISTRY).ShowDialog();
                    break;
                case 1:
                    new AddEditTreatmentPricesModal(null, Utils.TREATMENT_PAIN_CLINIC + (tcPainClinic.SelectedIndex == 0 ? string.Empty : Utils.TREATMENT_HEALTH_INSURANCE)).ShowDialog();
                    break;
                case 2:
                    new AddEditTreatmentPricesModal(null, Utils.TREATMENT_ENDODONTICS + (tcEndodontics.SelectedIndex == 0 ? string.Empty : Utils.TREATMENT_HEALTH_INSURANCE)).ShowDialog();
                    break;
                case 3:
                    new AddEditTreatmentPricesModal(null, Utils.TREATMENT_ORTHODONTICS + (tcOrthodontics.SelectedIndex == 0 ? string.Empty : Utils.TREATMENT_HEALTH_INSURANCE)).ShowDialog();
                    break;
                case 4:
                    new AddEditTreatmentPricesModal(null, Utils.TREATMENT_CMF + (tcCmf.SelectedIndex == 0 ? string.Empty : Utils.TREATMENT_HEALTH_INSURANCE)).ShowDialog();
                    break;
                case 5:
                    new AddEditTreatmentPricesModal(null, Utils.TREATMENT_PERIODONTICS + (tcPeriodontics.SelectedIndex == 0 ? string.Empty : Utils.TREATMENT_HEALTH_INSURANCE)).ShowDialog();
                    break;
                case 6:
                    new AddEditTreatmentPricesModal(null, Utils.TREATMENT_PEDIATRIC_DENTAL + (tcPediatricDental.SelectedIndex == 0 ? string.Empty : Utils.TREATMENT_HEALTH_INSURANCE)).ShowDialog();
                    break;
                default:
                    break;
            }

            dtudSelectedYear.Value = DateTime.Now;
            UpdateCurrentGrid();
        }

        private void btnEditTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGrid auxGrid = GetCurrentDataGrid();

            Model.TreatmentPrice treatmentSelected = auxGrid.SelectedItem == null ? null : auxGrid.SelectedItem as Model.TreatmentPrice;

            if (treatmentSelected == null)
            {
                MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (treatmentSelected.CreatedDate.Year != DateTime.Now.Year)
            {
                MessageBox.Show("No puede editar un tratamiento que no sea del año en curso", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                new AddEditTreatmentPricesModal(treatmentSelected, tcTratments.SelectedIndex == 0 ? Controllers.Utils.TREATMENT_DENTISTRY : string.Empty).ShowDialog();

                dtudSelectedYear.Value = DateTime.Now;
                UpdateCurrentGrid();
            }
        }

        private void btnDeleteTreatment_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DataGrid auxGrid = GetCurrentDataGrid();

            Model.TreatmentPrice treatmentSelected = auxGrid.SelectedItem == null ? null : auxGrid.SelectedItem as Model.TreatmentPrice;

            if (treatmentSelected == null)
            {
                MessageBox.Show("Seleccione un tratamiento", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (treatmentSelected.CreatedDate.Year != DateTime.Now.Year)
            {
                MessageBox.Show("No puede eliminar un tratamiento que no sea del año en curso", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (MessageBox.Show
                                (string.Format("¿Está seguro(a) que desea eliminar el tratamiento con clave '{0}'?",
                                        treatmentSelected.TreatmentKey),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                treatmentSelected.IsDeleted = true;

                if (BusinessController.Instance.Update<Model.TreatmentPrice>(treatmentSelected))
                {
                    dtudSelectedYear.Value = DateTime.Now;
                    UpdateCurrentGrid();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el tratamiento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCopyPrices_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int currentYear = DateTime.Now.Year;

            if (MessageBox.Show(string.Format("¿Está seguro(a) que desea copiar todos los precios de los tratamientos del año '{0}' a el año en curso ({1})?",
                                    currentYear - 1, currentYear),
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes 
                                && MainWindow.IsValidAdminPassword(_userLoggedIn))
            {
                CopyAllPricesOfLastYear();
            }
        }

        private void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateAllGrid();
        }
        #endregion

        #region Window's logic
        private void CopyAllPricesOfLastYear()
        {
            bool treatmentsAdded = true;
            List<Model.TreatmentPrice> allTreatments = BusinessController.Instance.FindBy<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == (DateTime.Now.Year - 1)).ToList();

            foreach (Model.TreatmentPrice treatmentToCopy in allTreatments)
            {
                Model.TreatmentPrice newTreatmentPrice = new Model.TreatmentPrice()
                {
                    CreatedDate = DateTime.Now,
                    Discount = treatmentToCopy.Discount,
                    IsDeleted = false,
                    Name = treatmentToCopy.Name,
                    Price = treatmentToCopy.Price,
                    TreatmentKey = treatmentToCopy.TreatmentKey,
                    Type = treatmentToCopy.Type
                };

                treatmentsAdded &= BusinessController.Instance.Add<Model.TreatmentPrice>(newTreatmentPrice);
            }

            dtudSelectedYear.Value = DateTime.Now;
            UpdateAllGrid();

            if (treatmentsAdded)
            {
                MessageBox.Show("Tratamientos copiados", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al tratar de copiar los precios de los tratamientos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DataGrid GetCurrentDataGrid()
        {
            switch (tcTratments.SelectedIndex)
            {
                case 0:
                    return dgDentristy;
                case 1:
                    return tcPainClinic.SelectedIndex == 0 ? dgPainClinic : dgPainClinicHI;
                case 2:
                    return tcEndodontics.SelectedIndex == 0 ? dgEndodontics : dgEndodonticsHI;
                case 3:
                    return tcOrthodontics.SelectedIndex == 0 ? dgOrthodontics : dgOrthodonticsHI;
                case 4:
                    return tcCmf.SelectedIndex == 0 ? dgCmf : dgCmfHI;
                case 5:
                    return tcPeriodontics.SelectedIndex == 0 ? dgPeriodontics : dgPeriodonticsHI;
                case 6:
                    return tcPediatricDental.SelectedIndex == 0 ? dgPediatricDental : dgPediatricDentalHI;
                default:
                    return null;
            }
        }

        private void UpdateCurrentGrid()
        {
            int year = dtudSelectedYear.Value.Value.Year;

            switch (tcTratments.SelectedIndex)
            {
                case 0:
                    _dentristyViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_DENTISTRY, "TreatmentKey", "asc");
                    dgDentristy.DataContext = _dentristyViewModel;
                    break;
                case 1:
                    if (tcPainClinic.SelectedIndex == 0)
                    {
                        _painClinicViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_PAIN_CLINIC, "TreatmentKey", "asc");
                        dgPainClinic.DataContext = _painClinicViewModel;
                    }
                    else
                    {
                        _painClinicHIViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_PAIN_CLINIC + Utils.TREATMENT_HEALTH_INSURANCE, "TreatmentKey", "asc");
                        dgPainClinicHI.DataContext = _painClinicHIViewModel;
                    }
                    break;
                case 2:
                    if (tcEndodontics.SelectedIndex == 0)
                    {
                        _endodonticsViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_ENDODONTICS, "TreatmentKey", "asc");
                        dgEndodontics.DataContext = _endodonticsViewModel;
                    }
                    else
                    {
                        _endodonticsHIViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_ENDODONTICS + Utils.TREATMENT_HEALTH_INSURANCE, "TreatmentKey", "asc");
                        dgEndodonticsHI.DataContext = _endodonticsHIViewModel;
                    }
                    break;
                case 3:
                    if (tcOrthodontics.SelectedIndex == 0)
                    {
                        _orthodonticsViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_ORTHODONTICS, "TreatmentKey", "asc");
                        dgOrthodontics.DataContext = _orthodonticsViewModel;
                    }
                    else
                    {
                        _orthodonticsHIViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_ORTHODONTICS + Utils.TREATMENT_HEALTH_INSURANCE, "TreatmentKey", "asc");
                        dgOrthodonticsHI.DataContext = _orthodonticsHIViewModel;
                    }
                    break;
                case 4:
                    if (tcCmf.SelectedIndex == 0)
                    {
                        _cmfViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_CMF, "TreatmentKey", "asc");
                        dgCmf.DataContext = _cmfViewModel;
                    }
                    else
                    {
                        _cmfHIViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_CMF + Utils.TREATMENT_HEALTH_INSURANCE, "TreatmentKey", "asc");
                        dgCmfHI.DataContext = _cmfHIViewModel;
                    }
                    break;
                case 5:
                    if (tcPeriodontics.SelectedIndex == 0)
                    {
                        _periodonticsViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_PERIODONTICS, "TreatmentKey", "asc");
                        dgPeriodontics.DataContext = _periodonticsViewModel;
                    }
                    else
                    {
                        _periodonticsHIViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_PERIODONTICS + Utils.TREATMENT_HEALTH_INSURANCE, "TreatmentKey", "asc");
                        dgPeriodonticsHI.DataContext = _periodonticsHIViewModel;
                    }
                    break;
                case 6:
                    if (tcPediatricDental.SelectedIndex == 0)
                    {
                        _pediatricDentalViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_PEDIATRIC_DENTAL, "TreatmentKey", "asc");
                        dgPediatricDental.DataContext = _pediatricDentalViewModel;
                    }
                    else
                    {
                        _pediatricDentalHIViewModel = new CustomViewModel<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year && t.Type == Utils.TREATMENT_PEDIATRIC_DENTAL + Utils.TREATMENT_HEALTH_INSURANCE, "TreatmentKey", "asc");
                        dgPediatricDentalHI.DataContext = _pediatricDentalHIViewModel;
                    }
                    break;
                default:
                    break;
            }
        }

        private void UpdateAllGrid()
        {
            int year = dtudSelectedYear.Value.Value.Year;

            List<Model.TreatmentPrice> allTreatments = BusinessController.Instance.FindBy<Model.TreatmentPrice>(t => t.IsDeleted == false && t.CreatedDate.Year == year).ToList();

            _dentristyViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_DENTISTRY).ToList());

            _painClinicViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_PAIN_CLINIC).ToList());
            _painClinicHIViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_PAIN_CLINIC + Utils.TREATMENT_HEALTH_INSURANCE).ToList());

            _endodonticsViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_ENDODONTICS).ToList());
            _endodonticsHIViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_ENDODONTICS + Utils.TREATMENT_HEALTH_INSURANCE).ToList());

            _orthodonticsViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_ORTHODONTICS).ToList());
            _orthodonticsHIViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_ORTHODONTICS + Utils.TREATMENT_HEALTH_INSURANCE).ToList());

            _cmfViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_CMF).ToList());
            _cmfHIViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_CMF + Utils.TREATMENT_HEALTH_INSURANCE).ToList());

            _periodonticsViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_PERIODONTICS).ToList());
            _periodonticsHIViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_PERIODONTICS + Utils.TREATMENT_HEALTH_INSURANCE).ToList());

            _pediatricDentalViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_PEDIATRIC_DENTAL).ToList());
            _pediatricDentalHIViewModel = new CustomViewModel<Model.TreatmentPrice>(allTreatments.Where(t => t.Type == Utils.TREATMENT_PEDIATRIC_DENTAL + Utils.TREATMENT_HEALTH_INSURANCE).ToList());

            dgDentristy.DataContext = _dentristyViewModel;

            dgPainClinic.DataContext = _painClinicViewModel;
            dgPainClinicHI.DataContext = _painClinicHIViewModel;

            dgEndodontics.DataContext = _endodonticsViewModel;
            dgEndodonticsHI.DataContext = _endodonticsHIViewModel;

            dgOrthodontics.DataContext = _orthodonticsViewModel;
            dgOrthodonticsHI.DataContext = _orthodonticsHIViewModel;

            dgCmf.DataContext = _cmfViewModel;
            dgCmfHI.DataContext = _cmfHIViewModel;

            dgPeriodontics.DataContext = _periodonticsViewModel;
            dgPeriodonticsHI.DataContext = _periodonticsHIViewModel;

            dgPediatricDental.DataContext = _pediatricDentalViewModel;
            dgPediatricDentalHI.DataContext = _pediatricDentalHIViewModel;
        }
        #endregion
    }
}