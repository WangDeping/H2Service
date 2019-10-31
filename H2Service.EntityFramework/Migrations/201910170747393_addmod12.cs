namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmod12 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentPatrolLogs", t => t.PatrolId, cascadeDelete: true)
                .ForeignKey("dbo.EquipmentProperties", t => t.PropertyId, cascadeDelete: true)
                .Index(t => t.PatrolId)
                .Index(t => t.PropertyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentPatrolDetails", "PropertyId", "dbo.EquipmentProperties");
            DropForeignKey("dbo.EquipmentPatrolDetails", "PatrolId", "dbo.EquipmentPatrolLogs");
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "PropertyId" });
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "PatrolId" });
            DropTable("dbo.EquipmentPatrolDetails");
        }
    }
}
