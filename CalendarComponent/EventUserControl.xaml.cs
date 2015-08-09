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

namespace WpfScheduler
{
    /// <summary>
    /// Interaction logic for EventUserControl.xaml
    /// </summary>
    public partial class EventUserControl : UserControl
    {
        Event _e;

        public EventUserControl(Event e)
        {
            InitializeComponent();

            _e = e;

            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.BorderElement.Background = e.Color;

            this.txbDateAndTreatment.Text = String.Format("{2} ({0} - {1})", e.EventInfo.StartEvent.ToString("HH:mm"), e.EventInfo.EndEvent.ToString("HH:mm"), e.EventInfo.Treatment.Name);
            this.txbPatientName.Text = "Paciente: " + e.EventInfo.Patient.FirstName + " " + e.EventInfo.Patient.LastName;
            this.BorderElement.ToolTip = this.txbDateAndTreatment.Text + System.Environment.NewLine + this.txbPatientName.Text;
        }

        public Event Event 
        {
            get { return _e; }
        }
    }


    
}
