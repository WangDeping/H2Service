namespace H2Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbirthday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Brithday", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Brithday");
        }
    }
}
