namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class opdiagonse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OPMedicalDiagnoses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdmNo = c.String(unicode: false),
                        PatNo = c.String(unicode: false),
                        AdDate = c.DateTime(nullable: false, precision: 0),
                        PatName = c.String(unicode: false),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        Age = c.Int(nullable: false),
                        PhoneNumber = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        AdmDep = c.String(unicode: false),
                        DoctorNo = c.String(unicode: false),
                        DoctorName = c.String(unicode: false),
                        DiagnoseCode1 = c.String(unicode: false),
                        DiagnoseName1 = c.String(unicode: false),
                        DiagnoseType1 = c.String(unicode: false),
                        DiagnoseStatus1 = c.String(unicode: false),
                        DiagnoseCode2 = c.String(unicode: false),
                        DiagnoseName2 = c.String(unicode: false),
                        DiagnoseType2 = c.String(unicode: false),
                        DiagnoseStatus2 = c.String(unicode: false),
                        DiagnoseCode3 = c.String(unicode: false),
                        DiagnoseName3 = c.String(unicode: false),
                        DiagnoseType3 = c.String(unicode: false),
                        DiagnoseStatus3 = c.String(unicode: false),
                        DiagnoseCode4 = c.String(unicode: false),
                        DiagnoseName4 = c.String(unicode: false),
                        DiagnoseType4 = c.String(unicode: false),
                        DiagnoseStatus4 = c.String(unicode: false),
                        DiagnoseCode5 = c.String(unicode: false),
                        DiagnoseName5 = c.String(unicode: false),
                        DiagnoseType5 = c.String(unicode: false),
                        DiagnoseStatus5 = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OPMedicalDiagnoses");
        }
    }
}
