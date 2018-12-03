namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alertpermissionName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Permissions", newName: "H2Permission");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.H2Permission", newName: "Permissions");
        }
    }
}
