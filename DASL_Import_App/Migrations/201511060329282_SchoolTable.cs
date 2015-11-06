namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchoolTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RefId = c.String(),
                        LocalId = c.String(),
                        StateProvinceId = c.String(),
                        SchoolName = c.String(),
                        DistrictRefId = c.String(),
                        SchoolUrl = c.String(),
                        PrincipalName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        PostalCode = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Schools");
        }
    }
}
