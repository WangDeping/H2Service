namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodel6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EquipmentPatrolDetails", "PropertyId");
            RenameColumn(table: "dbo.EquipmentPatrolDetails", name: "PatrolId", newName: "PropertyId");
            RenameIndex(table: "dbo.EquipmentPatrolDetails", name: "IX_PatrolId", newName: "IX_PropertyId");
            CreateIndex("dbo.EquipmentPatrolDetails", "PatrolId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "PatrolId" });
            RenameIndex(table: "dbo.EquipmentPatrolDetails", name: "IX_PropertyId", newName: "IX_PatrolId");
            RenameColumn(table: "dbo.EquipmentPatrolDetails", name: "PropertyId", newName: "PatrolId");
            AddColumn("dbo.EquipmentPatrolDetails", "PropertyId", c => c.Int(nullable: false));
        }
    }
}
