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
    
    public partial class Instrument
    {
        public Instrument()
        {
            this.Events = new HashSet<Event>();
            this.InstrumentComments = new HashSet<InstrumentComment>();
            this.Treatments = new HashSet<Treatment>();
        }
    
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int DrawerId { get; set; }
        public Nullable<int> UsesLeft { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> MaxUses { get; set; }
    
        public virtual Drawer Drawer { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<InstrumentComment> InstrumentComments { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
