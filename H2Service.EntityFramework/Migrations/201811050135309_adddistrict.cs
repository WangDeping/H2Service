namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class adddistrict : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_District_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Departments", "DistrictId", c => c.Int());
            CreateIndex("dbo.Departments", "DistrictId");
            AddForeignKey("dbo.Departments", "DistrictId", "dbo.Districts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "DistrictId", "dbo.Districts");
            DropIndex("dbo.Departments", new[] { "DistrictId" });
            DropColumn("dbo.Departments", "DistrictId");
            DropTable("dbo.Districts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_District_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
