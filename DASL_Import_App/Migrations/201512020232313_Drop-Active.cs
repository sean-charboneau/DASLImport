namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropActive : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Schools", "Active");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schools", "Active", c => c.Int(nullable: false));
        }
    }
}
