namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolnurseouthosday_homeapge : DbMigration
    {
        public override void Up()
        {
           // AddColumn("dbo.HomePages", "NurseOutHosDay", c => c.DateTime(precision: 0));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.HomePages", "NurseOutHosDay");
        }
    }
}
