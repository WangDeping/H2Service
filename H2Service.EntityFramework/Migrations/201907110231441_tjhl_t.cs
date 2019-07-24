namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tjhl_t : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HomePages", "TJHL_T", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HomePages", "TJHL_T", c => c.Int());
        }
    }
}
