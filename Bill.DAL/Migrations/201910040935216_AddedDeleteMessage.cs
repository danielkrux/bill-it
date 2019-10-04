namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDeleteMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "DeleteMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "DeleteMessage");
        }
    }
}
