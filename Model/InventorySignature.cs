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
    
    public partial class InventorySignature
    {
        public int InventorySignatureId { get; set; }
        public int Signature1 { get; set; }
        public int Signature2 { get; set; }
        public System.DateTime SignatureDate { get; set; }
        public int DrawerId { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual Drawer Drawer { get; set; }
    }
}
