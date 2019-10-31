namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmod10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EquipmentPatrolDetails", "EquipmentPatrolLog_Id", "dbo.EquipmentPatrolLogs");
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "EquipmentPatrolLog_Id" });
            DropColumn("dbo.EquipmentPatrolDetails", "EquipmentPatrolLog_Id");
            //RenameColumn(table: "dbo.EquipmentPatrolDetails", name: "EquipmentPatrolLog_Id", newName: "PatrolId");
            AlterColumn("dbo.EquipmentPatrolDetails", "PatrolId", c => c.Int(nullable: false));
            CreateIndex("dbo.EquipmentPatrolDetails", "PatrolId");
            CreateIndex("dbo.EquipmentPatrolDetails", "PropertyId");
            AddForeignKey("dbo.EquipmentPatrolDetails", "PropertyId", "dbo.EquipmentProperties", "Id", cascadeDelete: false);
            AddForeignKey("dbo.EquipmentPatrolDetails", "PatrolId", "dbo.EquipmentPatrolLogs", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentPatrolDetails", "PatrolId", "dbo.EquipmentPatrolLogs");
            DropForeignKey("dbo.EquipmentPatrolDetails", "PropertyId", "dbo.EquipmentProperties");
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "PropertyId" });
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "PatrolId" });
            AlterColumn("dbo.EquipmentPatrolDetails", "PatrolId", c => c.Int());
            RenameColumn(table: "dbo.EquipmentPatrolDetails", name: "PatrolId", newName: "EquipmentPatrolLog_Id");
            AddColumn("dbo.EquipmentPatrolDetails", "PatrolId", c => c.Int(nullable: false));
            CreateIndex("dbo.EquipmentPatrolDetails", "EquipmentPatrolLog_Id");
            AddForeignKey("dbo.EquipmentPatrolDetails", "EquipmentPatrolLog_Id", "dbo.EquipmentPatrolLogs", "Id");
        }
    }
}
