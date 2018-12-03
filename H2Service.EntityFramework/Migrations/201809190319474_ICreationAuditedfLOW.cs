namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ICreationAuditedfLOW : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalWasteFlows", "CreatorUserId", c => c.Long());
            AddColumn("dbo.MedicalWasteFlows", "CreationTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicalWasteFlows", "CreationTime");
            DropColumn("dbo.MedicalWasteFlows", "CreatorUserId");
        }
    }
}
