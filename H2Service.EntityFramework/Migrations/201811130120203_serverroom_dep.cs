namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serverroom_dep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerRooms", "DepartmentId", c => c.Int(nullable: true));
            CreateIndex("dbo.ServerRooms", "DepartmentId");
            AddForeignKey("dbo.ServerRooms", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServerRooms", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.ServerRooms", new[] { "DepartmentId" });
            DropColumn("dbo.ServerRooms", "DepartmentId");
        }
    }
}
