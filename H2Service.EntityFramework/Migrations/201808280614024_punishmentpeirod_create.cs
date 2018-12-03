namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class punishmentpeirod_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentPunishmentDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentPunishmentPeriodId = c.Int(nullable: false),
                        FunctionalDepartmentId = c.Int(nullable: false),
                        PunishedDepartmentId = c.Int(nullable: false),
                        PunishedUserId = c.Long(),
                        Desc = c.String(),
                        Points = c.Int(nullable: false),
                        CreatorUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorUserId)
                .ForeignKey("dbo.DepartmentPunishmentPeriods", t => t.DepartmentPunishmentPeriodId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.FunctionalDepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Departments", t => t.PunishedDepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.PunishedUserId)
                .Index(t => t.DepartmentPunishmentPeriodId)
                .Index(t => t.FunctionalDepartmentId)
                .Index(t => t.PunishedDepartmentId)
                .Index(t => t.PunishedUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.DepartmentPunishmentPeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Period = c.String(),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        CreatorDepartmentId = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.CreatorDepartmentId)
                .ForeignKey("dbo.Users", t => t.CreatorUserId)
                .Index(t => t.CreatorUserId)
                .Index(t => t.CreatorDepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentPunishmentDetails", "PunishedUserId", "dbo.Users");
            DropForeignKey("dbo.DepartmentPunishmentDetails", "PunishedDepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DepartmentPunishmentDetails", "FunctionalDepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DepartmentPunishmentDetails", "DepartmentPunishmentPeriodId", "dbo.DepartmentPunishmentPeriods");
            DropForeignKey("dbo.DepartmentPunishmentPeriods", "CreatorUserId", "dbo.Users");
            DropForeignKey("dbo.DepartmentPunishmentPeriods", "CreatorDepartmentId", "dbo.Departments");
            DropForeignKey("dbo.DepartmentPunishmentDetails", "CreatorUserId", "dbo.Users");
            DropIndex("dbo.DepartmentPunishmentPeriods", new[] { "CreatorDepartmentId" });
            DropIndex("dbo.DepartmentPunishmentPeriods", new[] { "CreatorUserId" });
            DropIndex("dbo.DepartmentPunishmentDetails", new[] { "CreatorUserId" });
            DropIndex("dbo.DepartmentPunishmentDetails", new[] { "PunishedUserId" });
            DropIndex("dbo.DepartmentPunishmentDetails", new[] { "PunishedDepartmentId" });
            DropIndex("dbo.DepartmentPunishmentDetails", new[] { "FunctionalDepartmentId" });
            DropIndex("dbo.DepartmentPunishmentDetails", new[] { "DepartmentPunishmentPeriodId" });
            DropTable("dbo.DepartmentPunishmentPeriods");
            DropTable("dbo.DepartmentPunishmentDetails");
        }
    }
}
