using System.Data.Entity;
using Models;

namespace DAL
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
                .HasMany(c => c.Invoices);
        }
    }
}
