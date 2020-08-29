namespace WebUoFASM1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Drop : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Enrollments", "TraineeId", "dbo.Trainees");
            DropIndex("dbo.Enrollments", new[] { "CourseId" });
            DropIndex("dbo.Enrollments", new[] { "TraineeId" });
            DropTable("dbo.Enrollments");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Enrollments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CourseId = c.Int(nullable: false),
                    TraineeId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateIndex("dbo.Enrollments", "TraineeId");
            CreateIndex("dbo.Enrollments", "CourseId");
            AddForeignKey("dbo.Enrollments", "TraineeId", "dbo.Trainees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}