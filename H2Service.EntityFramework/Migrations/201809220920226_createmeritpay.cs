namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createmeritpay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeritPayDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserNumber = c.String(),
                        HeaderNumber = c.String(),
                        MeritPayId = c.Int(nullable: false),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeritPayPeriods", t => t.MeritPayId, cascadeDelete: true)
                .Index(t => t.MeritPayId);
            
            CreateTable(
                "dbo.MeritPayPeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Period = c.String(),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeritPayDetails", "MeritPayId", "dbo.MeritPayPeriods");
            DropIndex("dbo.MeritPayDetails", new[] { "MeritPayId" });
            DropTable("dbo.MeritPayPeriods");
            DropTable("dbo.MeritPayDetails");
        }
    }
}
