namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentRewrite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "CourseCode", c => c.String());
            AddColumn("dbo.Classes", "SectionNumber", c => c.String());
            AddColumn("dbo.Classes", "TermCode", c => c.String());
            AddColumn("dbo.Classes", "Location", c => c.String());
            AddColumn("dbo.Classes", "TeacherCode", c => c.String());
            AddColumn("dbo.Classes", "PeriodNumber", c => c.String());
            DropColumn("dbo.Classes", "DistrictCourseId");
            DropColumn("dbo.Classes", "SchoolId");
            DropColumn("dbo.Classes", "ClassId");
            DropColumn("dbo.Classes", "CourseId");
            DropColumn("dbo.Classes", "RoomId");
            DropColumn("dbo.Classes", "TeacherId");
            DropColumn("dbo.Classes", "StartTime");
            DropColumn("dbo.Classes", "EndTime");
            DropColumn("dbo.Students", "MiddleName");
            DropColumn("dbo.Students", "DOB");
            DropTable("dbo.Rooms");
            DropTable("dbo.Staffs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RefId = c.String(),
                        LocalId = c.String(),
                        StateProvinceId = c.String(),
                        SchoolRefId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RefId = c.String(),
                        SchoolRefId = c.String(),
                        StaffRefId = c.String(),
                        RoomNumber = c.String(),
                        Capacity = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Students", "DOB", c => c.String());
            AddColumn("dbo.Students", "MiddleName", c => c.String());
            AddColumn("dbo.Classes", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Classes", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Classes", "TeacherId", c => c.String());
            AddColumn("dbo.Classes", "RoomId", c => c.String());
            AddColumn("dbo.Classes", "CourseId", c => c.String());
            AddColumn("dbo.Classes", "ClassId", c => c.String());
            AddColumn("dbo.Classes", "SchoolId", c => c.String());
            AddColumn("dbo.Classes", "DistrictCourseId", c => c.String());
            DropColumn("dbo.Classes", "PeriodNumber");
            DropColumn("dbo.Classes", "TeacherCode");
            DropColumn("dbo.Classes", "Location");
            DropColumn("dbo.Classes", "TermCode");
            DropColumn("dbo.Classes", "SectionNumber");
            DropColumn("dbo.Classes", "CourseCode");
        }
    }
}
