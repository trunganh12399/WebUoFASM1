namespace WebUoFASM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Details", "TrainerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Details", "TrainerId");
            AddForeignKey("dbo.Details", "TrainerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Details", "TrainerId", "dbo.AspNetUsers");
            DropIndex("dbo.Details", new[] { "TrainerId" });
            DropColumn("dbo.Details", "TrainerId");
        }
    }
}
