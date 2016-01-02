namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchoolRefactor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schools", "SchoolId", c => c.Int(nullable: false));
            AddColumn("dbo.Schools", "DistrictSchoolId", c => c.String());
            AddColumn("dbo.Schools", "Active", c => c.Int(nullable: false));
            AddColumn("dbo.Schools", "ZIP", c => c.String());
            AddColumn("dbo.Schools", "ExternalRefId", c => c.String());
            DropColumn("dbo.Schools", "RefId");
            DropColumn("dbo.Schools", "LocalId");
            DropColumn("dbo.Schools", "StateProvinceId");
            DropColumn("dbo.Schools", "DistrictRefId");
            DropColumn("dbo.Schools", "SchoolUrl");
            DropColumn("dbo.Schools", "Country");
            DropColumn("dbo.Schools", "PostalCode");
            DropTable("dbo.Districts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RefId = c.String(),
                        LocalId = c.String(),
                        StateProvinceId = c.String(),
                        LeaName = c.String(),
                        LeaUrl = c.String(),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        PostalCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Schools", "PostalCode", c => c.String());
            AddColumn("dbo.Schools", "Country", c => c.String());
            AddColumn("dbo.Schools", "SchoolUrl", c => c.String());
            AddColumn("dbo.Schools", "DistrictRefId", c => c.String());
            AddColumn("dbo.Schools", "StateProvinceId", c => c.String());
            AddColumn("dbo.Schools", "LocalId", c => c.String());
            AddColumn("dbo.Schools", "RefId", c => c.String());
            DropColumn("dbo.Schools", "ExternalRefId");
            DropColumn("dbo.Schools", "ZIP");
            DropColumn("dbo.Schools", "Active");
            DropColumn("dbo.Schools", "DistrictSchoolId");
            DropColumn("dbo.Schools", "SchoolId");
        }
    }
}
