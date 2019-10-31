namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfk : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.EquipmentLoanLogs", "EquipmentId");
            AddForeignKey("dbo.EquipmentLoanLogs", "EquipmentId", "dbo.Equipments", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentLoanLogs", "EquipmentId", "dbo.Equipments");
            DropIndex("dbo.EquipmentLoanLogs", new[] { "EquipmentId" });
        }
    }
}
