namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class opdiagonse_age : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OPMedicalDiagnoses", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OPMedicalDiagnoses", "Age", c => c.Int(nullable: false));
        }
    }
}
