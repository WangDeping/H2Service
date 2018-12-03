namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addallowedhasuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "IsAllowedHasUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "IsAllowedHasUser");
        }
    }
}
