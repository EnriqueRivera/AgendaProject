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

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private Model.User _userLoggedIn;
        private AgendaWindow _agendaWindow;
        private ManageUsersWindow _manageUsersWindow;
        private FinishedEventsReminderModal _finishedEventsReminderModal;
        private bool _stopCheckEventStatusThread = false;
        private int _timeToWait = (1000 * 60) * 15;
        private Thread _checkEventStatusThread;
        delegate void RepaintSchedulerFromAnotherThreadDelegate(List<DateTime> datesUpdates);

        public MainWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;

            _checkEventStatusThread = new Thread(DoWork);
            _checkEventStatusThread.SetApartmentState(ApartmentState.STA);
            _checkEventStatusThread.IsBackground = true;
            _checkEventStatusThread.Start();
		}

        public void DoWork()
        {
            while (_stopCheckEventStatusThread == false)
            {
                List<Model.Event> finishedEvents = Controllers.BusinessController.Instance.FindBy<Model.Event>
                                                    (e => e.EndEvent <= DateTime.Now && e.IsCanceled == false && e.IsCompleted == false)
                                                    .OrderBy(e => e.EndEvent)
                                                    .ToList();

                if (finishedEvents != null && finishedEvents.Count > 0)
                {
                    _finishedEventsReminderModal = new FinishedEventsReminderModal(finishedEvents, _userLoggedIn);
                    _finishedEventsReminderModal.ShowDialog();
                    _finishedEventsReminderModal = null;
                }

                List<DateTime> datesUpdated = finishedEvents
                                                .Where(fe => fe.IsCompleted)
                                                .Select(fe => (new DateTime(fe.StartEvent.Year, fe.StartEvent.Month, fe.StartEvent.Day)))
                                                .Distinct()
                                                .ToList();
                if (datesUpdated.Count > 0)
                {
                    RepaintSchedulerFromAnotherThread(datesUpdated);
                }

                Thread.Sleep(_timeToWait);
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
            CloseWindowInAnotherThread(_finishedEventsReminderModal);

            _stopCheckEventStatusThread = true;
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


        void RepaintSchedulerFromAnotherThread(List<DateTime> datesUpdates)
        {
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                Dispatcher.Invoke(new RepaintSchedulerFromAnotherThreadDelegate(RepaintSchedulerFromAnotherThread), datesUpdates);
                return;
            }
            
            if (_agendaWindow != null)
            {
                _agendaWindow.RepaintSchedulerFromAnotherThread(datesUpdates);
            }
        }

        void CloseWindowInAnotherThread(Window windowToClose)
        {
            if (windowToClose != null)
            {
                if (windowToClose.Dispatcher.CheckAccess())
                {
                    windowToClose.Close();
                }
                else
                {
                    windowToClose.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(windowToClose.Close));
                }
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
                _manageUsersWindow = new ManageUsersWindow(_userLoggedIn);
                _manageUsersWindow.Closed += ManageUsersWindow_Closed;
            }

            _manageUsersWindow.Show();
            _manageUsersWindow.WindowState = WindowState.Normal;
        }

        private void btnOpenAgenda_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_agendaWindow == null)
            {
                _agendaWindow = new AgendaWindow(_userLoggedIn);
                _agendaWindow.Closed += AgendaWindow_Closed;
            }

            _agendaWindow.Show();
            _agendaWindow.WindowState = WindowState.Normal;
        }

        void AgendaWindow_Closed(object sender, EventArgs e)
        {
            _agendaWindow = null;
        }

        void ManageUsersWindow_Closed(object sender, EventArgs e)
        {
            _manageUsersWindow = null;
        }
	}
}