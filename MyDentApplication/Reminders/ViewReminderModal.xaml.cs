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
	/// Interaction logic for ViewReminderModal.xaml
	/// </summary>
	public partial class ViewReminderModal : Window
    {
        #region Instance variables
        private Model.Reminder _reminderToView;
        #endregion

        #region Constructors
        public ViewReminderModal(Model.Reminder reminderToView)
		{
			this.InitializeComponent();

            _reminderToView = reminderToView;
            LoadReminderInfo();
		}
        #endregion

        #region Window's logic
        private void LoadReminderInfo()
        {
            txtReminderMessage.Text = _reminderToView.Message;
        }
        #endregion
    }
}