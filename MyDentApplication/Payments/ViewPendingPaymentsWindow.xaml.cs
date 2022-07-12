using Controllers;
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
using System.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for ViewPendingPaymentsWindow.xaml
	/// </summary>
	public partial class ViewPendingPaymentsWindow : Window
	{
        #region Instance variables
        private Controllers.CustomViewModel<PendigPayment> _pendingPaymentsViewModel;
        #endregion

        #region Constructors
        public ViewPendingPaymentsWindow()
		{
			this.InitializeComponent();

            UpdateGrid();
		}
        #endregion

        #region Window event handlers
        private void btnExportToPdf_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            try
            {
                // Configure save file dialog box
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Saldos pendientes " + DateTime.Now.ToString("dd-MM-yyyy"); // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "Text documents (.pdf)|*.pdf"; // Filter files by extension

                if (dlg.ShowDialog() == true)
                {
                    ExportToPdf(dlg.FileName);
                    MessageBox.Show("PDF generado", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar el PDF.\n\n Detalle del error:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
		}
        #endregion

        #region Window's logic
        private void UpdateGrid()
        {
            List<PendigPayment> pendingPayments = new List<PendigPayment>();
            var pendingStatements = BusinessController.Instance.FindBy<Model.Statement>(s => !s.IsPaid).ToList();
            decimal totalAmountOfTreatments, totalAmountOfPayments, grandTotal;
            decimal statementsTotal = 0m;

            foreach (var statement in pendingStatements)
            {
                totalAmountOfTreatments = GetTotalAmountOfTreatments(statement.TreatmentPayments.ToList());
                totalAmountOfPayments = GetTotalAmountOfPayments(statement.Payments.ToList());
                grandTotal = totalAmountOfTreatments - totalAmountOfPayments;
                grandTotal = grandTotal < 0m ? 0m : grandTotal;
                statementsTotal += grandTotal;

                pendingPayments.Add
                (
                    new PendigPayment()
                    {
                        Patient = string.Format("(Exp. No. {0}) {1} {2}", statement.Patient.AssignedId, statement.Patient.FirstName, statement.Patient.LastName),
                        StatementId = statement.StatementId,
                        Total = grandTotal
                    }
                );
            }

            _pendingPaymentsViewModel = new Controllers.CustomViewModel<PendigPayment>(pendingPayments);
            dgPendingPayments.DataContext = _pendingPaymentsViewModel;

            lblTotal.ToolTip = lblTotal.Content = "Total: $" + statementsTotal.ToString("0.00");
        }

        private decimal GetTotalAmountOfPayments(List<Model.Payment> payments)
        {
            decimal total = 0m;

            foreach (var item in payments)
	        {
		        total += item.Amount;
	        }

            return total;
        }

        private decimal GetTotalAmountOfTreatments(List<Model.TreatmentPayment> treatmentPayments)
        {
            decimal total = 0m;

            foreach (var item in treatmentPayments)
	        {
		        total += item.Total;
	        }

            return total;
        }

        private void ExportToPdf(string path)
        {
            BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\ARIAL.TTF", BaseFont.IDENTITY_H, true);

            iTextSharp.text.pdf.PdfPTable pendingPaymentsTable = MainWindow.GetTableWithHeaders(dgPendingPayments, bf);

            FillPendingPaymentsTable(pendingPaymentsTable, bf);

            //Create the PDF Document
            using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f))
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (var writer = PdfWriter.GetInstance(pdfDoc, fs))
                    {
                        pdfDoc.Open();

                        var paragraph = new iTextSharp.text.Paragraph("Saldos pendientes:");
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(paragraph);

                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));
                        pdfDoc.Add(pendingPaymentsTable);
                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));

                        paragraph = new iTextSharp.text.Paragraph(lblTotal.Content.ToString());
                        paragraph.Alignment = Element.ALIGN_RIGHT;
                        pdfDoc.Add(paragraph);

                        pdfDoc.Close();
                    }
                }
            }
        }

        private void FillPendingPaymentsTable(PdfPTable pendingPaymentsTable, BaseFont bf)
        {
            for (int i = 0; i < dgPendingPayments.Items.Count; i++)
            {
                PendigPayment pendingPayment = dgPendingPayments.Items[i] as PendigPayment;

                MainWindow.AddCell(pendingPaymentsTable, bf, pendingPayment.Patient);
                MainWindow.AddCell(pendingPaymentsTable, bf, pendingPayment.StatementId.ToString());
                MainWindow.AddCell(pendingPaymentsTable, bf, "$" + pendingPayment.Total.ToString("0.00"));
            }
        }
        #endregion

        private class PendigPayment
        {
            public string Patient { get; set; }
            public int StatementId { get; set; }
            public decimal Total { get; set; }
        }
	}
}