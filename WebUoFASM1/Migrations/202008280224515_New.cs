namespace WebUoFASM1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class New : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AssignTraineeToCourses", newName: "Enrollments");
        }

        public override void Down()
        {
            RenameTable(name: "dbo.Enrollments", newName: "AssignTraineeToCourses");
        }
    }
}