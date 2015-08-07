using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyDentApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            scheduler.SelectedDate = DateTime.Now;
            calendar.SelectedDate = DateTime.Now;
        }

        private void scheduler_OnEventMouseLeftButtonDown(object sender, WpfScheduler.Event e)
        {
            
        }

        private void scheduler_OnScheduleAddEvent(object sender, System.DateTime e)
        {
            /*WpfScheduler.Event myEvent = new WpfScheduler.Event();
            myEvent.Subject = "Test subject";
            myEvent.Start = e;
            myEvent.End = e.AddMinutes(30.0);

            scheduler.AddEvent(myEvent);*/

            /*Model.Events ev = new Model.Events()
            {
                StartEvent = e,
                EndEvent = e.AddMinutes(30.0),
                IsCanceled = false,
                IsCompleted = false,
                PatientCame = false,
                PatientId = 1,
                TreatmentId = 1
            };*/
        }

        private void scheduler_OnScheduleCancelEvent(object sender, WpfScheduler.Event e)
        {
            scheduler.DeleteEvent(e.Id);
        }

        private void calendar_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            scheduler.SelectedDate = calendar.SelectedDate.Value;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }
    }
}
