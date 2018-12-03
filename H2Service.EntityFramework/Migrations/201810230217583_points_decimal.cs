namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class points_decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QCAppraisalDetails", "Points", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.QCAppraisalDetails", "Points", c => c.Int(nullable: false));
        }
    }
}
