namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "StudentId", c => c.String());
            AddColumn("dbo.Students", "SchoolId", c => c.String());
            AddColumn("dbo.Students", "Gender", c => c.String());
            AddColumn("dbo.Students", "DOB", c => c.String());
            AddColumn("dbo.Students", "ExternalRefId", c => c.String());
            DropColumn("dbo.Students", "RefId");
            DropColumn("dbo.Students", "LocalId");
            DropColumn("dbo.Students", "StateProvinceId");
            DropColumn("dbo.Students", "SchoolRefId");
            DropColumn("dbo.Students", "HomeroomNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "HomeroomNumber", c => c.String());
            AddColumn("dbo.Students", "SchoolRefId", c => c.String());
            AddColumn("dbo.Students", "StateProvinceId", c => c.String());
            AddColumn("dbo.Students", "LocalId", c => c.String());
            AddColumn("dbo.Students", "RefId", c => c.String());
            DropColumn("dbo.Students", "ExternalRefId");
            DropColumn("dbo.Students", "DOB");
            DropColumn("dbo.Students", "Gender");
            DropColumn("dbo.Students", "SchoolId");
            DropColumn("dbo.Students", "StudentId");
        }
    }
}
