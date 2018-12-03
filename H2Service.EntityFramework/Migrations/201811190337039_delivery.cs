namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class delivery : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalWasteDeliveries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Summary = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        District_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MedicalWasteDelivery_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorUserId)
                .ForeignKey("dbo.Districts", t => t.District_Id)
                .Index(t => t.CreatorUserId)
                .Index(t => t.District_Id);
            
            AddColumn("dbo.MedicalWasteFlows", "MedicalWasteDeliveryId", c => c.Int());
            CreateIndex("dbo.MedicalWasteFlows", "MedicalWasteDeliveryId");
            AddForeignKey("dbo.MedicalWasteFlows", "MedicalWasteDeliveryId", "dbo.MedicalWasteDeliveries", "Id");
            DropColumn("dbo.MedicalWasteFlows", "DeliveryTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalWasteFlows", "DeliveryTime", c => c.DateTime());
            DropForeignKey("dbo.MedicalWasteFlows", "MedicalWasteDeliveryId", "dbo.MedicalWasteDeliveries");
            DropForeignKey("dbo.MedicalWasteDeliveries", "District_Id", "dbo.Districts");
            DropForeignKey("dbo.MedicalWasteDeliveries", "CreatorUserId", "dbo.Users");
            DropIndex("dbo.MedicalWasteDeliveries", new[] { "District_Id" });
            DropIndex("dbo.MedicalWasteDeliveries", new[] { "CreatorUserId" });
            DropIndex("dbo.MedicalWasteFlows", new[] { "MedicalWasteDeliveryId" });
            DropColumn("dbo.MedicalWasteFlows", "MedicalWasteDeliveryId");
            DropTable("dbo.MedicalWasteDeliveries",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MedicalWasteDelivery_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
