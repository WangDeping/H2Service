namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodel : DbMigration
    {
        public override void Up()
        {
           
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "EquipmentModelId", "dbo.EquipmentModels");
            DropForeignKey("dbo.Equipments", "EquipmentModelId", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Equipments", "EquipmentType_Id", "dbo.EquipmentTypes");
            DropIndex("dbo.Equipments", new[] { "EquipmentType_Id" });
            DropIndex("dbo.Equipments", new[] { "EquipmentModelId" });
            DropColumn("dbo.Equipments", "EquipmentType_Id");
            DropColumn("dbo.Equipments", "EquipmentModelId");
            DropTable("dbo.EquipmentModels");
        }
    }
}
