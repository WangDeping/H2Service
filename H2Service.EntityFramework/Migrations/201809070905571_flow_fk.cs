namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flow_fk : DbMigration
    {
        public override void Up()
        {
           
            CreateIndex("dbo.MedicalWastes", "FlowId");
            AddForeignKey("dbo.MedicalWastes", "FlowId", "dbo.MedicalWasteFlows", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalWastes", "MedicalWasteFlowId", "dbo.MedicalWasteFlows");
            DropIndex("dbo.MedicalWastes", new[] { "MedicalWasteFlowId" });
            DropColumn("dbo.MedicalWastes", "MedicalWasteFlowId");
        }
    }
}
