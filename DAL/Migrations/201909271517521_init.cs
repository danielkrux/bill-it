namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Email = c.String(),
                        Street = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Finished = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        ClientID = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.ClientID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.InvoiceLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Amount = c.Int(nullable: false),
                        VAT = c.Double(nullable: false),
                        MyProperty = c.Int(nullable: false),
                        InvoiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Invoices", t => t.InvoiceID, cascadeDelete: true)
                .Index(t => t.InvoiceID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        VATNumber = c.String(),
                        Street = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.Invoices", "ClientID", "dbo.Clients");
            DropForeignKey("dbo.InvoiceLines", "InvoiceID", "dbo.Invoices");
            DropIndex("dbo.InvoiceLines", new[] { "InvoiceID" });
            DropIndex("dbo.Invoices", new[] { "CompanyID" });
            DropIndex("dbo.Invoices", new[] { "ClientID" });
            DropTable("dbo.Companies");
            DropTable("dbo.InvoiceLines");
            DropTable("dbo.Invoices");
            DropTable("dbo.Clients");
        }
    }
}
