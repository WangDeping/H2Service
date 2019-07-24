namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class homepage_ryh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomePages", "RYH_T", c => c.Int());
            AddColumn("dbo.HomePages", "RYH_XS", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomePages", "RYH_XS");
            DropColumn("dbo.HomePages", "RYH_T");
        }
    }
}
