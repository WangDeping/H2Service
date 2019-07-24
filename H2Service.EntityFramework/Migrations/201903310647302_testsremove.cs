namespace H2Service.MigrationsMysql
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testsremove : DbMigration
    {
        public override void Up()
        {
            //DropTable("dbo.Tests");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.Tests",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(unicode: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
    }
}
