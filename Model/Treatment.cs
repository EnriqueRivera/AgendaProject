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
    
    public partial class Treatment
    {
        public Treatment()
        {
            this.Events = new HashSet<Event>();
        }
    
        public int TreatmentId { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<Event> Events { get; set; }
    }
}
