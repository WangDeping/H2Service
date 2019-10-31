namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validate_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomePageValidateMessages", "ValidateStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomePageValidateMessages", "ValidateStatus");
        }
    }
}
