namespace WebUoFASM1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Add2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleViewModels",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropTable("dbo.RoleViewModels");
        }
    }
}