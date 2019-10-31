namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmode9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EquipmentPatrolDetails", "PropertyId", "dbo.EquipmentProperties");
            DropForeignKey("dbo.EquipmentPatrolDetails", "PatrolId", "dbo.EquipmentPatrolLogs");
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "PatrolId" });
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "PropertyId" });
            AddColumn("dbo.EquipmentPatrolDetails", "EquipmentPatrolLog_Id", c => c.Int());
            CreateIndex("dbo.EquipmentPatrolDetails", "EquipmentPatrolLog_Id");
            AddForeignKey("dbo.EquipmentPatrolDetails", "EquipmentPatrolLog_Id", "dbo.EquipmentPatrolLogs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentPatrolDetails", "EquipmentPatrolLog_Id", "dbo.EquipmentPatrolLogs");
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "EquipmentPatrolLog_Id" });
            DropColumn("dbo.EquipmentPatrolDetails", "EquipmentPatrolLog_Id");
            CreateIndex("dbo.EquipmentPatrolDetails", "PropertyId");
            CreateIndex("dbo.EquipmentPatrolDetails", "PatrolId");
            AddForeignKey("dbo.EquipmentPatrolDetails", "PatrolId", "dbo.EquipmentPatrolLogs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EquipmentPatrolDetails", "PropertyId", "dbo.EquipmentProperties", "Id", cascadeDelete: true);
        }
    }
}
