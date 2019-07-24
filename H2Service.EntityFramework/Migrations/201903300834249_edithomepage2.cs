namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edithomepage2 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.HomePages", "CSRQ", c => c.DateTime(precision: 0));
            //DropColumn("dbo.HomePages", "CRSQ");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.HomePages", "CRSQ", c => c.DateTime(precision: 0));
            //DropColumn("dbo.HomePages", "CSRQ");
        }
    }
}
