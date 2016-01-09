namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdType : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Students", new[] { "StudentId" });
            AlterColumn("dbo.Students", "StudentId", c => c.Long(nullable: false));
            CreateIndex("dbo.Students", "StudentId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Students", new[] { "StudentId" });
            AlterColumn("dbo.Students", "StudentId", c => c.String());
            CreateIndex("dbo.Students", "StudentId", unique: true);
        }
    }
}
