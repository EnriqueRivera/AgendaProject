using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for FinishedEventsReminderModal.xaml
	/// </summary>
	public partial class FinishedEventsReminderModal : Window
	{
        List<Model.Event> _finishedEvents;
        private Model.User _userLoggedIn;

        public FinishedEventsReminderModal(List<Model.Event> finishedEvents, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            _finishedEvents = finishedEvents;
            _userLoggedIn = userLoggedIn;

            LoadStackPanel();
		}

        private void LoadStackPanel()
        {
            for (int i = 0; i < _finishedEvents.Count; i++)
            {
                FinishedEventsControl controlToAdd = new FinishedEventsControl(_finishedEvents[i], _userLoggedIn)
                {
                    BorderBrush = Brushes.Black
                };

                controlToAdd.BorderThickness = (_finishedEvents.Count == 1 || i == _finishedEvents.Count - 1) ? 
                                                new Thickness(2.0) : 
                                                new Thickness(2.0, 2.0, 2.0, 0.0);

                spFinishedEvents.Children.Add(controlToAdd);
            }
        }
	}
}