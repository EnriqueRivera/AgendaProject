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
        //Threads
        private Thread _checkFinishedEventsThread;
        private Thread _checkRemindersThread;
        //Delegates
        delegate void OpenFinishedEventsReminderModalDelegate();
        delegate void RefreshRemindersDelegate();

        public MainWindow(Model.User userLoggedIn)
        {
            CheckGlobalConfigurations();

            this.InitializeComponent();

            _userLoggedIn = userLoggedIn;
            HideButtonsForNonAdminUsers();
            lblLoggedIn.ToolTip = lblLoggedIn.Content = _userLoggedIn.FirstName + " " + _userLoggedIn.LastName;
            lblLoggedIn.FontWeight = _userLoggedIn.IsAdmin ? FontWeights.Bold : lblLoggedIn.FontWeight;

            _checkFinishedEventsThread = new Thread(CheckFinishedEvents);
            _checkFinishedEventsThread.SetApartmentState(ApartmentState.STA);
            _checkFinishedEventsThread.IsBackground = true;
            _checkFinishedEventsThread.Start();

            _checkRemindersThread = new Thread(RefreshReminders);
            _checkRemindersThread.SetApartmentState(ApartmentState.STA);
            _checkRemindersThread.IsBackground = true;
            _checkRemindersThread.Start();
        }

        private void CheckGlobalConfigurations()
        {
            bool configurationAddedSuccessfully = CheckSchedulerColorsConfiguration() && CheckMaxSkipsEventsConfiguration();
            if (configurationAddedSuccessfully == false)
            {
                MessageBox.Show("Alguna configuración inicial de la aplicación no pudo ser creada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckMaxSkipsEventsConfiguration()
        {
            return BusinessController.Instance.AddIfDoesntExist<Model.Configuration>(c => c.Name == Utils.PATIENT_MAX_SKIPPED_EVENTS_CONFIGURATION, new Model.Configuration() { Name = Utils.PATIENT_MAX_SKIPPED_EVENTS_CONFIGURATION, Value = "3" });
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

        private void HideButtonsForNonAdminUsers()
        {
            if (_userLoggedIn.IsAdmin == false)
            {
                btnManageUsers.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

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

        private void CloseAllWindows()
        {
            RegisterLoginAction(false, _userLoggedIn.UserId);

            CloseWindow(_agendaWindow);
            CloseWindow(_manageUsersWindow);
            CloseWindow(_manageRemindersWindow);
            CloseWindow(_finishedEventsReminderModal);

            //Flags for threads
            _stopCheckEventStatusThread = true;
            _stopCheckRemindersThread = true;
        }

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

        private void CloseWindow(Window windowToClose)
        {
            if (windowToClose != null)
            {
                windowToClose.Close();
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
        }

        private void btnLogOut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        void OpenFinishedEventsReminderModalFromAnotherThread()
        {
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                Dispatcher.Invoke(new OpenFinishedEventsReminderModalDelegate(OpenFinishedEventsReminderModalFromAnotherThread));
                return;
            }

            if (_finishedEventsReminderModal == null)
            {
                OpenFinishedEventsReminderModal();
            }
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
                for (int i = 0; i < spRemindersCount - todayRemindersCount; i++)
                {
                    spReminders.Children.RemoveRange(0, spRemindersCount - todayRemindersCount);
                }
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

            lblPendingReminders.ToolTip = lblPendingReminders.Content = "Recordatorios pendientes (" + pendingReminders + ")";
            lblSeenReminders.ToolTip = lblSeenReminders.Content = "Recordatorios mostrados (" + seenReminders + ")";
        }

        private void DisplayReminders(List<Model.Reminder> todayReminders)
        {
            List<Model.Reminder> remindersToDisplay = todayReminders
                                                        .Where(r => r.AppearDate <= DateTime.Now && r.Seen == false)
                                                        .OrderBy(r => r.AppearDate)
                                                        .ToList();

            foreach (Model.Reminder reminder in remindersToDisplay)
            {
                new ShowPendingReminderModal(reminder, _userLoggedIn).ShowDialog();
            }
        }

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
                _finishedEventsReminderModal.ShowDialog();
                _finishedEventsReminderModal = null;

                List<DateTime> datesUpdated = finishedEvents
                                                .Where(fe => fe.IsCompleted)
                                                .Select(fe => (new DateTime(fe.StartEvent.Year, fe.StartEvent.Month, fe.StartEvent.Day)))
                                                .Distinct()
                                                .ToList();

                if (datesUpdated.Count > 0 && _agendaWindow != null)
                {
                    _agendaWindow.RepaintSchedulerFromAnotherThread(datesUpdated);
                }
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
    }
}