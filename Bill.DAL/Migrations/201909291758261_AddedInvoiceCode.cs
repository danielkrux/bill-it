namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInvoiceCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "Code");
        }
    }
}
