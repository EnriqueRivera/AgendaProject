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
    
    public partial class Statement
    {
        public Statement()
        {
            this.Payments = new HashSet<Payment>();
            this.TreatmentPayments = new HashSet<TreatmentPayment>();
        }
    
        public int StatementId { get; set; }
        public int PatientId { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public bool IsPaid { get; set; }
        public int UserId { get; set; }
    
        public virtual Patient Patient { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<TreatmentPayment> TreatmentPayments { get; set; }
        public virtual User User { get; set; }
    }
}
