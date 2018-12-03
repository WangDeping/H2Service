namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class droptran : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalWasteFlows", "TransportUserId", "dbo.Users");
            DropIndex("dbo.MedicalWasteFlows", new[] { "TransportUserId" });
            AddColumn("dbo.MedicalWasteFlows", "DeliveryTime", c => c.DateTime());
            DropColumn("dbo.MedicalWasteFlows", "TransportUserId");
            DropColumn("dbo.MedicalWasteFlows", "TransportTime");
            DropColumn("dbo.MedicalWasteFlows", "DeliveryUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalWasteFlows", "DeliveryUserId", c => c.Long());
            AddColumn("dbo.MedicalWasteFlows", "TransportTime", c => c.DateTime());
            AddColumn("dbo.MedicalWasteFlows", "TransportUserId", c => c.Long());
            DropColumn("dbo.MedicalWasteFlows", "DeliveryTime");
            CreateIndex("dbo.MedicalWasteFlows", "TransportUserId");
            AddForeignKey("dbo.MedicalWasteFlows", "TransportUserId", "dbo.Users", "Id");
        }
    }
}
