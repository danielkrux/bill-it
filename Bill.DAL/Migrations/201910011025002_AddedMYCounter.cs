namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMYCounter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonthYearCounters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Month = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        CurrentCounter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MonthYearCounters");
        }
    }
}
