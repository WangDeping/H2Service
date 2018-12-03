namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcolperiodid2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.MeritPayPeriods", "CreatorUserId");
            AddForeignKey("dbo.MeritPayPeriods", "CreatorUserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeritPayPeriods", "CreatorUserId", "dbo.Users");
            DropIndex("dbo.MeritPayPeriods", new[] { "CreatorUserId" });
        }
    }
}
