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

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        private Model.User _userLoggedIn;
        private AgendaWindow _agendaWindow;
        private bool _stopCheckEventStatusThread = false;
        private Thread _checkEventStatusThread;
        delegate void ParametrizedMethodInvoker5(List<DateTime> datesUpdates);

        public MainWindow(Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _userLoggedIn = userLoggedIn;

            _checkEventStatusThread = new Thread(DoWork);
            _checkEventStatusThread.SetApartmentState(ApartmentState.STA);
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

                DateTime timeBeforeOpenWindow = DateTime.Now;
                if (finishedEvents != null && finishedEvents.Count > 0)
                {
                    new FinishedEventsReminderModal(finishedEvents, _userLoggedIn).ShowDialog();
                }
                DateTime timeAfterOpenWindow = DateTime.Now;

                List<DateTime> datesUpdated = finishedEvents
                                                .Where(fe => fe.IsCompleted)
                                                .Select(fe => (new DateTime(fe.StartEvent.Year, fe.StartEvent.Month, fe.StartEvent.Day)))
                                                .Distinct()
                                                .ToList();
                if (datesUpdated.Count > 0)
                {
                    RepaintSchedulerFromAnotherThread(datesUpdated);
                }

                int elapsedTime = (int)(timeAfterOpenWindow - timeBeforeOpenWindow).TotalMilliseconds;
                int defaultWaitTime = ((1000 * 60) * 15); //15 min

                if (elapsedTime < defaultWaitTime)
                {
                    Thread.Sleep(defaultWaitTime - elapsedTime);   
                }
            }
        }

        private void btnOpenAgenda_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_agendaWindow == null)
            {
                _agendaWindow = new AgendaWindow(_userLoggedIn);
            }

            _agendaWindow.Show();
            _agendaWindow.WindowState = System.Windows.WindowState.Maximized;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_agendaWindow != null)
            {
                _agendaWindow.Close();
            }

            _stopCheckEventStatusThread = true;
        }

        void RepaintSchedulerFromAnotherThread(List<DateTime> datesUpdates)
        {
            if (!Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                Dispatcher.Invoke(new ParametrizedMethodInvoker5(RepaintSchedulerFromAnotherThread), datesUpdates);
                return;
            }

            if (_agendaWindow != null)
            {
                _agendaWindow.RepaintSchedulerFromAnotherThread(datesUpdates);
            }
        }
	}
}