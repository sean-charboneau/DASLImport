namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchool : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Schools");
        }
    }
}
