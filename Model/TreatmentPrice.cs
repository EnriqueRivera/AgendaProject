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
    
    public partial class TreatmentPrice
    {
        public TreatmentPrice()
        {
            this.TreatmentPayments = new HashSet<TreatmentPayment>();
        }
    
        public int TreatmentPriceId { get; set; }
        public string TreatmentKey { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string Type { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<TreatmentPayment> TreatmentPayments { get; set; }
    }
}
