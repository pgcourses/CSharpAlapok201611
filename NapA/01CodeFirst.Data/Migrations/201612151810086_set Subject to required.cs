namespace _01CodeFirst.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setSubjecttorequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teachers", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Teachers", new[] { "Subject_Id" });
            AlterColumn("dbo.Teachers", "Subject_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Teachers", "Subject_Id");
            AddForeignKey("dbo.Teachers", "Subject_Id", "dbo.Subjects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Teachers", new[] { "Subject_Id" });
            AlterColumn("dbo.Teachers", "Subject_Id", c => c.Int());
            CreateIndex("dbo.Teachers", "Subject_Id");
            AddForeignKey("dbo.Teachers", "Subject_Id", "dbo.Subjects", "Id");
        }
    }
}
