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
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
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
        public DbSet<Dotation> Dotations { get; set; }
        public DbSet<ClinicHistory> ClinicHistories { get; set; }
        public DbSet<ClinicHistoryAttribute> ClinicHistoryAttributes { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetDetail> BudgetDetails { get; set; }
        public DbSet<BudgetTreatment> BudgetTreatments { get; set; }
        public DbSet<AmericanExpressPaid> AmericanExpressPaids { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<TreatmentPrice> TreatmentPrices { get; set; }
        public DbSet<Drawer> Drawers { get; set; }
        public DbSet<InstrumentComment> InstrumentComments { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<InventorySignature> InventorySignatures { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<PaymentFolio> PaymentFolios { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<TreatmentPayment> TreatmentPayments { get; set; }
        public DbSet<PositiveBalance> PositiveBalances { get; set; }
    
        public virtual ObjectResult<InventoryAvailability> GetInventoryAvailability(Nullable<int> drawerId, Nullable<int> year, Nullable<int> month)
        {
            var drawerIdParameter = drawerId.HasValue ?
                new ObjectParameter("drawerId", drawerId) :
                new ObjectParameter("drawerId", typeof(int));
    
            var yearParameter = year.HasValue ?
                new ObjectParameter("year", year) :
                new ObjectParameter("year", typeof(int));
    
            var monthParameter = month.HasValue ?
                new ObjectParameter("month", month) :
                new ObjectParameter("month", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<InventoryAvailability>("GetInventoryAvailability", drawerIdParameter, yearParameter, monthParameter);
        }
    }
}
