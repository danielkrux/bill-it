namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceCodeToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "Code", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "Code", c => c.String());
        }
    }
}
