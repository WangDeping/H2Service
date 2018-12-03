namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QCAppraisal_create : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DepartmentPunishmentDetails", newName: "QCAppraisalDetails");
            RenameTable(name: "dbo.DepartmentPunishmentPeriods", newName: "QCAppraisalPeriods");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.QCAppraisalPeriods", newName: "DepartmentPunishmentPeriods");
            RenameTable(name: "dbo.QCAppraisalDetails", newName: "DepartmentPunishmentDetails");
        }
    }
}
