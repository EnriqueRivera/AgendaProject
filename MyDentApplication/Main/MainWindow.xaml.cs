using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using Controllers;
using System.Data.Objects;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow : Window
    {
        #region Instance variables
        private Model.User _userLoggedIn;
        private bool _stopCheckEventStatusThread = false;
        private bool _stopCheckRemindersThread = false;
        private int _timeToWaitForFinishedEvents = (1000 * 60) * 15;
        private int _timeToWaitForReminders = (1000 * 60);
        //Windows
        private AgendaWindow _agendaWindow;
        private ManageUsersWindow _manageUsersWindow;
        private ManageRemindersWindow _manageRemindersWindow;
        private FinishedEventsReminderModal _finishedEventsReminderModal;
        private ManageTreatmentsWindow _manageTreatmentsWindow;
        private ManageReceivedInvoicesWindow _manageReceivedInvoicesWindow;
        private ManageOutgoingInvoicesWindow _manageOutgoingInvoicesWindow;
        private ManageProvidersWindow _manageProvidersWindow;
        private ManageTechnicalsWindow _manageTechnicalsWindow;
        private ManageLaboratoryWorksWindow _manageLaboratoryWorksWindow;
        private ManageMedicinesWindow _manageMedicinesWindow;
        private MedicalProofDocument _medicalProofDocumentWindow;
        private TotalInvoicesWindow _totalInvoicesWindow;
        private ManageGeneralPaidsWindow _manageGeneralPaidsWindow;
        private ManageContactsWindow _manageContactsWindow;
        private ManageCleanedMaterialsWindow _manageCleanedMaterialsWindow;
        private SendEmailWindow _sendEmailWindow;
        private ConfigureEmailWindow _configureEmailWindow;
        private ManagePatientsWindow _managePatientsWindow;
        private ManageDotationsWindow _manageDotationsWindow;
        //Threads
        private Thread _checkFinishedEventsThread;
        private Thread _checkRemindersThread;
        #endregion

        #region Delegates
        delegate void OpenFinishedEventsReminderModalDelegate();
        delegate void RefreshRemindersDelegate();
        #endregion

        #region Constructors
        public MainWindow(Model.User userLoggedIn)
        {
            CheckGlobalConfigurations();

            this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            HideButtonsForNonAdminUsers();
            lblLoggedIn.ToolTip = lblLoggedIn.Content = _userLoggedIn.FirstName + " " + _userLoggedIn.LastName;
            lblLoggedIn.FontWeight = _userLoggedIn.IsAdmin ? FontWeights.Bold : lblLoggedIn.FontWeight;

            RefreshMedicinesStackPanel();
            RefreshDotationsStackPanel();

            _checkFinishedEventsThread = new Thread(CheckFinishedEvents);
            _checkFinishedEventsThread.SetApartmentState(ApartmentState.STA);
            _checkFinishedEventsThread.IsBackground = true;
            _checkFinishedEventsThread.Start();

            _checkRemindersThread = new Thread(RefreshReminders);
            _checkRemindersThread.SetApartmentState(ApartmentState.STA);
            _checkRemindersThread.IsBackground = true;
            _checkRemindersThread.Start();
        }
        #endregion

        #region Check App Configuration
        private void CheckGlobalConfigurations()
        {
            bool configurationAddedSuccessfully = CheckSchedulerColorsConfiguration();
            if (configurationAddedSuccessfully == false)
            {
                MessageBox.Show("Alguna configuración inicial de la aplicación no pudo ser creada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckSchedulerColorsConfiguration()
        {
            string cancelEventConfigName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.CANCELED.ToString();
            string exceptionEventConfigName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.EXCEPTION.ToString();
            string completedEventConfigName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.COMPLETED.ToString();
            string patientSkipsEventConfigName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.PATIENT_SKIPS.ToString();
            string pendingEventConfigName = Utils.SCHEDULER_COLOR_CONFIGURATION_PREFIX + EventStatus.PENDING.ToString();

            return
                BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == cancelEventConfigName, new Model.Configuration() { Name = cancelEventConfigName, Value = Brushes.OrangeRed.ToString() })
                & BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == exceptionEventConfigName, new Model.Configuration() { Name = exceptionEventConfigName, Value = Brushes.Yellow.ToString() })
                & BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == completedEventConfigName, new Model.Configuration() { Name = completedEventConfigName, Value = Brushes.Green.ToString() })
                & BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == patientSkipsEventConfigName, new Model.Configuration() { Name = patientSkipsEventConfigName, Value = Brushes.Red.ToString() })
                & BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == pendingEventConfigName, new Model.Configuration() { Name = pendingEventConfigName, Value = Brushes.Orange.ToString() });
        }
        #endregion

        #region Hide/Show options based in roles
        private void HideButtonsForNonAdminUsers()
        {
            if (_userLoggedIn.IsAdmin == false)
            {
                gbAdministration.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        #endregion

        #region Thread logic
        public void CheckFinishedEvents()
        {
            while (_stopCheckEventStatusThread == false)
            {
                OpenFinishedEventsReminderModalFromAnotherThread();

                Thread.Sleep(_timeToWaitForFinishedEvents);
            }
        }

        public void RefreshReminders()
        {
            while (_stopCheckRemindersThread == false)
            {
                RefreshRemindersFromAnotherThread();

                Thread.Sleep(_timeToWaitForReminders);
            }
        }
        #endregion

        #region Logic for closing windows
        private void CloseAllWindows()
        {
            RegisterLoginAction(false, _userLoggedIn.UserId);
            
            CloseWindow(_agendaWindow);
            CloseWindow(_manageUsersWindow);
            CloseWindow(_manageRemindersWindow);
            CloseWindow(_finishedEventsReminderModal);
            CloseWindow(_manageTreatmentsWindow);
            CloseWindow(_manageReceivedInvoicesWindow);
            CloseWindow(_manageOutgoingInvoicesWindow);
            CloseWindow(_manageProvidersWindow);
            CloseWindow(_manageTechnicalsWindow);
            CloseWindow(_manageLaboratoryWorksWindow);
            CloseWindow(_manageMedicinesWindow);
            CloseWindow(_medicalProofDocumentWindow);
            CloseWindow(_totalInvoicesWindow);
            CloseWindow(_manageGeneralPaidsWindow);
            CloseWindow(_manageContactsWindow);
            CloseWindow(_manageCleanedMaterialsWindow);
            CloseWindow(_sendEmailWindow);
            CloseWindow(_configureEmailWindow);
            CloseWindow(_managePatientsWindow);
            CloseWindow(_manageDotationsWindow);

            //Flags for threads
            _stopCheckEventStatusThread = true;
            _stopCheckRemindersThread = true;
        }

        private void CloseWindow(Window windowToClose)
        {
            if (windowToClose != null)
            {
                windowToClose.Close();
            }
        }

        void Window_Closed(object sender, EventArgs e)
        {
            if (sender is AgendaWindow)
            {
                _agendaWindow = null;
            }
            else if (sender is ManageUsersWindow)
            {
                _manageUsersWindow = null;
            }
            else if (sender is ManageRemindersWindow)
            {
                _manageRemindersWindow = null;
                RefreshRemindersStackPanel();
            }
            else if (sender is ManageMedicinesWindow)
            {
                _manageMedicinesWindow = null;
                RefreshMedicinesStackPanel();
            }
            else if (sender is ManageTreatmentsWindow)
            {
                _manageTreatmentsWindow = null;
            }
            else if (sender is ManageReceivedInvoicesWindow)
            {
                _manageReceivedInvoicesWindow = null;
            }
            else if (sender is ManageOutgoingInvoicesWindow)
            {
                _manageOutgoingInvoicesWindow = null;
            }
            else if (sender is TotalInvoicesWindow)
            {
                _totalInvoicesWindow = null;
            }
            else if (sender is ManageProvidersWindow)
            {
                _manageProvidersWindow = null;
            }
            else if (sender is ManageTechnicalsWindow)
            {
                _manageTechnicalsWindow = null;
            }
            else if (sender is ManageLaboratoryWorksWindow)
            {
                _manageLaboratoryWorksWindow = null;
            }
            else if (sender is MedicalProofDocument)
            {
                _medicalProofDocumentWindow = null;
            }
            else if (sender is ManageGeneralPaidsWindow)
            {
                _manageGeneralPaidsWindow = null;
            }
            else if (sender is ManageContactsWindow)
            {
                _manageContactsWindow = null;
            }
            else if (sender is ManageCleanedMaterialsWindow)
            {
                _manageCleanedMaterialsWindow = null;
            }
            else if (sender is SendEmailWindow)
            {
                _sendEmailWindow = null;
            }
            else if (sender is ConfigureEmailWindow)
            {
                _configureEmailWindow = null;
            }
            else if (sender is ManagePatientsWindow)
            {
                _managePatientsWindow = null;
            }
            else if (sender is ManageDotationsWindow)
            {
                _manageDotationsWindow = null;
                RefreshDotationsStackPanel();
            }
            else if (sender is FinishedEventsReminderModal)
            {
                _finishedEventsReminderModal = null;

                List<DateTime> datesUpdated = (sender as FinishedEventsReminderModal).FinishedEvents
                                                .Where(fe => fe.IsCompleted || fe.IsCanceled)
                                                .Select(fe => (new DateTime(fe.StartEvent.Year, fe.StartEvent.Month, fe.StartEvent.Day)))
                                                .Distinct()
                                                .ToList();

                if (datesUpdated.Count > 0 && _agendaWindow != null)
                {
                    _agendaWindow.RepaintSchedulerIfDateModifiedIsSelected(datesUpdated);
                }
            }
        }

        #endregion

        #region Logic used in another window
        public static void RegisterLoginAction(bool isLogin, int userLoggedInId)
        {
            Model.Login login = new Model.Login()
            {
                IsLogin = isLogin,
                LoginDate = DateTime.Now,
                UserId = userLoggedInId
            };

            if (Controllers.BusinessController.Instance.Add<Model.Login>(login) == false)
            {
                MessageBox.Show("No se pudo registrar el " + (isLogin ? "inicio" : "cierre") + " de sesión", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static bool IsValidAdminPassword(Model.User userLoggedIn)
        {
            int adminId;
            return IsValidAdminPassword(userLoggedIn, out adminId);
        }

        public static bool IsValidAdminPassword(Model.User userLoggedIn, out int adminId)
        {
            if (userLoggedIn.IsAdmin)
            {
                adminId = userLoggedIn.UserId;
                return true;
            }

            Model.User userResult = new Model.User();
            new RequestAdminCredentialsModal(userResult).ShowDialog();

            adminId = userResult.UserId;
            return userResult.IsAdmin;
        }
        #endregion

        #region Actions to execute from another thread
        void OpenFinishedEventsReminderModalFromAnotherThread()
        {
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                Dispatcher.Invoke(new OpenFinishedEventsReminderModalDelegate(OpenFinishedEventsReminderModalFromAnotherThread));
                return;
            }

            if (_finishedEventsReminderModal != null)
            {
                _finishedEventsReminderModal.Close();
            }

            OpenFinishedEventsReminderModal();
        }

        void RefreshRemindersFromAnotherThread()
        {
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                Dispatcher.Invoke(new RefreshRemindersDelegate(RefreshRemindersFromAnotherThread));
                return;
            }
                        
            RefreshRemindersStackPanel();
        }
        #endregion

        #region Reminders
        private void RefreshRemindersStackPanel()
        {
            int pendingReminders = 0;
            int seenReminders = 0;

            List<Model.Reminder> todayReminders = BusinessController.Instance.FindBy<Model.Reminder>
                                                    (r => EntityFunctions.TruncateTime(r.AppearDate) == EntityFunctions.TruncateTime(DateTime.Now))
                                                    .OrderBy(r => r.AppearDate)
                                                    .ToList();

            DisplayReminders(todayReminders);

            int todayRemindersCount = todayReminders.Count;
            int spRemindersCount = spReminders.Children.Count;

            if (todayRemindersCount > spRemindersCount)
            {
                for (int i = 0; i < todayRemindersCount - spRemindersCount; i++)
                {
                    spReminders.Children.Add(new ViewReminderControl());
                }
            }
            else if (todayRemindersCount < spRemindersCount)
            {
                spReminders.Children.RemoveRange(0, spRemindersCount - todayRemindersCount);
            }

            for (int i = 0; i < todayRemindersCount; i++)
            {
                ViewReminderControl reminderControl = (spReminders.Children[i] as ViewReminderControl);

                reminderControl.Reminder = todayReminders[i];

                reminderControl.Margin = new Thickness(0.0, 0.0, 0.0, 1.0);

                if (todayReminders[i].Seen)
                {
                    seenReminders++;
                }
                else
                {
                    pendingReminders++;
                }
            }

            lblPendingReminders.ToolTip = lblPendingReminders.Content = "Pendientes (" + pendingReminders + ")";
            lblSeenReminders.ToolTip = lblSeenReminders.Content = "Mostrados (" + seenReminders + ")";
        }

        private void DisplayReminders(List<Model.Reminder> todayReminders)
        {
            List<Model.Reminder> remindersToDisplay = todayReminders
                                                        .Where(r => r.AppearDate <= DateTime.Now && r.Seen == false)
                                                        .OrderBy(r => r.AppearDate)
                                                        .ToList();

            foreach (Model.Reminder reminder in remindersToDisplay)
            {
                this.WindowState = this.WindowState == WindowState.Minimized ? WindowState.Normal : this.WindowState;
                new ShowPendingReminderModal(reminder, _userLoggedIn).ShowDialog();
            }
        }
        #endregion

        #region Medicines
        private void RefreshMedicinesStackPanel()
        {
            int expiredMedicines = 0;
            int replacedMedicines = 0;
            DateTime today = DateTime.Now;

            List<Model.Medicine> monthlyMedicines = BusinessController.Instance.FindBy<Model.Medicine>
                                                    (m => m.IsDeleted == false && m.ExpiredDate.Year == today.Year && m.ExpiredDate.Month == today.Month)
                                                    .OrderBy(m => m.WasReplaced == false)
                                                    .ThenBy(m => m.Name)
                                                    .ToList();

            int medicinesCount = monthlyMedicines.Count;
            int spMedicinesCount = spMedicines.Children.Count;

            if (medicinesCount > spMedicinesCount)
            {
                for (int i = 0; i < medicinesCount - spMedicinesCount; i++)
                {
                    ViewMedicineControl viewMedicineControl = new ViewMedicineControl();
                    viewMedicineControl.OnMedicineUpdated += viewMedicineControl_OnMedicineUpdated;

                    spMedicines.Children.Add(viewMedicineControl);
                }
            }
            else if (medicinesCount < spMedicinesCount)
            {
                spMedicines.Children.RemoveRange(0, spMedicinesCount - medicinesCount);
            }

            for (int i = 0; i < medicinesCount; i++)
            {
                ViewMedicineControl medicineControl = (spMedicines.Children[i] as ViewMedicineControl);

                medicineControl.Medicine = monthlyMedicines[i];

                medicineControl.Margin = new Thickness(0.0, 0.0, 0.0, 1.0);

                if (monthlyMedicines[i].WasReplaced)
                {
                    replacedMedicines++;
                }
                else
                {
                    expiredMedicines++;
                }
            }

            lblExpiredMedicine.ToolTip = lblExpiredMedicine.Content = "Sin reemplazar (" + expiredMedicines + ")";
            lblReplacedMedicine.ToolTip = lblReplacedMedicine.Content = "Reemplazado (" + replacedMedicines + ")";
        }

        void viewMedicineControl_OnMedicineUpdated(object sender, bool e)
        {
            RefreshMedicinesStackPanel();
        }
        #endregion

        #region Dotations
        private void RefreshDotationsStackPanel()
        {
            int pendingDotations = 0;
            int signedDotations = 0;

            List<Model.Dotation> dailyDotations = BusinessController.Instance.FindBy<Model.Dotation>
                                                    (d => EntityFunctions.TruncateTime(d.DotationDate) == EntityFunctions.TruncateTime(DateTime.Now))
                                                    .OrderBy(m => m.DotationDate)
                                                    .ToList();

            int dotationsCount = dailyDotations.Count;
            int spDotationsCount = spDotations.Children.Count;

            if (dotationsCount > spDotationsCount)
            {
                for (int i = 0; i < dotationsCount - spDotationsCount; i++)
                {
                    ViewDotationControl viewDotationControl = new ViewDotationControl();
                    viewDotationControl.OnDotationUpdated += viewDotationControl_OnDotationUpdated;

                    spDotations.Children.Add(viewDotationControl);
                }
            }
            else if (dotationsCount < spDotationsCount)
            {
                spDotations.Children.RemoveRange(0, spDotationsCount - dotationsCount);
            }

            for (int i = 0; i < dotationsCount; i++)
            {
                ViewDotationControl dotationControl = (spDotations.Children[i] as ViewDotationControl);

                dotationControl.Dotation = dailyDotations[i];

                dotationControl.Margin = new Thickness(0.0, 0.0, 0.0, 1.0);

                if (dailyDotations[i].UserId != null)
                {
                    signedDotations++;
                }
                else
                {
                    pendingDotations++;
                }
            }

            lblPendingDotations.ToolTip = lblPendingDotations.Content = "Sin firmar (" + pendingDotations + ")";
            lblSignedDotations.ToolTip = lblSignedDotations.Content = "Firmadas (" + signedDotations + ")";
        }

        void viewDotationControl_OnDotationUpdated(object sender, bool e)
        {
            RefreshDotationsStackPanel();
        }
        #endregion

        #region Finished events
        public void OpenFinishedEventsReminderModal()
        {
            List<Model.Event> finishedEvents = Controllers.BusinessController.Instance.FindBy<Model.Event>
                                                    (e => e.EndEvent <= DateTime.Now && e.IsCanceled == false && e.IsCompleted == false)
                                                    .OrderBy(e => e.EndEvent)
                                                    .ToList();

            if (finishedEvents != null && finishedEvents.Count > 0)
            {
                this.WindowState = this.WindowState == WindowState.Minimized ? WindowState.Normal : this.WindowState;
                _finishedEventsReminderModal = new FinishedEventsReminderModal(finishedEvents, _userLoggedIn);
                _finishedEventsReminderModal.Closed += Window_Closed;
                _finishedEventsReminderModal.Show();
            }
        }
        #endregion

        #region Window event handlers
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro(a) que desea cerrar sesión?",
                                    "Advertencia",
                                    MessageBoxButton.YesNo,
                                    MessageBoxImage.Warning
                                ) == MessageBoxResult.Yes)
            {
                CloseAllWindows();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnChangePassword_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new ChangePasswordModal(_userLoggedIn).ShowDialog();
        }

        private void btnManageUsers_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageUsersWindow == null)
            {
                _manageUsersWindow = new ManageUsersWindow();
                _manageUsersWindow.Closed += Window_Closed;
            }

            _manageUsersWindow.Show();
            _manageUsersWindow.WindowState = WindowState.Normal;
        }

        private void btnOpenAgenda_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_agendaWindow == null)
            {
                _agendaWindow = new AgendaWindow(_userLoggedIn);
                _agendaWindow.Closed += Window_Closed;
            }

            _agendaWindow.Show();
            _agendaWindow.WindowState = _agendaWindow.WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Maximized;
        }

        private void btnManageReminders_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageRemindersWindow == null)
            {
                _manageRemindersWindow = new ManageRemindersWindow();
                _manageRemindersWindow.Closed += Window_Closed;
            }

            _manageRemindersWindow.Show();
            _manageRemindersWindow.WindowState = WindowState.Normal;
        }

        private void btnManageTreatments_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageTreatmentsWindow == null)
            {
                _manageTreatmentsWindow = new ManageTreatmentsWindow();
                _manageTreatmentsWindow.Closed += Window_Closed;
            }

            _manageTreatmentsWindow.Show();
            _manageTreatmentsWindow.WindowState = WindowState.Normal;
        }

        private void btnLogOut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnManageReceivedInvoices_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageReceivedInvoicesWindow == null)
            {
                _manageReceivedInvoicesWindow = new ManageReceivedInvoicesWindow();
                _manageReceivedInvoicesWindow.Closed += Window_Closed;
            }

            _manageReceivedInvoicesWindow.Show();
            _manageReceivedInvoicesWindow.WindowState = WindowState.Normal;
        }

        private void btnManageOutgoingInvoices_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageOutgoingInvoicesWindow == null)
            {
                _manageOutgoingInvoicesWindow = new ManageOutgoingInvoicesWindow();
                _manageOutgoingInvoicesWindow.Closed += Window_Closed;
            }

            _manageOutgoingInvoicesWindow.Show();
            _manageOutgoingInvoicesWindow.WindowState = WindowState.Normal;
        }

        private void btnTotalInvoices_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_totalInvoicesWindow == null)
            {
                _totalInvoicesWindow = new TotalInvoicesWindow();
                _totalInvoicesWindow.Closed += Window_Closed;
            }

            _totalInvoicesWindow.Show();
            _totalInvoicesWindow.WindowState = WindowState.Normal;
        }

        private void btnManageProviders_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageProvidersWindow == null)
            {
                _manageProvidersWindow = new ManageProvidersWindow();
                _manageProvidersWindow.Closed += Window_Closed;
            }

            _manageProvidersWindow.Show();
            _manageProvidersWindow.WindowState = WindowState.Normal;
        }

        private void btnManageTechnicals_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageTechnicalsWindow == null)
            {
                _manageTechnicalsWindow = new ManageTechnicalsWindow();
                _manageTechnicalsWindow.Closed += Window_Closed;
            }

            _manageTechnicalsWindow.Show();
            _manageTechnicalsWindow.WindowState = WindowState.Normal;
        }

        private void btnManageLaboratoryWorks_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageLaboratoryWorksWindow == null)
            {
                _manageLaboratoryWorksWindow = new ManageLaboratoryWorksWindow();
                _manageLaboratoryWorksWindow.Closed += Window_Closed;
            }

            _manageLaboratoryWorksWindow.Show();
            _manageLaboratoryWorksWindow.WindowState = WindowState.Normal;
        }
        
        private void btnManageMedicines_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageMedicinesWindow == null)
            {
                _manageMedicinesWindow = new ManageMedicinesWindow(_userLoggedIn);
                _manageMedicinesWindow.Closed += Window_Closed;
            }

            _manageMedicinesWindow.Show();
            _manageMedicinesWindow.WindowState = WindowState.Normal;
        }

        private void btnPrintDocuments_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_medicalProofDocumentWindow == null)
            {
                _medicalProofDocumentWindow = new MedicalProofDocument();
                _medicalProofDocumentWindow.Closed += Window_Closed;
            }

            _medicalProofDocumentWindow.Show();
            _medicalProofDocumentWindow.WindowState = WindowState.Normal;
        }

        private void btnGeneralPaids_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageGeneralPaidsWindow == null)
            {
                _manageGeneralPaidsWindow = new ManageGeneralPaidsWindow();
                _manageGeneralPaidsWindow.Closed += Window_Closed;
            }

            _manageGeneralPaidsWindow.Show();
            _manageGeneralPaidsWindow.WindowState = WindowState.Normal;
        }

        private void btnManageContacts_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageContactsWindow == null)
            {
                _manageContactsWindow = new ManageContactsWindow();
                _manageContactsWindow.Closed += Window_Closed;
            }

            _manageContactsWindow.Show();
            _manageContactsWindow.WindowState = WindowState.Normal;
        }

        private void btnManageCleanedMaterials_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageCleanedMaterialsWindow == null)
            {
                _manageCleanedMaterialsWindow = new ManageCleanedMaterialsWindow(_userLoggedIn);
                _manageCleanedMaterialsWindow.Closed += Window_Closed;
            }

            _manageCleanedMaterialsWindow.Show();
            _manageCleanedMaterialsWindow.WindowState = WindowState.Normal;
        }

        private void btnSendMails_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_sendEmailWindow == null)
            {
                _sendEmailWindow = new SendEmailWindow();
                _sendEmailWindow.Closed += Window_Closed;
            }

            _sendEmailWindow.Show();
            _sendEmailWindow.WindowState = WindowState.Normal;
        }

        private void btnConfigureEmail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_configureEmailWindow == null)
            {
                _configureEmailWindow = new ConfigureEmailWindow();
                _configureEmailWindow.Closed += Window_Closed;
            }

            _configureEmailWindow.Show();
            _configureEmailWindow.WindowState = WindowState.Normal;
        }

        private void btnManagePatients_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_managePatientsWindow == null)
            {
                _managePatientsWindow = new ManagePatientsWindow(_userLoggedIn);
                _managePatientsWindow.Closed += Window_Closed;
            }

            _managePatientsWindow.Show();
            _managePatientsWindow.WindowState = WindowState.Normal;
        }

        private void btnManageDotations_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_manageDotationsWindow == null)
            {
                _manageDotationsWindow = new ManageDotationsWindow();
                _manageDotationsWindow.Closed += Window_Closed;
            }

            _manageDotationsWindow.Show();
            _manageDotationsWindow.WindowState = WindowState.Normal;
        }
        #endregion
    }
}