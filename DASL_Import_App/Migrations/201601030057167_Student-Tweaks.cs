namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentTweaks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "School", c => c.String());
            DropColumn("dbo.Students", "SchoolId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "SchoolId", c => c.String());
            DropColumn("dbo.Students", "School");
        }
    }
}
