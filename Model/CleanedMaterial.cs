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
    
    public partial class CleanedMaterial
    {
        public int CleanedMaterialId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string GroupLetter { get; set; }
        public Nullable<int> Cleaned { get; set; }
        public Nullable<int> Packaged { get; set; }
        public Nullable<int> Sterilized { get; set; }
        public string Observations { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual CleanedAction CleanedAction { get; set; }
        public virtual CleanedAction PackagedAction { get; set; }
        public virtual CleanedAction SterilizedAction { get; set; }
    }
}
