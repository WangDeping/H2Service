namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class removes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SchedulingDepartments", "AuditorId", "dbo.Users");
            DropForeignKey("dbo.SchedulingDepartments", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.SchedulingDepartments", "ShedulingUserId", "dbo.Users");
            DropIndex("dbo.SchedulingDepartments", new[] { "DepartmentId" });
            DropIndex("dbo.SchedulingDepartments", new[] { "ShedulingUserId" });
            DropIndex("dbo.SchedulingDepartments", new[] { "AuditorId" });
            DropTable("dbo.SchedulingDepartments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SchedulingDepartment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SchedulingDepartments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        ShedulingUserId = c.Long(),
                        AuditorId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SchedulingDepartment_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.SchedulingDepartments", "AuditorId");
            CreateIndex("dbo.SchedulingDepartments", "ShedulingUserId");
            CreateIndex("dbo.SchedulingDepartments", "DepartmentId");
            AddForeignKey("dbo.SchedulingDepartments", "ShedulingUserId", "dbo.Users", "Id");
            AddForeignKey("dbo.SchedulingDepartments", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SchedulingDepartments", "AuditorId", "dbo.Users", "Id");
        }
    }
}
