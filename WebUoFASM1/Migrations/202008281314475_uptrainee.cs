namespace WebUoFASM1.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class uptrainee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TraineeInfoes", "TraineeId", "dbo.AspNetUsers");
            DropIndex("dbo.TraineeInfoes", new[] { "TraineeId" });
            AlterColumn("dbo.TraineeInfoes", "TraineeId", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.TraineeInfoes", "TraineeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TraineeInfoes", "TraineeId");
            AddForeignKey("dbo.TraineeInfoes", "TraineeId", "dbo.AspNetUsers", "Id");
        }
    }
}