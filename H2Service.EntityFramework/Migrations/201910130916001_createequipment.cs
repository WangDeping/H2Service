namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class createequipment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        EquipmentName = c.String(),
                        EquipmentKindId = c.Int(nullable: false),
                        EquipmentTypeId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        EquipmentImg = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Equipment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.EquipmentKinds", t => t.EquipmentKindId, cascadeDelete: true)
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentTypeId, cascadeDelete: true)
                .Index(t => t.EquipmentKindId)
                .Index(t => t.EquipmentTypeId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.EquipmentKinds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentKindName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EquipmentPatrolLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorUserId)               
                .Index(t => t.EquipmentId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EquipmentUsageLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentId = c.Int(nullable: false),
                        PatientAdmNo = c.String(),
                        PatientName = c.String(),
                        Diagnose = c.String(),
                        BeginTime = c.DateTime(),
                        BeginUserId = c.Long(),
                        EndTime = c.DateTime(),
                        EndUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.BeginUserId)
                .ForeignKey("dbo.Users", t => t.EndUserId)               
                .Index(t => t.EquipmentId)
                .Index(t => t.BeginUserId)
                .Index(t => t.EndUserId);
            
            CreateTable(
                "dbo.EquipmentLoanLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EquipmentId = c.Int(nullable: false),
                        LoanTime = c.DateTime(nullable: false),
                        LoanUserId = c.Long(nullable: false),
                        LoanDeptId = c.Int(nullable: false),
                        BorrowUserId = c.Long(nullable: false),
                        BorrowDeptId = c.Int(nullable: false),
                        ReturnTime = c.DateTime(),
                        ReturnUserId = c.Long(),
                        SignUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.LoanDeptId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LoanUserId, cascadeDelete: true)            
                .ForeignKey("dbo.Users", t => t.ReturnUserId)
                .ForeignKey("dbo.Users", t => t.SignUserId)
                .Index(t => t.EquipmentId)
                .Index(t => t.LoanUserId)
                .Index(t => t.LoanDeptId)
                .Index(t => t.ReturnUserId)
                .Index(t => t.SignUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentLoanLogs", "SignUserId", "dbo.Users");
            DropForeignKey("dbo.EquipmentLoanLogs", "ReturnUserId", "dbo.Users");
            DropForeignKey("dbo.EquipmentLoanLogs", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentLoanLogs", "LoanUserId", "dbo.Users");
            DropForeignKey("dbo.EquipmentLoanLogs", "LoanDeptId", "dbo.Departments");
            DropForeignKey("dbo.EquipmentUsageLogs", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentUsageLogs", "EndUserId", "dbo.Users");
            DropForeignKey("dbo.EquipmentUsageLogs", "BeginUserId", "dbo.Users");
            DropForeignKey("dbo.Equipments", "EquipmentTypeId", "dbo.EquipmentTypes");
            DropForeignKey("dbo.EquipmentPatrolLogs", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentPatrolLogs", "CreatorUserId", "dbo.Users");
            DropForeignKey("dbo.Equipments", "EquipmentKindId", "dbo.EquipmentKinds");
            DropForeignKey("dbo.Equipments", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.EquipmentLoanLogs", new[] { "SignUserId" });
            DropIndex("dbo.EquipmentLoanLogs", new[] { "ReturnUserId" });
            DropIndex("dbo.EquipmentLoanLogs", new[] { "LoanDeptId" });
            DropIndex("dbo.EquipmentLoanLogs", new[] { "LoanUserId" });
            DropIndex("dbo.EquipmentLoanLogs", new[] { "EquipmentId" });
            DropIndex("dbo.EquipmentUsageLogs", new[] { "EndUserId" });
            DropIndex("dbo.EquipmentUsageLogs", new[] { "BeginUserId" });
            DropIndex("dbo.EquipmentUsageLogs", new[] { "EquipmentId" });
            DropIndex("dbo.EquipmentPatrolLogs", new[] { "CreatorUserId" });
            DropIndex("dbo.EquipmentPatrolLogs", new[] { "EquipmentId" });
            DropIndex("dbo.Equipments", new[] { "DepartmentId" });
            DropIndex("dbo.Equipments", new[] { "EquipmentTypeId" });
            DropIndex("dbo.Equipments", new[] { "EquipmentKindId" });
            DropTable("dbo.EquipmentLoanLogs");
            DropTable("dbo.EquipmentUsageLogs");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.EquipmentPatrolLogs");
            DropTable("dbo.EquipmentKinds");
            DropTable("dbo.Equipments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Equipment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
