namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validatetype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomePageValidateMessages", "ValidateType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomePageValidateMessages", "ValidateType");
        }
    }
}
