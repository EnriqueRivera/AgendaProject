//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient
    {
        public Patient()
        {
            this.Events = new HashSet<Event>();
            this.LaboratoryWorks = new HashSet<LaboratoryWork>();
            this.OutgoingInvoices = new HashSet<OutgoingInvoice>();
            this.Budgets = new HashSet<Budget>();
        }
    
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }
        public System.DateTime CaptureDate { get; set; }
        public bool HasHealthInsurance { get; set; }
        public Nullable<int> ClinicHistoryId { get; set; }
        public int DataCapturerId { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<LaboratoryWork> LaboratoryWorks { get; set; }
        public virtual ICollection<OutgoingInvoice> OutgoingInvoices { get; set; }
        public virtual User User { get; set; }
        public virtual ClinicHistory ClinicHistory { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
    }
}
