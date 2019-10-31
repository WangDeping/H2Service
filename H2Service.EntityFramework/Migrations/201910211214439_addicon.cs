namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addicon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EquipmentTypes", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EquipmentTypes", "Icon");
        }
    }
}
