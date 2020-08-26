namespace WebUoFASM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainers", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Trainers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Trainees", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Trainees", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Trainers", "ApplicationUser_Id");
            CreateIndex("dbo.Trainees", "ApplicationUser_Id");
            AddForeignKey("dbo.Trainers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Trainees", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Trainers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Trainees", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Trainers", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Trainees", "ApplicationUser_Id");
            DropColumn("dbo.Trainees", "UserId");
            DropColumn("dbo.Trainers", "ApplicationUser_Id");
            DropColumn("dbo.Trainers", "UserId");
        }
    }
}
