namespace _01CodeFirst.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudenttableamdconnectiontoteacher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);

            Sql("set identity_insert Students on");
            Sql("insert Students (Id, Name, Teacher_Id) values (1, 'Kis Józsi', 2)");
            Sql("insert Students (Id, Name, Teacher_Id) values (2, 'Nagy Ferenc', 1)");
            Sql("set identity_insert Students off");


    }
        
        public override void Down()
        {
            Sql("delete Students");
            DropForeignKey("dbo.Students", "Teacher_Id", "dbo.Teachers");
            DropIndex("dbo.Students", new[] { "Teacher_Id" });
            DropTable("dbo.Students");
        }
    }
}
