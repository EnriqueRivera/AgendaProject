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
    
    public partial class User
    {
        public User()
        {
            this.Events = new HashSet<Event>();
            this.EventStatusChanges = new HashSet<EventStatusChanx>();
            this.Logins = new HashSet<Login>();
            this.Medicines = new HashSet<Medicine>();
            this.Patients = new HashSet<Patient>();
            this.Reminders = new HashSet<Reminder>();
            this.CleanedActions = new HashSet<CleanedAction>();
        }
    
        public int UserId { get; set; }
        public int AssignedUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<EventStatusChanx> EventStatusChanges { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Medicine> Medicines { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Reminder> Reminders { get; set; }
        public virtual ICollection<CleanedAction> CleanedActions { get; set; }
    }
}
