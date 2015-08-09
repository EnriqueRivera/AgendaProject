using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfScheduler
{
    /*public enum Mode
    {
        Day,
        Week,
        Month
    }*/

    /// <summary>
    /// Interaction logic for WpfSchedule.xaml
    /// </summary>
    public partial class Scheduler : UserControl
    {
        public event EventHandler<Event> OnEventMouseLeftButtonDown;
        public event EventHandler<DateTime> OnScheduleAddEvent;
        public event EventHandler<Event> OnScheduleCancelEvent;
        internal event EventHandler<Event> OnEventAdded;
        internal event EventHandler<Event> OnEventDeleted;
        internal event EventHandler OnEventsModified;

        #region Getters And Setters
        public int FinishFirstExtraHour { get { return DayScheduler.FinishFirstExtraHour; } }
        public int StartSecondExtraHour { get { return DayScheduler.StartSecondExtraHour; } }
        public int MinHour { get { return DayScheduler.MinHour; } }
        public int MaxHour { get { return DayScheduler.MaxHour; } }
        public int HourIntervals { get { return DayScheduler.HourIntervals; } }

        public bool CanceledEventsVisible
        {
            get { return DayScheduler.CanceledEventsVisible; }
            set { DayScheduler.CanceledEventsVisible = value; }
        }

        public bool ExceptionEventsVisible
        {
            get { return DayScheduler.ExceptionEventsVisible; }
            set { DayScheduler.ExceptionEventsVisible = value; }
        }

        public bool PatientCameEventsVisible
        {
            get { return DayScheduler.PatientCameEventsVisible; }
            set { DayScheduler.PatientCameEventsVisible = value; }
        }

        public bool PatientNotCameEventsVisible
        {
            get { return DayScheduler.PatientNotCameEventsVisible; }
            set { DayScheduler.PatientNotCameEventsVisible = value; }
        }

        public bool NotCompletedEventsVisible
        {
            get { return DayScheduler.NotCompletedEventsVisible; }
            set { DayScheduler.NotCompletedEventsVisible = value; }
        }
        #endregion

        #region SelectedDate
        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(Scheduler),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(SelectedDateChanged)));

        public DateTime SelectedDate
        {
            get { return (DateTime)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        private static void SelectedDateChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            DateTime SelectedDate = (DateTime)e.NewValue;
            Scheduler sc = source as Scheduler;
            sc.DayScheduler.CurrentDay = SelectedDate;
        }
        #endregion

        #region Events
        public static readonly DependencyProperty EventsProperty =
            DependencyProperty.Register("Events", typeof(ObservableCollection<Event>), typeof(Scheduler),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(AdjustEvents)));

        public ObservableCollection<Event> Events
        {
            get { return (ObservableCollection<Event>)GetValue(EventsProperty); }
            set { SetValue(EventsProperty, value); }
        }

        private static void AdjustEvents(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if ((source as Scheduler).OnEventsModified != null) 
                (source as Scheduler).OnEventsModified(source, null);
        }
        #endregion

        #region DayScheduler
        internal static readonly DependencyProperty DaySchedulerProperty =
            DependencyProperty.Register("DayScheduler", typeof(DayScheduler), typeof(Scheduler),
            new FrameworkPropertyMetadata());

        internal DayScheduler DayScheduler
        {
            get { return ucDayScheduler; }
        }
        #endregion

        #region Mode
        /*public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(Mode), typeof(Scheduler),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(ModeChanged)));*/

        /*public Mode Mode
        {
            get { return (Mode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }*/

        /*private static void ModeChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            Mode mode = (Mode)e.NewValue;
            Scheduler sc = source as Scheduler;
            sc.DayScheduler.Visibility = (mode == Mode.Day ? Visibility.Visible : Visibility.Hidden);

           	sc.DayScheduler.CurrentDay = SelectedDate;
        }*/
        #endregion

        public Scheduler()
        {
            InitializeComponent();
            //Mode = WpfScheduler.Mode.Week;
            Events = new ObservableCollection<Event>();
            SelectedDate = DateTime.Now;

            DayScheduler.OnEventMouseLeftButtonDown += InnerScheduler_OnEventMouseLeftButtonDown;

            DayScheduler.OnScheduleAddEvent += InnerScheduler_OnScheduleAddEvent;
            DayScheduler.OnScheduleCancelEvent += InnerScheduler_OnScheduleCancelEvent;
        }

        void InnerScheduler_OnScheduleCancelEvent(object sender, Event e)
        {
            if (OnScheduleCancelEvent != null)
            {
                OnScheduleCancelEvent(sender, e);
            }
        }

        void InnerScheduler_OnScheduleAddEvent(object sender, DateTime e)
        {
            if (OnScheduleAddEvent != null)
            {
                OnScheduleAddEvent(sender, e);
            }
        }

        void InnerScheduler_OnEventMouseLeftButtonDown(object sender, Event e)
        {
            if (OnEventMouseLeftButtonDown != null)
            {
                OnEventMouseLeftButtonDown(sender, e);
            }
        }

        public void AddEvent(Event e)
        {
            if (e.EventInfo.StartEvent > e.EventInfo.EndEvent)
            {
                throw new ArgumentException("End date is before Start date");
            }

            Events.Add(e);

            if (OnEventAdded != null)
            {
                OnEventAdded(this, e);
            }
        }

        public void DeleteEvent(Guid id)
        {
            Event e = Events.SingleOrDefault(ev => ev.Id.Equals(id));
            if (e != null)
            {
                DateTime date = e.EventInfo.StartEvent;
                Events.Remove(e);
                if (OnEventDeleted != null)
                {
                    OnEventDeleted(this, e);
                }
            }
        }

        public void NextPage()
        {
            SelectedDate = SelectedDate.AddDays(1.0);
        }

        public void PrevPage()
        {
            SelectedDate = SelectedDate.AddDays(-1.0);
        }

        public void RepaintEvents()
        {
            DayScheduler.RepaintEvents();
        }
    }
}
