namespace WebUoFASM1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAssign : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainees", "PhoneNumber", c => c.String());
            DropColumn("dbo.Trainees", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainees", "Phone", c => c.String());
            DropColumn("dbo.Trainees", "PhoneNumber");
        }
    }
}
