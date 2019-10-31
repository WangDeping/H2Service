namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodel51 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EquipmentPatrolLogs", "Summary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EquipmentPatrolLogs", "Summary");
        }
    }
}
