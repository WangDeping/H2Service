namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_colec : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MedicalWasteFlows", name: "CollectUserId2", newName: "CollectUserId");
           // RenameIndex(table: "dbo.MedicalWasteFlows", name: "IX_CollectUserId2", newName: "IX_CollectUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MedicalWasteFlows", name: "IX_CollectUserId", newName: "IX_CollectUserId2");
            RenameColumn(table: "dbo.MedicalWasteFlows", name: "CollectUserId", newName: "CollectUserId2");
        }
    }
}
