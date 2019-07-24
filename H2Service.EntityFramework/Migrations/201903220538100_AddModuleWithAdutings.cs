namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddModuleWithAdutings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModuleWithAuditings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Module = c.Int(nullable: false),
                        DepartmentId = c.Int(),
                        DoUserId = c.Long(),
                        AuditorId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ModuleWithAuditing_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuditorId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Users", t => t.DoUserId)
                .Index(t => t.DepartmentId)
                .Index(t => t.DoUserId)
                .Index(t => t.AuditorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModuleWithAuditings", "DoUserId", "dbo.Users");
            DropForeignKey("dbo.ModuleWithAuditings", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ModuleWithAuditings", "AuditorId", "dbo.Users");
            DropIndex("dbo.ModuleWithAuditings", new[] { "AuditorId" });
            DropIndex("dbo.ModuleWithAuditings", new[] { "DoUserId" });
            DropIndex("dbo.ModuleWithAuditings", new[] { "DepartmentId" });
            DropTable("dbo.ModuleWithAuditings",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ModuleWithAuditing_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
