namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class waste_rename_collect : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MedicalWastes", name: "CollecteUserId", newName: "CollectUserId");
            RenameIndex(table: "dbo.MedicalWastes", name: "IX_CollecteUserId", newName: "IX_CollectUserId");
            AddColumn("dbo.MedicalWastes", "CollectTime", c => c.DateTime());
            DropColumn("dbo.MedicalWastes", "CollecteTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalWastes", "CollecteTime", c => c.DateTime());
            DropColumn("dbo.MedicalWastes", "CollectTime");
            RenameIndex(table: "dbo.MedicalWastes", name: "IX_CollectUserId", newName: "IX_CollecteUserId");
            RenameColumn(table: "dbo.MedicalWastes", name: "CollectUserId", newName: "CollecteUserId");
        }
    }
}
