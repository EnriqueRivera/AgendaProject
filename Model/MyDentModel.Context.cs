﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyDentDBEntities : DbContext
    {
        public MyDentDBEntities()
            : base("name=MyDentDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventStatusChanx> EventStatusChanges { get; set; }
        public DbSet<GeneralPaid> GeneralPaids { get; set; }
        public DbSet<LaboratoryWork> LaboratoryWorks { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<OutgoingInvoice> OutgoingInvoices { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ReceivedInvoice> ReceivedInvoices { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ResourceProvider> ResourceProviders { get; set; }
        public DbSet<Technical> Technicals { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CleanedMaterial> CleanedMaterials { get; set; }
        public DbSet<CleanedAction> CleanedActions { get; set; }
    }
}
