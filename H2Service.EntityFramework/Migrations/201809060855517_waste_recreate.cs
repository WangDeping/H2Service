namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class waste_recreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalWastes", "NurseUserId", "dbo.Users");
            DropIndex("dbo.MedicalWastes", new[] { "NurseUserId" });
            RenameColumn(table: "dbo.MedicalWastes", name: "ExternalUserId", newName: "CollecteUserId");
            RenameIndex(table: "dbo.MedicalWastes", name: "IX_ExternalUserId", newName: "IX_CollecteUserId");
            AddColumn("dbo.MedicalWastes", "Kind", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalWastes", "Weight", c => c.Single(nullable: false));
            AddColumn("dbo.MedicalWastes", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalWastes", "Code", c => c.String());
            AddColumn("dbo.MedicalWastes", "CollecteTime", c => c.DateTime());
            AddColumn("dbo.MedicalWastes", "TransportUserId", c => c.Int());
            AddColumn("dbo.MedicalWastes", "TransportTime", c => c.DateTime());
            CreateIndex("dbo.MedicalWastes", "TransportUserId");
            AddForeignKey("dbo.MedicalWastes", "TransportUserId", "dbo.ExternalUsers", "Id");
            DropColumn("dbo.MedicalWastes", "IsInfection");
            DropColumn("dbo.MedicalWastes", "InfectionNumber");
            DropColumn("dbo.MedicalWastes", "InfectionWeight");
            DropColumn("dbo.MedicalWastes", "IsPathology");
            DropColumn("dbo.MedicalWastes", "PathologyNumber");
            DropColumn("dbo.MedicalWastes", "PathologyWeight");
            DropColumn("dbo.MedicalWastes", "IsDrug");
            DropColumn("dbo.MedicalWastes", "DrugNumber");
            DropColumn("dbo.MedicalWastes", "DrugWeight");
            DropColumn("dbo.MedicalWastes", "IsDamage");
            DropColumn("dbo.MedicalWastes", "DamageNumber");
            DropColumn("dbo.MedicalWastes", "DamageWeight");
            DropColumn("dbo.MedicalWastes", "IsChemical");
            DropColumn("dbo.MedicalWastes", "ChemicalNumber");
            DropColumn("dbo.MedicalWastes", "ChemicalWeight");
            DropColumn("dbo.MedicalWastes", "NurseUserId");
            DropColumn("dbo.MedicalWastes", "TurnOverTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalWastes", "TurnOverTime", c => c.DateTime());
            AddColumn("dbo.MedicalWastes", "NurseUserId", c => c.Long());
            AddColumn("dbo.MedicalWastes", "ChemicalWeight", c => c.Single(nullable: false));
            AddColumn("dbo.MedicalWastes", "ChemicalNumber", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalWastes", "IsChemical", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalWastes", "DamageWeight", c => c.Single(nullable: false));
            AddColumn("dbo.MedicalWastes", "DamageNumber", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalWastes", "IsDamage", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalWastes", "DrugWeight", c => c.Single(nullable: false));
            AddColumn("dbo.MedicalWastes", "DrugNumber", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalWastes", "IsDrug", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalWastes", "PathologyWeight", c => c.Single(nullable: false));
            AddColumn("dbo.MedicalWastes", "PathologyNumber", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalWastes", "IsPathology", c => c.Boolean(nullable: false));
            AddColumn("dbo.MedicalWastes", "InfectionWeight", c => c.Single(nullable: false));
            AddColumn("dbo.MedicalWastes", "InfectionNumber", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalWastes", "IsInfection", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.MedicalWastes", "TransportUserId", "dbo.ExternalUsers");
            DropIndex("dbo.MedicalWastes", new[] { "TransportUserId" });
            DropColumn("dbo.MedicalWastes", "TransportTime");
            DropColumn("dbo.MedicalWastes", "TransportUserId");
            DropColumn("dbo.MedicalWastes", "CollecteTime");
            DropColumn("dbo.MedicalWastes", "Code");
            DropColumn("dbo.MedicalWastes", "Status");
            DropColumn("dbo.MedicalWastes", "Weight");
            DropColumn("dbo.MedicalWastes", "Kind");
            RenameIndex(table: "dbo.MedicalWastes", name: "IX_CollecteUserId", newName: "IX_ExternalUserId");
            RenameColumn(table: "dbo.MedicalWastes", name: "CollecteUserId", newName: "ExternalUserId");
            CreateIndex("dbo.MedicalWastes", "NurseUserId");
            AddForeignKey("dbo.MedicalWastes", "NurseUserId", "dbo.Users", "Id");
        }
    }
}
