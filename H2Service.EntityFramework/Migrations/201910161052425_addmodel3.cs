namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodel3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipments", "EquipmentModelId", "dbo.EquipmentModels");
            DropIndex("dbo.Equipments", new[] { "EquipmentModelId" });
            DropColumn("dbo.Equipments", "EquipmentModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipments", "EquipmentModelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Equipments", "EquipmentModelId");
            AddForeignKey("dbo.Equipments", "EquipmentModelId", "dbo.EquipmentModels", "Id", cascadeDelete: true);
        }
    }
}
