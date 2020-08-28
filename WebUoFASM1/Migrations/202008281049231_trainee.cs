namespace WebUoFASM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class trainee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trainees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Trainers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Trainees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Trainers", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.TraineeInfoes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TraineeId = c.String(maxLength: 128),
                    Name = c.String(),
                    Email = c.String(),
                    Education = c.String(),
                    PhoneNumber = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId)
                .Index(t => t.TraineeId);

            CreateTable(
                "dbo.TrainerInfoes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TrainerId = c.String(maxLength: 128),
                    Name = c.String(),
                    Email = c.String(),
                    Type = c.String(),
                    PhoneNumber = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerId)
                .Index(t => t.TrainerId);

            DropTable("dbo.Trainees");
            DropTable("dbo.Trainers");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.Trainers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Type = c.String(),
                    PhoneNumber = c.String(),
                    ApplicationUser_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Trainees",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(),
                    Email = c.String(),
                    FullName = c.String(),
                    Education = c.String(),
                    PhoneNumber = c.String(),
                    ApplicationUser_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            DropForeignKey("dbo.TrainerInfoes", "TrainerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TraineeInfoes", "TraineeId", "dbo.AspNetUsers");
            DropIndex("dbo.TrainerInfoes", new[] { "TrainerId" });
            DropIndex("dbo.TraineeInfoes", new[] { "TraineeId" });
            DropTable("dbo.TrainerInfoes");
            DropTable("dbo.TraineeInfoes");
            CreateIndex("dbo.Trainers", "ApplicationUser_Id");
            CreateIndex("dbo.Trainees", "ApplicationUser_Id");
            AddForeignKey("dbo.Trainers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Trainees", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}