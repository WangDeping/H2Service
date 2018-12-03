namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coluser2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Code", c => c.String());
            AddColumn("dbo.MedicalWasteFlows", "CollectUserId2", c => c.Long());
            CreateIndex("dbo.MedicalWasteFlows", "CollectUserId2");
            AddForeignKey("dbo.MedicalWasteFlows", "CollectUserId2", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalWasteFlows", "CollectUserId2", "dbo.Users");
            DropIndex("dbo.MedicalWasteFlows", new[] { "CollectUserId2" });
            DropColumn("dbo.MedicalWasteFlows", "CollectUserId2");
            DropColumn("dbo.Users", "Code");
        }
    }
}
