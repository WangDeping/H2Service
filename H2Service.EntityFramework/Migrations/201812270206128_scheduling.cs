namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class scheduling : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchedulingGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchedulingGroupName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SchedulingGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchedulingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SchedulingTypeName = c.String(),
                        IsEnable = c.Boolean(nullable: false),
                        IsTimeLimit = c.Boolean(nullable: false),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        EndTimeDayType = c.Int(nullable: false),
                        SchedulingTypeValue = c.Single(nullable: false),
                        IsRequiredAttence = c.Boolean(nullable: false),
                        IsDuty = c.Boolean(nullable: false),
                        SchedulingGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SchedulingGroups", t => t.SchedulingGroup_Id)
                .Index(t => t.SchedulingGroup_Id);
            
            CreateTable(
                "dbo.SchedulingRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Long(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        IsChecked = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SchedulingRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorUserId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.DepartmentId)
                .Index(t => t.CreatorUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedulingRecords", "UserId", "dbo.Users");
            DropForeignKey("dbo.SchedulingRecords", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.SchedulingRecords", "CreatorUserId", "dbo.Users");
            DropForeignKey("dbo.SchedulingTypes", "SchedulingGroup_Id", "dbo.SchedulingGroups");
            DropIndex("dbo.SchedulingRecords", new[] { "CreatorUserId" });
            DropIndex("dbo.SchedulingRecords", new[] { "DepartmentId" });
            DropIndex("dbo.SchedulingRecords", new[] { "UserId" });
            DropIndex("dbo.SchedulingTypes", new[] { "SchedulingGroup_Id" });
            DropTable("dbo.SchedulingRecords",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SchedulingRecord_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.SchedulingTypes");
            DropTable("dbo.SchedulingGroups",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SchedulingGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
