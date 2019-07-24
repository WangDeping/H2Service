namespace H2Service.MigrationsMysql
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class homepage_validatemsg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HomePageValidateMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserNumber = c.String(unicode: false),
                        BAH = c.String(unicode: false),
                        AdmNo = c.String(unicode: false),
                        Dep = c.String(unicode: false),
                        Message = c.String(unicode: false),
                        SendTime = c.DateTime(nullable: false, precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HomePageValidateMessage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HomePageValidateMessages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_HomePageValidateMessage_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
