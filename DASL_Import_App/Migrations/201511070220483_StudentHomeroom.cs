namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentHomeroom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "HomeroomNumber", c => c.String());
            DropColumn("dbo.Students", "PhoneNumber");
            DropColumn("dbo.Students", "HomeroomLocalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "HomeroomLocalId", c => c.String());
            AddColumn("dbo.Students", "PhoneNumber", c => c.String());
            DropColumn("dbo.Students", "HomeroomNumber");
        }
    }
}
