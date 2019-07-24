namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumadmno_homeapge : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.HomePages", "AdmNo", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "InHosDep", c => c.String(unicode: false));
            //AddColumn("dbo.HomePages", "OutHosDep", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.HomePages", "OutHosDep");
            //DropColumn("dbo.HomePages", "InHosDep");
            //DropColumn("dbo.HomePages", "AdmNo");
        }
    }
}
