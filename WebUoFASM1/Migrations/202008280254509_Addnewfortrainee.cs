namespace WebUoFASM1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Addnewfortrainee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainees", "Education", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Trainees", "Education");
        }
    }
}