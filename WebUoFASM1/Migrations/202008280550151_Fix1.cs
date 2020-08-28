namespace WebUoFASM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Fix1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Topics", "TrainerId", "dbo.Trainers");
            DropIndex("dbo.Topics", new[] { "TrainerId" });
            DropColumn("dbo.Topics", "TrainerId");
        }

        public override void Down()
        {
            AddColumn("dbo.Topics", "TrainerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Topics", "TrainerId");
            AddForeignKey("dbo.Topics", "TrainerId", "dbo.Trainers", "Id", cascadeDelete: true);
        }
    }
}