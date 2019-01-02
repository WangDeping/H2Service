namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class maintenance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaintenanceOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaintenanceDepartmentId = c.Int(nullable: false),
                        MaintenanceEngineerId = c.Long(),
                        MaintenanceRemarks = c.String(),
                        CallerId = c.Long(),
                        CallerDepartmentId = c.Int(nullable: false),
                        Trouble = c.String(),
                        Remarks = c.String(),
                        CompletionTime = c.DateTime(),
                        Status = c.Int(nullable: false),
                        RecorderId = c.Long(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ArrivalSpeed = c.Single(nullable: false),
                        RepairEfficiency = c.Single(nullable: false),
                        Service = c.Single(nullable: false),
                        Quality = c.Single(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MaintenanceOrder_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CallerId)
                .ForeignKey("dbo.Departments", t => t.CallerDepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.CreatorUserId)
                .ForeignKey("dbo.Departments", t => t.MaintenanceDepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.MaintenanceEngineerId)
                .ForeignKey("dbo.Users", t => t.RecorderId)
                .Index(t => t.MaintenanceDepartmentId)
                .Index(t => t.MaintenanceEngineerId)
                .Index(t => t.CallerId)
                .Index(t => t.CallerDepartmentId)
                .Index(t => t.RecorderId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.MaintenanceTraces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorUserId)
                .ForeignKey("dbo.MaintenanceOrders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.CreatorUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaintenanceOrders", "RecorderId", "dbo.Users");
            DropForeignKey("dbo.MaintenanceTraces", "OrderId", "dbo.MaintenanceOrders");
            DropForeignKey("dbo.MaintenanceTraces", "CreatorUserId", "dbo.Users");
            DropForeignKey("dbo.MaintenanceOrders", "MaintenanceEngineerId", "dbo.Users");
            DropForeignKey("dbo.MaintenanceOrders", "MaintenanceDepartmentId", "dbo.Departments");
            DropForeignKey("dbo.MaintenanceOrders", "CreatorUserId", "dbo.Users");
            DropForeignKey("dbo.MaintenanceOrders", "CallerDepartmentId", "dbo.Departments");
            DropForeignKey("dbo.MaintenanceOrders", "CallerId", "dbo.Users");
            DropIndex("dbo.MaintenanceTraces", new[] { "CreatorUserId" });
            DropIndex("dbo.MaintenanceTraces", new[] { "OrderId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "CreatorUserId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "RecorderId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "CallerDepartmentId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "CallerId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "MaintenanceEngineerId" });
            DropIndex("dbo.MaintenanceOrders", new[] { "MaintenanceDepartmentId" });
            DropTable("dbo.MaintenanceTraces");
            DropTable("dbo.MaintenanceOrders",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MaintenanceOrder_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
