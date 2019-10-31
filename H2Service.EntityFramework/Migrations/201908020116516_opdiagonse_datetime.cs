namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class opdiagonse_datetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OPMedicalDiagnoses", "AdDate", c => c.DateTime(precision: 0));
            AlterColumn("dbo.OPMedicalDiagnoses", "Birthday", c => c.DateTime(precision: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OPMedicalDiagnoses", "Birthday", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.OPMedicalDiagnoses", "AdDate", c => c.DateTime(nullable: false, precision: 0));
        }
    }
}
