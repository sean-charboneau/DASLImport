namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveGender : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StaffMembers", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StaffMembers", "Gender", c => c.String());
        }
    }
}
