namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmod11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EquipmentPatrolDetails", "PatrolId", "dbo.EquipmentPatrolLogs");
            DropForeignKey("dbo.EquipmentPatrolDetails", "PropertyId", "dbo.EquipmentProperties");
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "PatrolId" });
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "PropertyId" });
            DropTable("dbo.EquipmentPatrolDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EquipmentPatrolDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatrolId = c.Int(nullable: false),
                        PropertyId = c.Int(nullable: false),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.EquipmentPatrolDetails", "PropertyId");
            CreateIndex("dbo.EquipmentPatrolDetails", "PatrolId");
            AddForeignKey("dbo.EquipmentPatrolDetails", "PropertyId", "dbo.EquipmentProperties", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EquipmentPatrolDetails", "PatrolId", "dbo.EquipmentPatrolLogs", "Id", cascadeDelete: true);
        }
    }
}
