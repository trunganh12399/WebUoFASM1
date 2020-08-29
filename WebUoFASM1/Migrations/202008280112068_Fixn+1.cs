namespace WebUoFASM1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Fixn1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignTraineeToCourses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TraineeId = c.String(maxLength: 128),
                    CourseId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId)
                .Index(t => t.TraineeId)
                .Index(t => t.CourseId);

            CreateTable(
                "dbo.AssignTrainerToTopics",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TrainerId = c.String(maxLength: 128),
                    TopicId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerId)
                .Index(t => t.TrainerId)
                .Index(t => t.TopicId);

            AddColumn("dbo.Trainers", "PhoneNumber", c => c.String());
            AddColumn("dbo.Trainees", "FullName", c => c.String());
            AlterColumn("dbo.Trainers", "Name", c => c.String());
            AlterColumn("dbo.Trainers", "Type", c => c.String());
            AlterColumn("dbo.Trainees", "Email", c => c.String());
            AlterColumn("dbo.Trainees", "Phone", c => c.String());
            AlterColumn("dbo.Trainees", "UserId", c => c.String());
            DropColumn("dbo.Trainers", "Email");
            DropColumn("dbo.Trainers", "Phone");
            DropColumn("dbo.Trainers", "UserId");
            DropColumn("dbo.Trainees", "Name");
            DropColumn("dbo.Trainees", "Education");
        }

        public override void Down()
        {
            AddColumn("dbo.Trainees", "Education", c => c.String(nullable: false));
            AddColumn("dbo.Trainees", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Trainers", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Trainers", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.Trainers", "Email", c => c.String(nullable: false));
            DropForeignKey("dbo.AssignTrainerToTopics", "TrainerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignTrainerToTopics", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.AssignTraineeToCourses", "TraineeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AssignTraineeToCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.AssignTrainerToTopics", new[] { "TopicId" });
            DropIndex("dbo.AssignTrainerToTopics", new[] { "TrainerId" });
            DropIndex("dbo.AssignTraineeToCourses", new[] { "CourseId" });
            DropIndex("dbo.AssignTraineeToCourses", new[] { "TraineeId" });
            AlterColumn("dbo.Trainees", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Trainees", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Trainees", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Trainers", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Trainers", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Trainees", "FullName");
            DropColumn("dbo.Trainers", "PhoneNumber");
            DropTable("dbo.AssignTrainerToTopics");
            DropTable("dbo.AssignTraineeToCourses");
        }
    }
}