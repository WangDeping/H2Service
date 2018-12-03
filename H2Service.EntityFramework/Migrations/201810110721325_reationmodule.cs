namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class reationmodule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentRelateModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        Module = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DepartmentRelateModule_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentRelateModules", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.DepartmentRelateModules", new[] { "DepartmentId" });
            DropTable("dbo.DepartmentRelateModules",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DepartmentRelateModule_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
