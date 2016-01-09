namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PeriodTimes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SchoolPeriods", "StartTime", c => c.String());
            AlterColumn("dbo.SchoolPeriods", "EndTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SchoolPeriods", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.SchoolPeriods", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
