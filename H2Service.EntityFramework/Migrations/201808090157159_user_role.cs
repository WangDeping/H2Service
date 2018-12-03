namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_role : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropIndex("dbo.RoleUsers", new[] { "User_Id" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropTable("dbo.RoleUsers");
        }
    }
}
