namespace H2Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class QCAppraisal_add_softdelete : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.QCAppraisalPeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Period = c.String(),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        CreatorDepartmentId = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_QCAppraisalPeriod_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.QCAppraisalDetails", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.QCAppraisalPeriods", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QCAppraisalPeriods", "IsDeleted");
            DropColumn("dbo.QCAppraisalDetails", "IsDeleted");
            AlterTableAnnotations(
                "dbo.QCAppraisalPeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Period = c.String(),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        CreatorDepartmentId = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_QCAppraisalPeriod_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
