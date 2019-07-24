namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validate_senduser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomePageValidateMessages", "SendUser", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomePageValidateMessages", "SendUser");
        }
    }
}
