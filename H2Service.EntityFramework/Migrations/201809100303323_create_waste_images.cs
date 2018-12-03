namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_waste_images : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalWasteImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgPath = c.String(),
                        FlowId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorUserId)
                .ForeignKey("dbo.MedicalWasteFlows", t => t.FlowId, cascadeDelete: true)
                .Index(t => t.FlowId)
                .Index(t => t.CreatorUserId);
            
            DropColumn("dbo.MedicalWasteFlows", "ImgsPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalWasteFlows", "ImgsPath", c => c.String());
            DropForeignKey("dbo.MedicalWasteImages", "FlowId", "dbo.MedicalWasteFlows");
            DropForeignKey("dbo.MedicalWasteImages", "CreatorUserId", "dbo.Users");
            DropIndex("dbo.MedicalWasteImages", new[] { "CreatorUserId" });
            DropIndex("dbo.MedicalWasteImages", new[] { "FlowId" });
            DropTable("dbo.MedicalWasteImages");
        }
    }
}
