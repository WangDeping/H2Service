namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_cysjs_rysjs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HomePages", "RYSJS", c => c.String(unicode: false));
            AlterColumn("dbo.HomePages", "CYSJS", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HomePages", "CYSJS", c => c.Int());
            AlterColumn("dbo.HomePages", "RYSJS", c => c.Int());
        }
    }
}
