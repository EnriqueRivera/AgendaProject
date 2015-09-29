using System;
using System.Collections.Generic;
using System.Globalization;
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
	/// Interaction logic for EmailContactControl.xaml
	/// </summary>
	public partial class EmailContactControl : UserControl
    {
        #region Instance variables
        public Controllers.EmailElement EmailElement { get; set; }
        internal event EventHandler<bool> OnRemove;
        #endregion

        #region Constructors
        public EmailContactControl(Controllers.EmailElement emailElement)
		{
			this.InitializeComponent();
            EmailElement = emailElement;

            UpdateControlInfo();
		}
        #endregion

        #region Window event handlers
        private void btnRemoveEmail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OnRemove(this, EmailElement is Controllers.EmailContact);
        }
        #endregion

        #region Window's logic
        private void UpdateControlInfo()
        {
            if (EmailElement is Controllers.EmailContact)
            {
                Controllers.EmailContact emailContact = EmailElement as Controllers.EmailContact;
                rcBackground.Fill = emailContact.IsPatient ? rcIsPatient.Fill : rcIsNotPatient.Fill;
                lblEmailName.Content = string.IsNullOrEmpty(emailContact.FullName) ? emailContact.Email : emailContact.FullName;
                lblEmailName.ToolTip = string.Format("{0} <{1}>", emailContact.FullName, emailContact.Email);
            }
            else
            {
                Controllers.EmailAttachment emailAttachment = EmailElement as Controllers.EmailAttachment;
                rcBackground.Fill = rcAttachment.Fill;
                lblEmailName.Content = emailAttachment.FileName;
                lblEmailName.ToolTip = emailAttachment.Path;
            }

            var lblSize = MeasureString(lblEmailName.Content.ToString());
            UserControl.Width = lblSize.Width + 50.0;
        }

        private Size MeasureString(string candidate)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(this.lblEmailName.FontFamily, this.lblEmailName.FontStyle, this.lblEmailName.FontWeight, this.lblEmailName.FontStretch),
                this.lblEmailName.FontSize,
                Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
        }
        #endregion
    }
}