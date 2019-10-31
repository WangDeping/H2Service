namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmodel2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.EquipmentModels",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   EquipmentModelName = c.String(),
               })
               .PrimaryKey(t => t.Id);

            AddColumn("dbo.Equipments", "EquipmentModelId", c => c.Int(nullable: false));
            AddColumn("dbo.Equipments", "EquipmentType_Id", c => c.Int());
            CreateIndex("dbo.Equipments", "EquipmentModelId");
            CreateIndex("dbo.Equipments", "EquipmentType_Id");            
            DropForeignKey("dbo.Equipments", "EquipmentType_Id", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Equipments", "EquipmentModelId", "dbo.EquipmentModels");
            DropIndex("dbo.Equipments", new[] { "EquipmentType_Id" });
            DropColumn("dbo.Equipments", "EquipmentType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipments", "EquipmentType_Id", c => c.Int());
            CreateIndex("dbo.Equipments", "EquipmentType_Id");
            AddForeignKey("dbo.Equipments", "EquipmentModelId", "dbo.EquipmentModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Equipments", "EquipmentType_Id", "dbo.EquipmentTypes", "Id");
        }
    }
}
