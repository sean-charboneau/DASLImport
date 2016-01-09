namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Periods : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchoolPeriods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        School = c.String(),
                        PeriodNumber = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Schools");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolId = c.String(),
                        DistrictSchoolId = c.String(),
                        SchoolName = c.String(),
                        PrincipalName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZIP = c.String(),
                        PhoneNumber = c.String(),
                        ExternalRefId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.SchoolPeriods");
        }
    }
}
