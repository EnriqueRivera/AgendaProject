using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class Utils
    {
        public const string SCHEDULER_COLOR_CONFIGURATION_PREFIX = "SCHEDULER_COLOR_";
        public const string EMAIL_CONFIGURATION_PREFIX = "EMAIL_CONFIGURATION_";
        public const string PORT = "PORT";
        public const string HOST = "HOST";
        public const string ENABLE_SSL = "ENABLE_SSL";
        public const string USERNAME = "USERNAME";
        public const string PASSWORD = "PASSWORD";
        public const string TREATMENT_DENTISTRY = "Odontología";
        public const string TREATMENT_PAIN_CLINIC = "Clínica del dolor";
        public const string TREATMENT_ENDODONTICS = "Endodoncia";
        public const string TREATMENT_ORTHODONTICS = "Ortodoncia";
        public const string TREATMENT_CMF = "CMF";
        public const string TREATMENT_PERIODONTICS = "Periodoncia";
        public const string TREATMENT_PEDIATRIC_DENTAL = "Odontopediatría";
        public const string TREATMENT_HEALTH_INSURANCE = " - Con seguro médico";
        public const string PATIENT_PICTURE = "FOTO_DEL_PACIENTE";
        //public const string PATIENT_MAX_SKIPPED_EVENTS_CONFIGURATION = "PATIENT_MAX_SKIPPED_EVENTS";
        private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static bool IsOverlappedTime(DateTime event1Start, DateTime event1End, DateTime event2Start, DateTime event2End)
        {
            return ((event1Start >= event2Start && event1Start < event2End) || (event1End > event2Start && event1End <= event2End))
                    || ((event2Start >= event1Start && event2Start < event1End) || (event2End > event1Start && event2End <= event1End));
        }

        public static bool AddEventStatusChanges(string oldEventStatus, string newEventStatus, int eventId, int userId)
        {
            Model.EventStatusChanx eventStatusChanged = new Model.EventStatusChanx()
            {
                OldStatus = oldEventStatus,
                NewStatus = newEventStatus,
                ChangeDate = DateTime.Now,
                EventId = eventId,
                StatusChangerId = userId
            };

            return BusinessController.Instance.Add<Model.EventStatusChanx>(eventStatusChanged);
        }

        public static string EventStatusString(EventStatus es)
        {
            switch (es)
            {
                case EventStatus.CANCELED: return "Cancelada";
                case EventStatus.COMPLETED: return "Completada";
                case EventStatus.EXCEPTION: return "Excepción";
                case EventStatus.PATIENT_SKIPS: return "Paciente no asisitó";
                case EventStatus.PENDING: return "Sin concretar";
                case EventStatus.CONFIRMED: return "Confirmada";
                default: return string.Empty;
            }
        }

        public static bool IsValidEmail(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }

            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue / 1024) >= 1)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n1} {1}", dValue, SizeSuffixes[i]);
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string BuildTreatmentPricesTable(List<Model.TreatmentPayment> treatmentPayments, out decimal totalAmount)
        {
            totalAmount = 0m;
            StringBuilder treatments = new StringBuilder();
            string treatmentsTable = @"<table style='width:100%'>
                                    <tr>
                                        <th>Tratamiento</th>
                                        <th>Cantidad</th> 
                                        <th>Precio unitario</th>
                                        <th>Descuento</th>
                                        <th>Total</th>
                                        <th>Fecha</th>
                                    </tr>
                                    {0}
                                </table>";
            
            foreach (var item in treatmentPayments)
            {
                treatments.Append("<tr>");
                treatments.AppendFormat("<td align='center'>{0}</td>", string.Format("{0} - {1} ({2})", item.TreatmentPrice.TreatmentKey, item.TreatmentPrice.Name, item.TreatmentPrice.Type));
                treatments.AppendFormat("<td align='center'>{0}</td>", item.Quantity);
                treatments.AppendFormat("<td align='center'>{0}</td>", "$" + item.Price.ToString("0.00"));
                treatments.AppendFormat("<td align='center'>{0}</td>", item.Discount + "%");
                treatments.AppendFormat("<td align='center'>{0}</td>", "$" + item.Total.ToString("0.00"));
                treatments.AppendFormat("<td align='center'>{0}</td>", item.TreatmentDate.ToString("dd/MMMM/yyyy"));
                treatments.Append("</tr>");

                totalAmount += item.Total;
            }

            return string.Format(treatmentsTable, treatments.ToString());
        }

        public static string BuildPaymentsTable(List<Model.Payment> paymentsList, out decimal totalAmount)
        {
            totalAmount = 0m;
            StringBuilder payments = new StringBuilder();
            string paymentsTable = @"<table style='width:100%'>
                                    <tr>
                                        <th>Tipo de pago</th>
                                        <th>Banco</th> 
                                        <th>Cantidad</th>
                                        <th>No. de voucher o cheque</th>
                                        <th>Fecha de pago</th>
                                        <th>Observaciones</th>
                                    </tr>
                                    {0}
                                </table>";

            foreach (var item in paymentsList)
            {
                payments.Append("<tr>");
                payments.AppendFormat("<td align='center'>{0}</td>", item.Type);
                payments.AppendFormat("<td align='center'>{0}</td>", item.Bank == null ? "N/A" : item.Bank.Name);
                payments.AppendFormat("<td align='center'>{0}</td>", "$" + item.Amount.ToString("0.00"));
                payments.AppendFormat("<td align='center'>{0}</td>", string.IsNullOrEmpty(item.VoucherCheckNumber) ? "N/A" : item.VoucherCheckNumber);
                payments.AppendFormat("<td align='center'>{0}</td>", item.PaymentDate.ToString("dd/MMMM/yyyy"));
                payments.AppendFormat("<td align='center'>{0}</td>", item.Observation);
                payments.Append("</tr>");

                totalAmount += item.Amount;
            }

            return string.Format(paymentsTable, payments.ToString());
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class EmailElement { }

    public class EmailContact : EmailElement
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsPatient { get; set; }
        public bool IsValid { get; set; }
    }

    public class EmailAttachment : EmailElement
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
    }

    public class CustomViewModel<T> where T : class
    {
        private ObservableCollection<T> _allData = new ObservableCollection<T>();

        public CustomViewModel(System.Linq.Expressions.Expression<Func<T, bool>> findBy, string sortBy, string sortDirection)
        {
            var param = Expression.Parameter(typeof(T), "item");
            var sortExpression = Expression.Lambda<Func<T, object>>
                                    (Expression.Convert(Expression.Property(param, sortBy), typeof(object)), param);

            var allData = Controllers.BusinessController.Instance.FindBy<T>(findBy).ToList();

            switch (sortDirection.ToLower())
            {
                case "asc":
                    allData = allData.AsQueryable<T>().OrderBy<T, object>(sortExpression).ToList();
                    break;
                default:
                    allData = allData.AsQueryable<T>().OrderByDescending<T, object>(sortExpression).ToList();
                    break;
            }

            _allData = new ObservableCollection<T>(allData.ToList());
        }

        public CustomViewModel(List<T> data)
        {
            _allData = new ObservableCollection<T>(data);
        }

        public ObservableCollection<T> ObservableData
        {
            get { return _allData; }
        }
    }

    public enum EventStatus
    {
        CANCELED = 1,
        COMPLETED = 2,
        EXCEPTION = 3,
        PATIENT_SKIPS = 4,
        PENDING = 5,
        CONFIRMED = 6
    }

    public enum CleaningType
    {
        CLEANED,
        PACKAGED,
        STERILIZED
    }

    public enum PaymentType
    {
        Efectivo,
        Cheque,
        Tarjeta
    }
}
