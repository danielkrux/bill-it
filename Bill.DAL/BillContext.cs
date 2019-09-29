using System.Data.Entity;
using Bill.DataModels;

namespace Bill.DAL
{
    public class BillContext:DbContext
    {
        public BillContext() : base("billDB") { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Invoices)
                .WithRequired(i => i.Client)
                .HasForeignKey(i => i.ClientID);

            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.InvoiceLines)
                .WithRequired(il => il.Invoice)
                .HasForeignKey(il => il.InvoiceID);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Invoices)
                .WithRequired(i => i.Company)
                .HasForeignKey(i => i.CompanyID);

            modelBuilder.Entity<InvoiceLine>()
                .Ignore(il => il.TotalCost);
        }
    }
}
