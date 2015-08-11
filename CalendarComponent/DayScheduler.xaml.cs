using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for DayScheduler.xaml
    /// </summary>
    public partial class DayScheduler : UserControl
    {
        #region Instance variables
        internal event EventHandler<Event> OnEventMouseLeftButtonDown;
        internal event EventHandler<DateTime> OnScheduleAddEvent;
        internal event EventHandler<Event> OnScheduleContextMenuEvent;
        private Scheduler _scheduler;
        private const int _finishFirstExtraHour = 9;
        private const int _startSecondExtraHour = 20;
        private const int _minHour = 7;
        private const int _maxHour = 22;
        private const int _hourIntervals = 4;
        private const double _rowHeight = 50.0;
        private Guid _eventSelected = Guid.Empty;
        private const double _selectedEventBorderThickness = 3.0;
        private const double _oneHourHeight = _rowHeight * _hourIntervals;
        public Model.User UserLoggedIn { get; set; }

        public bool CanceledEventsVisible { get; set; }
        public bool ExceptionEventsVisible { get; set; }
        public bool CompletedEventsVisible { get; set; }
        public bool PatientSkipsEventsVisible { get; set; }
        public bool PendingEventsVisible { get; set; }
        #endregion

        #region Getters
        public int FinishFirstExtraHour { get { return _finishFirstExtraHour; } }

        public int StartSecondExtraHour { get { return _startSecondExtraHour; } }

        public int MinHour { get { return _minHour; } }

        public int MaxHour { get { return _maxHour; } }

        public int HourIntervals { get { return _hourIntervals; } }

        private IEnumerable<Event> TodayEvents
        {
            get
            {
                var result = _scheduler.Events.Where(ev => ev.EventInfo.StartEvent.Date <= CurrentDay.Date && ev.EventInfo.EndEvent.Date >= CurrentDay.Date);

                if (CanceledEventsVisible == false)
                {
                    result = result.Where(e => e.EventStatus != Controllers.EventStatus.CANCELED);
                }

                if (ExceptionEventsVisible == false)
                {
                    result = result.Where(e => e.EventStatus != Controllers.EventStatus.EXCEPTION);
                }

                if (CompletedEventsVisible == false)
                {
                    result = result.Where(e => e.EventStatus != Controllers.EventStatus.COMPLETED);
                }

                if (PatientSkipsEventsVisible == false)
                {
                    result = result.Where(e => e.EventStatus != Controllers.EventStatus.PATIENT_SKIPS);
                }

                if (PendingEventsVisible == false)
                {
                    result = result.Where(e => e.EventStatus != Controllers.EventStatus.PENDING);
                }

                return result;
            }
        }
        #endregion

        #region CurrentDay

        private DateTime _currentDay;
        internal DateTime CurrentDay
        {
            get { return _currentDay; }
            set {
                _currentDay = value;
                AdjustCurrentDay(value);
            }
        }

        private void AdjustCurrentDay(DateTime currentDay)
        {
            dayLabel.Content = currentDay.ToString("D");

            PaintAllEvents();
        }
        #endregion

        #region Constructor Methods
        public DayScheduler()
        {
            InitializeComponent();

            AddRowDefinitions();
            AddExtraTime();
            AddTimeLabels();
            AddButtonsForEvents();
            EnableFilters();

            column.Background = Brushes.Transparent;
        }

        private void EnableFilters()
        {
            CanceledEventsVisible = true;
            ExceptionEventsVisible = true;
            CompletedEventsVisible = true;
            PatientSkipsEventsVisible = true;
            PendingEventsVisible = true;
        }

        private void AddExtraTime()
        {
            EventsGrid.Children.Add(GetExtraHourPanel(1, 0, (_finishFirstExtraHour - _minHour) * _hourIntervals));
            EventsGrid.Children.Add(GetExtraHourPanel(1, (_startSecondExtraHour - _minHour) * _hourIntervals, (_maxHour - _startSecondExtraHour) * _hourIntervals));
        }

        private StackPanel GetExtraHourPanel(int column, int row, int rowSpan)
        {
            StackPanel sp = new StackPanel()
            {
                Opacity = 0.3,
                Background = Brushes.Gray,
                Orientation = Orientation.Horizontal,
                VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
            };
            
            Grid.SetZIndex(sp, -1);
            Grid.SetColumn(sp, column);
            Grid.SetRow(sp, row);
            Grid.SetRowSpan(sp, rowSpan);

            return sp;
        }

        private void AddRowDefinitions()
        {
            int rowsCount = (_maxHour - _minHour) * _hourIntervals;
            for (int i = 0; i < rowsCount; i++)
            {
                EventsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(_rowHeight) });
            }
        }

        private void AddTimeLabels()
        {
            for (int i = _minHour, k = 0; i < _maxHour; i++)
            {
                for (int j = 0; j < _hourIntervals; j++, k++)
                {
                    Label lblTime = new Label()
                    {
                        Margin = new Thickness(0.5, 0, 0, 0),
                        FontSize = 10.667,
                        Content = string.Format("{0}:{1}", i.ToString("00"), (j * (60 / _hourIntervals)).ToString("00")),
                        FontWeight = (k % _hourIntervals == 0) ? FontWeights.Bold : FontWeight
                    };

                    Grid.SetColumn(lblTime, 0);
                    Grid.SetRow(lblTime, k);
                    Grid.SetRowSpan(lblTime, 1);

                    EventsGrid.Children.Add(lblTime);
                }
            }
        }

        private void AddButtonsForEvents()
        {
            for (int i = _minHour, k = 0; i < _maxHour; i++)
            {
                for (int j = 0; j < _hourIntervals; j++, k++)
                {
                    string time = string.Format("{0}:{1}", i.ToString("00"), (j * (60 / _hourIntervals)).ToString("00"));

                    Image img = new Image()
                    {
                        Margin = new Thickness(0, 0, 19, 0),
                        Height = 30,
                        Width = 30,
                        Tag = time,
                        ToolTip = "Agendar cita a las " + time + " hrs...",
                        Cursor = Cursors.Hand,
                        Source = new BitmapImage(new Uri("pack://application:,,,/WpfScheduler;component/Images/AddEventIcon.png"))
                    };

                    Grid.SetColumn(img, 2);
                    Grid.SetRow(img, k);
                    img.MouseLeftButtonDown += Image_MouseLeftButtonDown;

                    EventsGrid.Children.Add(img);
                }
            }
        }
        #endregion

        #region Control Events
        private void UserControl_Loaded(object sender, RoutedEventArgs ea)
        {
            DependencyObject ucParent = (sender as DayScheduler).Parent;
            while (!(ucParent is Scheduler))
            {
                ucParent = LogicalTreeHelper.GetParent(ucParent);
            }

            _scheduler = ucParent as Scheduler;

            _scheduler.OnEventAdded += ((object s, Event e) =>
            {
                if (e.EventInfo.StartEvent.Date == e.EventInfo.EndEvent.Date)
                {
                    PaintAllEvents();
                }
            });

            _scheduler.OnEventDeleted += ((object s, Event e) =>
            {
                if (e.EventInfo.StartEvent.Date == e.EventInfo.EndEvent.Date)
                {
                    PaintAllEvents();
                }
            });

            _scheduler.OnEventsModified += ((object s, EventArgs e) =>
            {
                PaintAllEvents();
            });

            (sender as DayScheduler).SizeChanged += DayScheduler_SizeChanged;

            ResizeGrids(new Size(this.ActualWidth, this.ActualHeight));
            PaintAllEvents();
        }

        private void DayScheduler_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeGrids(e.NewSize);
            PaintAllEvents();
        }

        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string[] time = (sender as Image).Tag.ToString().Split(':');
            int hour = 0;
            int minutes = 0;

            if (time.Count() > 1)
            {
                int.TryParse(time[0], out hour);
                int.TryParse(time[1], out minutes);
            }

            OnScheduleAddEvent(sender, new DateTime(CurrentDay.Year, CurrentDay.Month, CurrentDay.Day, hour, minutes, 0));
        }

        private void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OnScheduleContextMenuEvent(sender, ((sender as MenuItem).Parent as ContextMenu).Tag as Event);
        }
        #endregion

        #region Paint Events
        private void PaintAllEvents()
        {
            if (_scheduler == null || _scheduler.Events == null)
            {
                return;
            }

            IEnumerable<Event> eventList = TodayEvents
                                            .Where(ev => ev.EventInfo.StartEvent.Date == ev.EventInfo.EndEvent.Date)
                                            .OrderBy(ev => ev.EventInfo.IsCanceled)
                                            .ThenBy(ev => ev.EventInfo.StartEvent);

            column.Children.Clear();

            double columnWidth = EventsGrid.ColumnDefinitions[1].Width.Value;

            foreach (Event e in eventList)
            {
                List<Event> concurrentEvents = new List<Event>();
                GetConcurrentEvents(e, concurrentEvents);
                concurrentEvents = concurrentEvents
                                    .OrderBy(ev => ev.EventInfo.StartEvent)
                                    .ThenBy(ev => ev.EventInfo.EndEvent)
                                    .ToList();

                double marginTop = _oneHourHeight * ((e.EventInfo.StartEvent.Hour + (e.EventInfo.StartEvent.Minute / 60.0)) - _minHour);
                double width = columnWidth / (concurrentEvents.Count());
                double marginLeft = width * concurrentEvents.ToList().FindIndex(ev => ev.Id == e.Id);

                EventUserControl wEvent = new EventUserControl(e);
                wEvent.Width = width;
                wEvent.Height = e.EventInfo.EndEvent.Subtract(e.EventInfo.StartEvent).TotalHours * _oneHourHeight;
                wEvent.Margin = new Thickness(marginLeft, marginTop, 0, 0);
                wEvent.BorderElement.BorderThickness = new Thickness(_eventSelected == e.Id ? _selectedEventBorderThickness : 1.0);

                wEvent.MouseLeftButtonDown += ((object sender, MouseButtonEventArgs ea) =>
                {
                    ea.Handled = true;

                    SelectEvent(sender as EventUserControl);

                    OnEventMouseLeftButtonDown(sender, wEvent.Event);
                });
                wEvent.MouseRightButtonDown += ((object sender, MouseButtonEventArgs ea) =>
                {
                    SelectEvent(sender as EventUserControl);

                    ContextMenu cm = this.FindResource("SchedulerContextMenu") as ContextMenu;
                    cm.Tag = e;
                    cm.IsOpen = (e.EventInfo.IsCanceled == false && e.EventInfo.IsCompleted == false) || UserLoggedIn.IsAdmin;
                });

                column.Children.Add(wEvent);
            }
        }

        private void GetConcurrentEvents(Event e, List<Event> concurrentEvents)
        {
            foreach (Event ev in TodayEvents)
            {
                if (Controllers.Utils.IsOverlappedTime(e.EventInfo.StartEvent, e.EventInfo.EndEvent, ev.EventInfo.StartEvent, ev.EventInfo.EndEvent) 
                    && !concurrentEvents.Contains(ev))
                {
                    concurrentEvents.Add(ev);
                    GetConcurrentEvents(ev, concurrentEvents);
                }
            }
        }

        private void SelectEvent(EventUserControl sender)
        {
            _eventSelected = sender.Event.Id;

            foreach (EventUserControl ev in column.Children)
            {
                ev.BorderElement.BorderThickness = new Thickness(1.0);
            }

            sender.BorderElement.BorderThickness = new Thickness(_selectedEventBorderThickness);
        }

        private void ResizeGrids(Size newSize)
        {
            EventsGrid.Width = newSize.Width;
            EventsHeaderGrid.Width = newSize.Width;

            double columnWidth = (this.ActualWidth - EventsGrid.ColumnDefinitions[0].ActualWidth - EventsGrid.ColumnDefinitions[2].ActualWidth);
            
            EventsGrid.ColumnDefinitions[1].Width = new GridLength(columnWidth);
            EventsHeaderGrid.ColumnDefinitions[1].Width = new GridLength(columnWidth);
        }        

        public void RepaintEvents()
        {
            PaintAllEvents();
        }
        #endregion
    }
}
