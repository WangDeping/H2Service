namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editcolperiodid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MeritPayDetails", name: "MeritPayId", newName: "MeritPayPeriodId");
            RenameIndex(table: "dbo.MeritPayDetails", name: "IX_MeritPayId", newName: "IX_MeritPayPeriodId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MeritPayDetails", name: "IX_MeritPayPeriodId", newName: "IX_MeritPayId");
            RenameColumn(table: "dbo.MeritPayDetails", name: "MeritPayPeriodId", newName: "MeritPayId");
        }
    }
}
