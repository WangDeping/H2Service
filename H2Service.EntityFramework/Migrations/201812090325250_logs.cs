namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserNumber = c.String(),
                        Device = c.String(),
                        IP = c.String(),
                        LoginTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoginLogs");
        }
    }
}
