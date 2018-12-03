namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class rrrr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalWastes", "CollectUserId", "dbo.ExternalUsers");
            DropForeignKey("dbo.MedicalWastes", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.MedicalWastes", "TransportUserId", "dbo.ExternalUsers");
            DropIndex("dbo.MedicalWastes", new[] { "DepartmentId" });
            DropIndex("dbo.MedicalWastes", new[] { "CollectUserId" });
            DropIndex("dbo.MedicalWastes", new[] { "TransportUserId" });
            CreateTable(
                "dbo.MedicalWasteFlows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        ImgsPath = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        NurseId = c.Long(),
                        CollectUserId = c.Int(),
                        CollectTime = c.DateTime(),
                        TransportUserId = c.Int(),
                        TransportTime = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MedicalWasteFlow_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExternalUsers", t => t.CollectUserId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.NurseId)
                .ForeignKey("dbo.ExternalUsers", t => t.TransportUserId)
                .Index(t => t.DepartmentId)
                .Index(t => t.NurseId)
                .Index(t => t.CollectUserId)
                .Index(t => t.TransportUserId);
            
            AlterColumn("dbo.MedicalWastes", "Weight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalWastes", "TransportTime", c => c.DateTime());
            AddColumn("dbo.MedicalWastes", "TransportUserId", c => c.Int());
            AddColumn("dbo.MedicalWastes", "CollectTime", c => c.DateTime());
            AddColumn("dbo.MedicalWastes", "CollectUserId", c => c.Int());
            AddColumn("dbo.MedicalWastes", "DepartmentId", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalWastes", "ImgsPath", c => c.String());
            AddColumn("dbo.MedicalWastes", "Status", c => c.Int(nullable: false));
            DropForeignKey("dbo.MedicalWasteFlows", "TransportUserId", "dbo.ExternalUsers");
            DropForeignKey("dbo.MedicalWasteFlows", "NurseId", "dbo.Users");
            DropForeignKey("dbo.MedicalWasteFlows", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.MedicalWasteFlows", "CollectUserId", "dbo.ExternalUsers");
            DropIndex("dbo.MedicalWasteFlows", new[] { "TransportUserId" });
            DropIndex("dbo.MedicalWasteFlows", new[] { "CollectUserId" });
            DropIndex("dbo.MedicalWasteFlows", new[] { "NurseId" });
            DropIndex("dbo.MedicalWasteFlows", new[] { "DepartmentId" });
            AlterColumn("dbo.MedicalWastes", "Weight", c => c.Single(nullable: false));
            DropTable("dbo.MedicalWasteFlows",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MedicalWasteFlow_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            CreateIndex("dbo.MedicalWastes", "TransportUserId");
            CreateIndex("dbo.MedicalWastes", "CollectUserId");
            CreateIndex("dbo.MedicalWastes", "DepartmentId");
            AddForeignKey("dbo.MedicalWastes", "TransportUserId", "dbo.ExternalUsers", "Id");
            AddForeignKey("dbo.MedicalWastes", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MedicalWastes", "CollectUserId", "dbo.ExternalUsers", "Id");
        }
    }
}
