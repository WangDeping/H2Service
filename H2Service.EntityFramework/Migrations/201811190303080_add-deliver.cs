namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddeliver : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalWasteFlows", "DeliveryUserId", c => c.Long());
            AddForeignKey("dbo.MedicalWasteFlows", "TransportUserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalWasteFlows", "TransportUserId", "dbo.Users");
            DropColumn("dbo.MedicalWasteFlows", "DeliveryUserId");
        }
    }
}
