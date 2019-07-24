namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validate_dischargedate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomePageValidateMessages", "DischargeDate", c => c.DateTime(precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomePageValidateMessages", "DischargeDate");
        }
    }
}
