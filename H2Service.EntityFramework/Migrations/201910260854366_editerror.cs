namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editerror : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EquipmentLoanLogs", "BorrowDeptId");
            DropColumn("dbo.EquipmentLoanLogs", "BorrowUserId");
            RenameColumn(table: "dbo.EquipmentLoanLogs", name: "LoanDeptId", newName: "BorrowDeptId");
            RenameColumn(table: "dbo.EquipmentLoanLogs", name: "LoanUserId", newName: "BorrowUserId");
            RenameIndex(table: "dbo.EquipmentLoanLogs", name: "IX_LoanUserId", newName: "IX_BorrowUserId");
            RenameIndex(table: "dbo.EquipmentLoanLogs", name: "IX_LoanDeptId", newName: "IX_BorrowDeptId");
          
        }
        
        public override void Down()
        {
            DropIndex("dbo.EquipmentLoanLogs", new[] { "LoanDeptId" });
            DropIndex("dbo.EquipmentLoanLogs", new[] { "LoanUserId" });
            RenameIndex(table: "dbo.EquipmentLoanLogs", name: "IX_BorrowDeptId", newName: "IX_LoanDeptId");
            RenameIndex(table: "dbo.EquipmentLoanLogs", name: "IX_BorrowUserId", newName: "IX_LoanUserId");
            RenameColumn(table: "dbo.EquipmentLoanLogs", name: "BorrowUserId", newName: "LoanUserId");
            RenameColumn(table: "dbo.EquipmentLoanLogs", name: "BorrowDeptId", newName: "LoanDeptId");
            AddColumn("dbo.EquipmentLoanLogs", "BorrowUserId", c => c.Long(nullable: false));
            AddColumn("dbo.EquipmentLoanLogs", "BorrowDeptId", c => c.Int(nullable: false));
        }
    }
}
