namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RYQF : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomePages", "RYQ_F", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomePages", "RYQ_F");
        }
    }
}
