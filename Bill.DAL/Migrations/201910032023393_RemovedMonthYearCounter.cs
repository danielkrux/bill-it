namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedMonthYearCounter : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.MonthYearCounters");
        }
        
        public override void Down()
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
    }
}
