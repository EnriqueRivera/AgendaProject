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
        #region Instance variables
        public List<Model.Event> FinishedEvents { get; set; }
        private Model.User _userLoggedIn;
        #endregion

        #region Constructors
        public FinishedEventsReminderModal(List<Model.Event> finishedEvents, Model.User userLoggedIn)
		{
			this.InitializeComponent();

            FinishedEvents = finishedEvents;
            _userLoggedIn = userLoggedIn;

            LoadStackPanel();
		}
        #endregion

        #region Window's logic
        private void LoadStackPanel()
        {
            for (int i = 0; i < FinishedEvents.Count; i++)
            {
                FinishedEventsControl controlToAdd = new FinishedEventsControl(FinishedEvents[i], _userLoggedIn);

                controlToAdd.Margin = new Thickness(0.0, 0.0, 0.0, 1.0);

                spFinishedEvents.Children.Add(controlToAdd);
            }
        }
        #endregion
    }
}