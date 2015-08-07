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
        private Scheduler _scheduler;

        internal event EventHandler<Event> OnEventMouseLeftButtonDown;
        internal event EventHandler<DateTime> OnScheduleAddEvent;
        internal event EventHandler<Event> OnScheduleCancelEvent;
        private const int finishFirstExtraHour = 9;
        private const int startSecondExtraHour = 21;
        private const int minHour = 8;
        private const int maxHour = 22;
        private const int hourIntervals = 4;
        private const double rowHeight = 50.0;
        private Guid eventSelected = Guid.Empty;
        private const double selectedEventBorderThickness = 3.0;

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

        public DayScheduler()
        {
            InitializeComponent();

            AddRowDefinitions();
            AddExtraTime();
            AddTimeLabels();
            AddButtonsForEvents();

            column.Background = Brushes.Transparent;
        }

        private void AddExtraTime()
        {
            EventsGrid.Children.Add(GetExtraHourPanel(1, 0, (finishFirstExtraHour - minHour) * hourIntervals));
            EventsGrid.Children.Add(GetExtraHourPanel(1, (startSecondExtraHour - minHour) * hourIntervals, (maxHour - startSecondExtraHour) * hourIntervals));
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
            int rowsCount = (maxHour - minHour) * hourIntervals;
            for (int i = 0; i < rowsCount; i++)
            {
                EventsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(rowHeight) });
            }
        }

        private void AddTimeLabels()
        {
            for (int i = minHour, k = 0; i < maxHour; i++)
            {
                for (int j = 0; j < hourIntervals; j++, k++)
                {
                    Label lblTime = new Label()
                    {
                        Margin = new Thickness(0.5, 0, 0, 0),
                        FontSize = 10.667,
                        Content = string.Format("{0}:{1}", i.ToString("00"), (j * (60 / hourIntervals)).ToString("00")),
                        FontWeight = (k % hourIntervals == 0) ? FontWeights.Bold : FontWeight
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
            for (int i = minHour, k = 0; i < maxHour; i++)
            {
                for (int j = 0; j < hourIntervals; j++, k++)
                {
                    string time = string.Format("{0}:{1}", i.ToString("00"), (j * 15).ToString("00"));

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
                    img.MouseDown += Image_MouseDown;

                    EventsGrid.Children.Add(img);
                }
            }
        }

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

        private IEnumerable<Event> TodayEvents
        {
            get
            {
                return _scheduler.Events.Where(ev => ev.EventInfo.StartEvent.Date <= CurrentDay.Date && ev.EventInfo.EndEvent.Date >= CurrentDay.Date);
            }
        }

        private void PaintAllEvents()
        {
            if (_scheduler == null || _scheduler.Events == null)
            {
                return;
            }

            IEnumerable<Event> eventList = TodayEvents.Where(ev => ev.EventInfo.StartEvent.Date == ev.EventInfo.EndEvent.Date).OrderBy(ev => ev.EventInfo.StartEvent);

            column.Children.Clear();

            double columnWidth = EventsGrid.ColumnDefinitions[1].Width.Value;

            foreach (Event e in eventList)
            {
                double oneHourHeight = 200.0;

                var concurrentEvents = TodayEvents.Where(e1 => ((e1.EventInfo.StartEvent <= e.EventInfo.StartEvent && e1.EventInfo.EndEvent > e.EventInfo.StartEvent) ||
                                                                (e1.EventInfo.StartEvent > e.EventInfo.StartEvent && e1.EventInfo.StartEvent < e.EventInfo.EndEvent)) &&
                                                                e1.EventInfo.EndEvent.Date == e1.EventInfo.StartEvent.Date).OrderBy(ev => ev.EventInfo.StartEvent);

                double marginTop = oneHourHeight * ((e.EventInfo.StartEvent.Hour + (e.EventInfo.StartEvent.Minute / 60.0)) - minHour);
                double width = columnWidth / (concurrentEvents.Count());
                double marginLeft = width * concurrentEvents.ToList().FindIndex(ev => ev.Id == e.Id);

                EventUserControl wEvent = new EventUserControl(e, true);
                wEvent.Width = width;
                wEvent.Height = e.EventInfo.EndEvent.Subtract(e.EventInfo.StartEvent).TotalHours * oneHourHeight;
                wEvent.Margin = new Thickness(marginLeft, marginTop, 0, 0);
                wEvent.BorderElement.BorderThickness = new Thickness(eventSelected == e.Id ? selectedEventBorderThickness : 1.0);

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
                    cm.IsOpen = true;
                });

                column.Children.Add(wEvent);
            }
        }

        private void SelectEvent(EventUserControl sender)
        {
            eventSelected = sender.Event.Id;

            foreach (EventUserControl ev in column.Children)
            {
                ev.BorderElement.BorderThickness = new Thickness(1.0);
            }

            sender.BorderElement.BorderThickness = new Thickness(selectedEventBorderThickness);
        }

        private void ResizeGrids(Size newSize)
        {
            EventsGrid.Width = newSize.Width;
            EventsHeaderGrid.Width = newSize.Width;

            double columnWidth = (this.ActualWidth - EventsGrid.ColumnDefinitions[0].ActualWidth - EventsGrid.ColumnDefinitions[2].ActualWidth);
            
            EventsGrid.ColumnDefinitions[1].Width = new GridLength(columnWidth);
            EventsHeaderGrid.ColumnDefinitions[1].Width = new GridLength(columnWidth);
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
            MenuItem mi = sender as MenuItem;
            switch (mi.Tag.ToString())
            {
                case "CancelEvent":
                    OnScheduleCancelEvent(sender, (mi.Parent as ContextMenu).Tag as Event);
                    break;
                default:
                    break;
            }
        }

        public void RepaintEvents()
        {
            PaintAllEvents();
        }
    }
}
