namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodel4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "EquipmentModelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Equipments", "EquipmentModelId");
            AddForeignKey("dbo.Equipments", "EquipmentModelId", "dbo.EquipmentModels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "EquipmentModelId", "dbo.EquipmentModels");
            DropIndex("dbo.Equipments", new[] { "EquipmentModelId" });
            DropColumn("dbo.Equipments", "EquipmentModelId");
        }
    }
}
