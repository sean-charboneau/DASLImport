namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StaffMembers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StaffMembers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeacherId = c.Long(nullable: false),
                        DistrictStaffId = c.String(),
                        SchoolId = c.String(),
                        FirstName = c.String(),
                        MidName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        SchoolName = c.String(),
                        SchoolShortName = c.String(),
                        ExternalRefId = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.TeacherId, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.StaffMembers", new[] { "TeacherId" });
            DropTable("dbo.StaffMembers");
        }
    }
}
