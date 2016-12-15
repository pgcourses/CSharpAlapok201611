namespace _01CodeFirst.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubjecttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            Sql("set identity_insert Subjects on");
            Sql("insert Subjects (Id, Name) values (1, 'Matematika')");
            Sql("insert Subjects (Id, Name) values (2, 'Magyar')");
            Sql("set identity_insert Subjects off");

        }

        public override void Down()
        {
            Sql("delete Subjects");
            DropTable("dbo.Subjects");
        }
    }
}
