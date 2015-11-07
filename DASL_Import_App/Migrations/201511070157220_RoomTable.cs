namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomTable : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rooms");
        }
    }
}
