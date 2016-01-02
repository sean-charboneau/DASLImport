namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtraClassFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "RoomId", c => c.String());
            AddColumn("dbo.Classes", "TeacherId", c => c.String());
            AddColumn("dbo.Classes", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Classes", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "EndTime");
            DropColumn("dbo.Classes", "StartTime");
            DropColumn("dbo.Classes", "TeacherId");
            DropColumn("dbo.Classes", "RoomId");
        }
    }
}
