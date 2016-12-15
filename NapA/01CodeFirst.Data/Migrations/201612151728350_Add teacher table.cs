namespace _01CodeFirst.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addteachertable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        ClassCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("set identity_insert Teachers on");
            Sql("insert Teachers (Id, FirstName, LastName, ClassCode) values (2, 'Gipsz', 'Jakab', '1/A')");
            Sql("insert Teachers (Id, FirstName, LastName, ClassCode) values (1, 'Forgó', 'Morgó', '2/A')");
            Sql("set identity_insert Teachers off");

        }

        public override void Down()
        {
            Sql("delete Teachers");
            DropTable("dbo.Teachers");
        }
    }
}
