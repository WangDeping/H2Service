namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_waste_images : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalWasteImages", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicalWasteImages", "Status");
        }
    }
}
