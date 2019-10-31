namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeqprotery : DbMigration
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
                .ForeignKey("dbo.EquipmentProperties", t => t.PatrolId, cascadeDelete: true)
                .Index(t => t.PatrolId);
            
            CreateTable(
                "dbo.EquipmentProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentPropertyName = c.String(),
                        PartrolType = c.Int(nullable: false),
                        Frequency = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EquipmentTypeProterties",
                c => new
                    {
                        EquipmentTypeID = c.Int(nullable: false),
                        EquipmentPropertyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EquipmentTypeID, t.EquipmentPropertyID })
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentTypeID, cascadeDelete: true)
                .ForeignKey("dbo.EquipmentProperties", t => t.EquipmentPropertyID, cascadeDelete: true)
                .Index(t => t.EquipmentTypeID)
                .Index(t => t.EquipmentPropertyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentPatrolDetails", "PatrolId", "dbo.EquipmentProperties");
            DropForeignKey("dbo.EquipmentTypeProterties", "EquipmentPropertyID", "dbo.EquipmentProperties");
            DropForeignKey("dbo.EquipmentTypeProterties", "EquipmentTypeID", "dbo.EquipmentTypes");
            DropForeignKey("dbo.EquipmentPatrolDetails", "PatrolId", "dbo.EquipmentPatrolLogs");
            DropIndex("dbo.EquipmentTypeProterties", new[] { "EquipmentPropertyID" });
            DropIndex("dbo.EquipmentTypeProterties", new[] { "EquipmentTypeID" });
            DropIndex("dbo.EquipmentPatrolDetails", new[] { "PatrolId" });
            DropTable("dbo.EquipmentTypeProterties");
            DropTable("dbo.EquipmentProperties");
            DropTable("dbo.EquipmentPatrolDetails");
        }
    }
}
