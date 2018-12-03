namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetransportuser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MedicalWasteFlows", new[] { "TransportUserId" });
            AlterColumn("dbo.MedicalWasteFlows", "TransportUserId", c => c.Long());
            CreateIndex("dbo.MedicalWasteFlows", "TransportUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MedicalWasteFlows", new[] { "TransportUserId" });
            AlterColumn("dbo.MedicalWasteFlows", "TransportUserId", c => c.Int());
            CreateIndex("dbo.MedicalWasteFlows", "TransportUserId");
        }
    }
}
