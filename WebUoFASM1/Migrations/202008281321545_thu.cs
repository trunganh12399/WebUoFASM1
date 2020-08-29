namespace WebUoFASM1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class thu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraineeInfoes", "Trainer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.TraineeInfoes", "Trainer_Id");
            AddForeignKey("dbo.TraineeInfoes", "Trainer_Id", "dbo.AspNetUsers", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.TraineeInfoes", "Trainer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeInfoes", new[] { "Trainer_Id" });
            DropColumn("dbo.TraineeInfoes", "Trainer_Id");
        }
    }
}