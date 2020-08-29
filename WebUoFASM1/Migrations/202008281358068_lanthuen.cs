namespace WebUoFASM1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class lanthuen : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TraineeInfoes", new[] { "Trainer_Id" });
            DropColumn("dbo.TraineeInfoes", "TraineeId");
            RenameColumn(table: "dbo.TraineeInfoes", name: "Trainer_Id", newName: "TraineeId");
            AlterColumn("dbo.TraineeInfoes", "TraineeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TraineeInfoes", "TraineeId");
        }

        public override void Down()
        {
            DropIndex("dbo.TraineeInfoes", new[] { "TraineeId" });
            AlterColumn("dbo.TraineeInfoes", "TraineeId", c => c.String());
            RenameColumn(table: "dbo.TraineeInfoes", name: "TraineeId", newName: "Trainer_Id");
            AddColumn("dbo.TraineeInfoes", "TraineeId", c => c.String());
            CreateIndex("dbo.TraineeInfoes", "Trainer_Id");
        }
    }
}