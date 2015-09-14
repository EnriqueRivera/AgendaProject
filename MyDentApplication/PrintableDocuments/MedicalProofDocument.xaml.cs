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
	/// Interaction logic for MedicalProofDocument.xaml
	/// </summary>
    public partial class MedicalProofDocument : Window
    {
        public MedicalProofDocument()
        {
            this.InitializeComponent();

            DateTime today = DateTime.Now;
            txtDay.Text = today.Day.ToString();
            txtMonth.Text = today.ToString("MMMM");
            txtYear.Text = today.ToString("yyyy").Substring(2);
        }

        private void btnPrint_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                svPrintableDocument.ScrollToTop();
                svPrintableDocument.ScrollToLeftEnd();
                dlg.PrintVisual(canvasPrintableDocument, Title);
            }
        }
    }
}