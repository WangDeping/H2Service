namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class updatetableName_h2moduleaudutingwith : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ModuleWithAuditings", newName: "H2ModuleWithAuditing");
            AlterTableAnnotations(
                "dbo.H2ModuleWithAuditing",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Module = c.Int(nullable: false),
                        DepartmentId = c.Int(),
                        DoUserId = c.Long(),
                        AuditorId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_H2ModuleWithAuditing_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_ModuleWithAuditing_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
        
        public override void Down()
        {
            AlterTableAnnotations(
                "dbo.H2ModuleWithAuditing",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Module = c.Int(nullable: false),
                        DepartmentId = c.Int(),
                        DoUserId = c.Long(),
                        AuditorId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_H2ModuleWithAuditing_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_ModuleWithAuditing_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            RenameTable(name: "dbo.H2ModuleWithAuditing", newName: "ModuleWithAuditings");
        }
    }
}
