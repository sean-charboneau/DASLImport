namespace DASL_Import_App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentClasses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentId = c.String(),
                        ClassId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudentClasses");
        }
    }
}
