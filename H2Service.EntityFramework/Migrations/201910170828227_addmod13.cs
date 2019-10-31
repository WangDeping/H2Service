namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmod13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EquipmentPatrolLogs", "MainCheck", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EquipmentPatrolLogs", "MainCheck");
        }
    }
}
