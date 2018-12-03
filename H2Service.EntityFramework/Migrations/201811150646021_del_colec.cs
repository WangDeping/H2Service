namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class del_colec : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalWasteFlows", "CollectUserId", "dbo.ExternalUsers");
            DropIndex("dbo.MedicalWasteFlows", new[] { "CollectUserId" });
            DropColumn("dbo.MedicalWasteFlows", "CollectUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalWasteFlows", "CollectUserId", c => c.Int());
            CreateIndex("dbo.MedicalWasteFlows", "CollectUserId");
            AddForeignKey("dbo.MedicalWasteFlows", "CollectUserId", "dbo.ExternalUsers", "Id");
        }
    }
}
