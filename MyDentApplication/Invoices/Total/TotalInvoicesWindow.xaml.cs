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
using System.Web;
using System.IO;

namespace MyDentApplication
{
	/// <summary>
	/// Interaction logic for TotalInvoicesWindow.xaml
	/// </summary>
	public partial class TotalInvoicesWindow : Window
	{
		#region Instance variables
        private Controllers.CustomViewModel<Model.ReceivedInvoice> _receivedInvoicesViewModel;
        private Controllers.CustomViewModel<Model.OutgoingInvoice> _outgoingInvoicesViewModel;
        private DateTime _currentSelectedMonth;
        #endregion

        #region Constructors
        public TotalInvoicesWindow()
		{
			this.InitializeComponent();

            dtudSelectedMonth.Value = DateTime.Now;
            UpdateGrids();
		}
        #endregion

        #region Window event handlers
        private void btnRefreshInvoices_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateGrids();
        }

        private void btnExportToPdf_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                // Configure save file dialog box
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Facturacion " + _currentSelectedMonth.ToString("MM-yyyy"); // Default file name
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
        private void UpdateGrids()
        {
            _currentSelectedMonth = dtudSelectedMonth.Value.Value;

            _receivedInvoicesViewModel = new Controllers.CustomViewModel<Model.ReceivedInvoice>(i => i.IsDeleted == false && i.InvoiceDate.Value.Month == _currentSelectedMonth.Month && i.InvoiceDate.Value.Year == _currentSelectedMonth.Year, "InvoiceDate", "asc");
            _outgoingInvoicesViewModel = new Controllers.CustomViewModel<Model.OutgoingInvoice>(i => i.IsDeleted == false && i.InvoiceDate.Value.Month == _currentSelectedMonth.Month && i.InvoiceDate.Value.Year == _currentSelectedMonth.Year, "InvoiceDate", "asc");
                        
            dgReceivedInvoices.DataContext = _receivedInvoicesViewModel;
            dgOutgoingInvoices.DataContext = _outgoingInvoicesViewModel;

            decimal receivedInvoicesTotal = _receivedInvoicesViewModel.ObservableData.Sum(i => i.TotalAmount);
            decimal outgoingInvoicesTotal = _outgoingInvoicesViewModel.ObservableData.Sum(i => i.TotalAmount);
            lblTotalMonth.ToolTip = lblTotalMonth.Content = "Diferencia: $" + (outgoingInvoicesTotal - receivedInvoicesTotal).ToString("0.00");
        }

        private void ExportToPdf(string path)
        {
            BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\ARIALUNI.TTF", BaseFont.IDENTITY_H, true);

            iTextSharp.text.pdf.PdfPTable outgoingInvoicesTable = GetTableWithHeaders(dgOutgoingInvoices, bf);
            iTextSharp.text.pdf.PdfPTable receivedInvoicesTable = GetTableWithHeaders(dgReceivedInvoices, bf);

            FillOutgoingInvoicesTable(outgoingInvoicesTable, bf);
            FillReceivedInvoicesTable(receivedInvoicesTable, bf);

            //Create the PDF Document
            using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f))
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (var writer = PdfWriter.GetInstance(pdfDoc, fs))
                    {
                        pdfDoc.Open();

                        var paragraph = new iTextSharp.text.Paragraph("Recibos de honorarios:");
                        paragraph.Alignment = Element.ALIGN_CENTER;

                        pdfDoc.Add(new iTextSharp.text.Paragraph(paragraph));
                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));
                        pdfDoc.Add(outgoingInvoicesTable);
                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));

                        paragraph = new iTextSharp.text.Paragraph("Facturas:");
                        paragraph.Alignment = Element.ALIGN_CENTER;

                        pdfDoc.Add(paragraph);
                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));
                        pdfDoc.Add(receivedInvoicesTable);
                        pdfDoc.Add(new iTextSharp.text.Paragraph(" "));

                        paragraph = new iTextSharp.text.Paragraph(lblTotalMonth.Content.ToString());
                        paragraph.Alignment = Element.ALIGN_RIGHT;

                        pdfDoc.Add(paragraph);
                        pdfDoc.Close();
                    }
                }
            }
        }

        private void FillReceivedInvoicesTable(PdfPTable receivedInvoicesTable, BaseFont bf)
        {
            for (int i = 0; i < dgReceivedInvoices.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dgReceivedInvoices.ItemContainerGenerator.ContainerFromIndex(i);
                Model.ReceivedInvoice invoice = row.Item as Model.ReceivedInvoice;

                AddCell(receivedInvoicesTable, bf, invoice.ResourceProvider.Name);
                AddCell(receivedInvoicesTable, bf, invoice.InvoiceDate.Value.ToString("dd/MM/yyyy"));
                AddCell(receivedInvoicesTable, bf, invoice.PurchaseDate.ToString("dd/MM/yyyy"));
                AddCell(receivedInvoicesTable, bf, invoice.Folio);
                AddCell(receivedInvoicesTable, bf, invoice.PaidMethod);
                AddCell(receivedInvoicesTable, bf, invoice.IsPaid ? "Si" : "No");
                AddCell(receivedInvoicesTable, bf, "$" + invoice.TotalAmount.ToString());
            }
        }

        private void FillOutgoingInvoicesTable(iTextSharp.text.pdf.PdfPTable outgoingInvoicesTable, BaseFont bf)
        {
            for (int i = 0; i < dgOutgoingInvoices.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dgOutgoingInvoices.ItemContainerGenerator.ContainerFromIndex(i);
                Model.OutgoingInvoice invoice = row.Item as Model.OutgoingInvoice;

                AddCell(outgoingInvoicesTable, bf, string.Format("(Exp. No. {0}) {1} {2}", invoice.PatientId, invoice.Patient.FirstName, invoice.Patient.LastName));
                AddCell(outgoingInvoicesTable, bf, invoice.InvoiceDate.Value.ToString("dd/MM/yyyy"));
                AddCell(outgoingInvoicesTable, bf, invoice.PaidDate.ToString("dd/MM/yyyy"));
                AddCell(outgoingInvoicesTable, bf, invoice.Folio);
                AddCell(outgoingInvoicesTable, bf, invoice.PaidMethod);
                AddCell(outgoingInvoicesTable, bf, "$" + invoice.TotalAmount.ToString());
            }
        }

        private iTextSharp.text.pdf.PdfPTable GetTableWithHeaders(DataGrid dgInvoices, BaseFont bf)
        {
            iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(dgInvoices.Columns.Count);
            int[] widths = new int[dgInvoices.Columns.Count];
            for (int i = 0; i < dgInvoices.Columns.Count; i++)
            {
                widths[i] = (int)(dgInvoices.Columns[i].Width.Value * 100);
                string cellText = HttpUtility.HtmlDecode(dgInvoices.Columns[i].Header.ToString());

                //Set Font and Font Color
                iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.BOLD);
                font.Color = new BaseColor(0, 0, 0);
                iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));

                table.AddCell(cell);
            }
            table.SetWidths(widths);

            return table;
        }

        private void AddCell(PdfPTable table, BaseFont bf, string text)
        {
            string cellText = HttpUtility.HtmlDecode(text);

            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            font.Color = new BaseColor(0, 0, 0);
            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));

            table.AddCell(cell);
        }
        #endregion
	}
}