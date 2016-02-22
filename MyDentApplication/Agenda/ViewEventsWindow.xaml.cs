using Controllers;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
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
	/// Interaction logic for ViewEventsWindow.xaml
	/// </summary>
	public partial class ViewEventsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<Model.Event> _eventsViewModel;
        #endregion

        #region Constructors
        public ViewEventsWindow()
		{
			this.InitializeComponent();

            dpStartDate.SelectedDate = DateTime.Now;
            dpEndDate.SelectedDate = DateTime.Now;

            FillPatients();
		}
        #endregion

        #region Window event handlers
        private void btnFilter_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            UpdateGrid();
        }
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            DateTime startDate = dpStartDate.SelectedDate.Value;
            DateTime endDate = dpEndDate.SelectedDate.Value;
            Model.Patient selectedPatient = (cbPatients.SelectedValue as Controllers.ComboBoxItem).Value as Model.Patient;

            if (selectedPatient == null)
                _eventsViewModel = new Controllers.CustomViewModel<Model.Event>(e => EntityFunctions.TruncateTime(e.StartEvent) >= EntityFunctions.TruncateTime(startDate) && EntityFunctions.TruncateTime(e.StartEvent) <= EntityFunctions.TruncateTime(endDate), "StartEvent", "asc");
            else
                _eventsViewModel = new Controllers.CustomViewModel<Model.Event>(e => EntityFunctions.TruncateTime(e.StartEvent) >= EntityFunctions.TruncateTime(startDate) && EntityFunctions.TruncateTime(e.StartEvent) <= EntityFunctions.TruncateTime(endDate) && e.PatientId == selectedPatient.PatientId, "StartEvent", "asc");


            this.DataContext = _eventsViewModel;
        }

        private void FillPatients()
        {
            List<Model.Patient> patients = BusinessController.Instance.FindBy<Model.Patient>(p => p.IsDeleted == false)
                                                .OrderBy(p => p.FirstName)
                                                .ThenBy(p => p.LastName)
                                                .ThenBy(p => p.AssignedId)
                                                .ToList();

            cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = "", Value = null });

            foreach (Model.Patient patient in patients)
            {
                cbPatients.Items.Add(new Controllers.ComboBoxItem() { Text = string.Format("{1} {2} (Exp. No. {0})", patient.AssignedId, patient.FirstName, patient.LastName), Value = patient });
            }

            cbPatients.SelectedIndex = 0;
        }
        #endregion
    }

    public class EventSatusAndFullDateValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 5)
            {
                Model.Event modelEvent = new Model.Event()
                {
                    IsException = (bool)values[0],
                    IsCanceled = (bool)values[1],
                    IsCompleted = (bool)values[2],
                    PatientSkips = (bool)values[3],
                    IsConfirmed = (bool)values[4]
                };

                WpfScheduler.Event scheduleEvent = new WpfScheduler.Event() { EventInfo = modelEvent };

                return scheduleEvent.EventStatusString;
            }
            else if (values.Length == 1 && values[0] is DateTime)
            {
                DateTime dateToShow = (DateTime)values[0];
                return dateToShow.ToString("dd/MMMM/yyyy") + dateToShow.ToString(", HH:mm") + " hrs";
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}