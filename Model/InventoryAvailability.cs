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
    
    public partial class InventoryAvailability
    {
        public int InstrumentId { get; set; }
        public int Quantity { get; set; }
        public string TreatmentName { get; set; }
        public Nullable<int> UsesLeft { get; set; }
        public Nullable<int> InstrumentCommentId { get; set; }
        public Nullable<bool> IsChecked { get; set; }
        public string InstrumentName { get; set; }
        public string Comment { get; set; }
        public Nullable<int> MaxUses { get; set; }
        public Nullable<int> TreatmentId { get; set; }
    }
}
