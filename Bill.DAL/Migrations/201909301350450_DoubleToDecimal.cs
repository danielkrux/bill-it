namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoubleToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoiceLines", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceLines", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.InvoiceLines", "VAT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceLines", "VAT", c => c.Double(nullable: false));
            AlterColumn("dbo.InvoiceLines", "Discount", c => c.Double(nullable: false));
            AlterColumn("dbo.InvoiceLines", "Price", c => c.Double(nullable: false));
        }
    }
}
