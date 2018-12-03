namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class waste_add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExternalCompanies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Aspect = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExternalUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Code = c.String(),
                        TelPhone = c.String(),
                        IDNumber = c.String(),
                        AvatarUrl = c.String(),
                        Gender = c.Int(nullable: false),
                        ExternalCompanyId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ExternalUser_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExternalCompanies", t => t.ExternalCompanyId, cascadeDelete: true)
                .Index(t => t.ExternalCompanyId);
            
            CreateTable(
                "dbo.MedicalWastes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsInfection = c.Boolean(nullable: false),
                        InfectionNumber = c.Int(nullable: false),
                        InfectionWeight = c.Single(nullable: false),
                        IsPathology = c.Boolean(nullable: false),
                        PathologyNumber = c.Int(nullable: false),
                        PathologyWeight = c.Single(nullable: false),
                        IsDrug = c.Boolean(nullable: false),
                        DrugNumber = c.Int(nullable: false),
                        DrugWeight = c.Single(nullable: false),
                        IsDamage = c.Boolean(nullable: false),
                        DamageNumber = c.Int(nullable: false),
                        DamageWeight = c.Single(nullable: false),
                        IsChemical = c.Boolean(nullable: false),
                        ChemicalNumber = c.Int(nullable: false),
                        ChemicalWeight = c.Single(nullable: false),
                        NurseUserId = c.Long(),
                        ExternalUserId = c.Int(),
                        ImgsPath = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        TurnOverTime = c.DateTime(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MedicalWaste_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorUserId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.ExternalUsers", t => t.ExternalUserId)
                .ForeignKey("dbo.Users", t => t.NurseUserId)
                .Index(t => t.NurseUserId)
                .Index(t => t.ExternalUserId)
                .Index(t => t.DepartmentId)
                .Index(t => t.CreatorUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalWastes", "NurseUserId", "dbo.Users");
            DropForeignKey("dbo.MedicalWastes", "ExternalUserId", "dbo.ExternalUsers");
            DropForeignKey("dbo.MedicalWastes", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.MedicalWastes", "CreatorUserId", "dbo.Users");
            DropForeignKey("dbo.ExternalUsers", "ExternalCompanyId", "dbo.ExternalCompanies");
            DropIndex("dbo.MedicalWastes", new[] { "CreatorUserId" });
            DropIndex("dbo.MedicalWastes", new[] { "DepartmentId" });
            DropIndex("dbo.MedicalWastes", new[] { "ExternalUserId" });
            DropIndex("dbo.MedicalWastes", new[] { "NurseUserId" });
            DropIndex("dbo.ExternalUsers", new[] { "ExternalCompanyId" });
            DropTable("dbo.MedicalWastes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MedicalWaste_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ExternalUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ExternalUser_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.ExternalCompanies");
        }
    }
}
