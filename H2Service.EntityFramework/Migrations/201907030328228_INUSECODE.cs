namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INUSECODE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InuseICD9CM3",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperCode = c.String(unicode: false),
                        OperName = c.String(unicode: false),
                        OperDegree = c.String(unicode: false),
                        OperKind = c.String(unicode: false),
                        Option = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InuseICD10",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InuseICD10");
            DropTable("dbo.InuseICD9CM3");
        }
    }
}
