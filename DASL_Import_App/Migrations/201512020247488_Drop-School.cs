namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropSchool : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Schools");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SchoolId = c.Int(nullable: false),
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
            
        }
    }
}
