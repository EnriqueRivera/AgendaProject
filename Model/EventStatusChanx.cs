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
    
    public partial class EventStatusChanx
    {
        public int EventStatusChangeId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public System.DateTime ChangeDate { get; set; }
        public int EventId { get; set; }
        public int StatusChangerId { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}