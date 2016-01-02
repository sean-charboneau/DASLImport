namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DistrictCourseId = c.String(),
                        CourseName = c.String(),
                        SchoolId = c.String(),
                        ClassId = c.String(),
                        CourseId = c.String(),
                        ExternalRefId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Classes");
        }
    }
}
