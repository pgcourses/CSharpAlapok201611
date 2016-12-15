namespace _01CodeFirst.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addconnectionTeachertoSubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "Subject_Id", c => c.Int());
            CreateIndex("dbo.Teachers", "Subject_Id");
            AddForeignKey("dbo.Teachers", "Subject_Id", "dbo.Subjects", "Id");

            Sql("update Teachers set Subject_id = 1 where Id = 1");
            Sql("update Teachers set Subject_id = 2 where Id = 2");

        }

        public override void Down()
        {
            Sql("update Teachers set Subject_id = null");

            DropForeignKey("dbo.Teachers", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Teachers", new[] { "Subject_Id" });
            DropColumn("dbo.Teachers", "Subject_Id");
        }
    }
}
