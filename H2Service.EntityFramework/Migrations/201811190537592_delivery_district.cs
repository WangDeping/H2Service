namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delivery_district : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalWasteDeliveries", "District_Id", "dbo.Districts");
            DropIndex("dbo.MedicalWasteDeliveries", new[] { "District_Id" });
            RenameColumn(table: "dbo.MedicalWasteDeliveries", name: "District_Id", newName: "DistrictId");
            AlterColumn("dbo.MedicalWasteDeliveries", "DistrictId", c => c.Int(nullable: false));
            CreateIndex("dbo.MedicalWasteDeliveries", "DistrictId");
            AddForeignKey("dbo.MedicalWasteDeliveries", "DistrictId", "dbo.Districts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalWasteDeliveries", "DistrictId", "dbo.Districts");
            DropIndex("dbo.MedicalWasteDeliveries", new[] { "DistrictId" });
            AlterColumn("dbo.MedicalWasteDeliveries", "DistrictId", c => c.Int());
            RenameColumn(table: "dbo.MedicalWasteDeliveries", name: "DistrictId", newName: "District_Id");
            CreateIndex("dbo.MedicalWasteDeliveries", "District_Id");
            AddForeignKey("dbo.MedicalWasteDeliveries", "District_Id", "dbo.Districts", "Id");
        }
    }
}
